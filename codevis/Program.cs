// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CommandLine;
using Gma.CodeVisuals.Generator.DependencyForceGraph;
using Gma.CodeVisuals.Generator.DependencyForceGraph.Do;
using System.Threading;

#endregion

namespace Gma.CodeVisuals.Generator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RedirectUnhandledExceptionsToConsole();
            var options = GetParsedArguments(args);
            var graph = Generate(options);
            //if (options.Verbose) Process.Start("Gma.CodeVisuals.WebApi.exe");
        }

        private static Options GetParsedArguments(string[] args)
        {
            var options = new Options();
            bool isOk = Parser.Default.ParseArguments(args, options);
            if (!isOk || options.Assemblies==null || options.Assemblies.Count==0)
            {
                Console.WriteLine(options.GetUsage());
                Environment.Exit(1);
            }

            if (options.Path == null || options.Path.Count == 0)
            {
                options.Path = new[] {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)};
            }
            return options;
        }

        private static void RedirectUnhandledExceptionsToConsole()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var exception = e.ExceptionObject as Exception;
                var message = exception != null
                    ? string.Format("ERROR: {0}", exception.Message)
                    : e.ExceptionObject;
                Console.WriteLine(message);
                Environment.Exit(1);
            };
        }

        private static Graph Generate(Options options)
        {
            IEnumerable<string> patterns;
            IEnumerable<string> searchPaths;

            AssemblySearchHelper.OptionsToNamesAndSearchPath(options, out patterns, out searchPaths);
            var assemblyFullNames = AssemblySearchHelper.GetAssemblyFullNames(patterns, searchPaths);

            Console.WriteLine("{0} files found.", assemblyFullNames.Count);
            if (assemblyFullNames.Count == 0) Environment.Exit(1);
            Console.WriteLine("Loading assemblies:");
            var rootName = GenerateResultFileName(assemblyFullNames);

            var ePrev = AnalyzesProgress.Started();
            var analyzer = new Analyzer(eCurrent => { ePrev = ConsoleProgress(eCurrent, ePrev); });

            using(var loadHelper = new AssemblyLoadHelper(searchPaths))
            {
                var assemblies = assemblyFullNames.Select(loadHelper.Load);
                analyzer.Analyze(assemblies, rootName, CancellationToken.None);
            }
            return analyzer.GetGraph();
        }

        private static string GenerateResultFileName(List<string> assemblyFullPaths)
        {
            if (assemblyFullPaths.Count == 0) Environment.Exit(1);
            if (assemblyFullPaths.Count == 1) return Path.GetFileNameWithoutExtension(assemblyFullPaths[0]);
            string dirName =
                (Path.GetDirectoryName(assemblyFullPaths[0]) ?? "unknown").Split(Path.PathSeparator).LastOrDefault();
            return string.Format("{0} assemblies in ({1})", assemblyFullPaths.Count, dirName);
        }

        private static AnalyzesProgress ConsoleProgress(AnalyzesProgress eCurrent, AnalyzesProgress ePrev)
        {
            if (eCurrent.IsFinished)
            {
                Console.WriteLine("Analyzes finished.");
                return ePrev;
            }
            var currentPercentage = eCurrent.Actual*100/eCurrent.Max;
            int prevPercentage = ePrev.Actual*100/ePrev.Max;
            if (currentPercentage != prevPercentage)
            {
                Console.Write("\r{0} {1}%   ", eCurrent.Message, currentPercentage);
                ePrev = eCurrent;
            }
            return ePrev;
        }
    }
}