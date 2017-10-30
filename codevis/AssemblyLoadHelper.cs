// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

#endregion

namespace Gma.CodeVisuals.Generator
{
    internal class AssemblyLoadHelper : IDisposable
    {
        private readonly ResolveEventHandler m_Resolver;

        public AssemblyLoadHelper(IEnumerable<string> searchPaths)
        {
            m_Resolver = (sender, args) => Resolve(args, searchPaths);
            AppDomain.CurrentDomain.AssemblyResolve += m_Resolver;
        }


        public Assembly Load(string fullPath)
        {
            Console.WriteLine(fullPath);
            return Assembly.LoadFile(fullPath);
        }

        private static Assembly Resolve(ResolveEventArgs args, IEnumerable<string> directories)
        {
            return directories
                .Select(d => Resolve(args, d))
                .FirstOrDefault();
        }

        private static Assembly Resolve(ResolveEventArgs args, string directory)
        {
            var name = args.Name;
            var fileName = name.Substring(0, name.IndexOf(','));
            var fullPath = Path.Combine(directory, fileName + ".dll");
            if (File.Exists(fullPath)) return Assembly.LoadFile(fullPath);
            fullPath = Path.Combine(directory, fileName + ".exe");
            if (File.Exists(fullPath)) return Assembly.LoadFile(fullPath);
            Console.WriteLine("Can not resolve assembly [{0}] in folder [{1}].", name, directory);
            return null;
        }

        public void Dispose()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= m_Resolver;
        }
    }
}