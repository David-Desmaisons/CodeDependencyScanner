using System;
using Gma.CodeVisuals.Generator.DependencyForceGraph.Do;
using System.Collections.Generic;
using Gma.CodeVisuals.Generator.DependencyForceGraph;
using System.Linq;
using System.IO;
using System.Threading;

namespace Gma.CodeVisuals.Generator.Api
{
    public class GraphBuilder : IGraphBuilder
    {
        public AnalyseResult GenerateFromAssembly(string assemblyPath, CancellationToken cancellationToken, IProgress<AnalyzesProgress> progress)
        {
            var options = new Options()
            {
                Assemblies = new List<string> { assemblyPath },
                Path = new[] { Path.GetDirectoryName(assemblyPath) }
            };
            var graph = Generate(options, cancellationToken, progress);
            return new AnalyseResult(graph, Path.GetFileName(assemblyPath));
        }

        private static Graph Generate(Options options, CancellationToken cancellationToken, IProgress<AnalyzesProgress> progress)
        {
            IEnumerable<string> patterns;
            IEnumerable<string> searchPaths;

            AssemblySearchHelper.OptionsToNamesAndSearchPath(options, out patterns, out searchPaths);
            var assemblyFullNames = AssemblySearchHelper.GetAssemblyFullNames(patterns, searchPaths);

            if (assemblyFullNames.Count == 0)
                return null;

            Console.WriteLine("Loading assemblies:");
            var rootName = GenerateResultFileName(assemblyFullNames);

            progress?.Report(AnalyzesProgress.Started());
            var analyzer = new Analyzer(eCurrent => progress?.Report(eCurrent));

            using (var loadHelper = new AssemblyLoadHelper(searchPaths))
            {
                var assemblies = assemblyFullNames.Select(loadHelper.Load);
                analyzer.Analyze(assemblies, rootName, cancellationToken);
            }
            return analyzer.GetGraph();
        }

        private static string GenerateResultFileName(List<string> assemblyFullPaths)
        {
            if (assemblyFullPaths.Count == 0) Environment.Exit(1);
            if (assemblyFullPaths.Count == 1) return Path.GetFileNameWithoutExtension(assemblyFullPaths[0]);
            string dirName =
                (Path.GetDirectoryName(assemblyFullPaths[0]) ?? "unknown").Split(Path.PathSeparator).LastOrDefault();
            return $"{assemblyFullPaths.Count} assemblies in ({dirName})";
        }
    }
}
