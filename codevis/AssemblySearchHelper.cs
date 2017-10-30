using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gma.CodeVisuals.Generator
{
    internal class AssemblySearchHelper
    {
        private static readonly string[] AssemblyExtensions = { ".dll", ".exe" };

        public static List<string> GetAssemblyFullNames(IEnumerable<string> patterns, IEnumerable<string> searchPaths)
        {
            return patterns
                .SelectMany(pattern => SeacrhAssembly(searchPaths, pattern))
                .Where(IsAssembly)
                .ToList();
        }

        public static void OptionsToNamesAndSearchPath(Options options, out IEnumerable<string> patterns, out IEnumerable<string> searchPaths)
        {
            var files = new Stack<string>();
            var paths = new Stack<string>(options.Path);

            foreach (var item in options.Assemblies)
            {
                string pattern;
                if (Path.IsPathRooted(item))
                {
                    pattern = Path.GetFileName(item);
                    string path = Path.GetDirectoryName(item);
                    paths.Push(path);
                }
                else
                {
                    pattern = item;
                }
                files.Push(pattern);
            }

            patterns = files.Distinct().ToList();
            searchPaths = paths.Distinct().ToList();
        }

        private static IEnumerable<string> SeacrhAssembly(IEnumerable<string> paths, string pattern)
        {
            return paths
                .SelectMany(p => GetBestMatchFullNames(p, pattern));
        }

        private static bool IsAssembly(string path)
        {
            return
                AssemblyExtensions.Contains(
                    Path.GetExtension(path) ?? String.Empty,
                    StringComparer.InvariantCultureIgnoreCase);
        }

        private static IEnumerable<string> GetBestMatchFullNames(string path, string pattern)
        {
            return GetBestMatchNames(path, pattern).Select(shortName => Path.Combine(path, shortName));
        }

        private static IEnumerable<string> GetBestMatchNames(string path, string pattern)
        {
            var result = Directory.GetFiles(path, pattern).Where(IsAssembly).ToArray();
            if (result.Length > 0) return result;
            foreach (var extension in AssemblyExtensions)
            {
                result = Directory.GetFiles(path, pattern + extension);
                if (result.Length > 0) return result;
            }
            return result;
        }
    }
}