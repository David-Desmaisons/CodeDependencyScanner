// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

#endregion

namespace Gma.CodeVisuals.Generator
{
    internal class Options
    {
        [ValueList(typeof (List<string>))]
        public IList<string> Assemblies { get; set; }

        [Option('v', "verbose", DefaultValue = true, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [OptionList('p', "path", Separator = ';', HelpText = "Specify search path, separated by a semicolon.")]
        public IList<string> Path { get; set; }

        [Option('o', "output", DefaultValue = "", HelpText = "Oputput folder for graph files.")]
        public string Output { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = HelpText.AutoBuild(this,
                current => HelpText.DefaultParsingErrorsHandler(this, current));
            help.MaximumDisplayWidth = 80;
            help.AddPreOptionsLine(
                string.Format(
                    @"{0}"
                    + "Usage:{0}"
                    + "{0}"
                    + "codevis.exe assembly1 [assembly2...N] [-p[[;]directories]] [-v] [-o outdir] {0}"
                    + "{0}"
                    + "Enter assembly names separated by space. "
                    + "Assembly names can contain '*' and '?' wildcards. "
                    + "Names are either absolute paths or file names under one of the folders from path option.{0}"
                    + "{0}"
                    + "Examples:{0}"
                    + "{0}"
                    + " codevis.exe c:/dir/System.Core.dll {0}"
                    + " codevis.exe c:/dir/System.Data*{0}"
                    + " codevis.exe MyDll System.Core -p: C:/dir/;c:/mydir{0}"
                    + "{0}"
                    , Environment.NewLine));

            return help;
        }
    }
}