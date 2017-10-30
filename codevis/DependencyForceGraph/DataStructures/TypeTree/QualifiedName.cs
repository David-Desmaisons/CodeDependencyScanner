// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Collections.Generic;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.TypeTree
{
    public class QualifiedName
    {
        public const char Qualifier = '.';
        private readonly string m_Leaf;
        private readonly string[] m_Path;

        protected QualifiedName(string[] path, string leaf)
        {
            m_Path = path;
            m_Leaf = leaf;
        }

        public IEnumerable<string> Nodes => m_Path;

        public string Leaf => m_Leaf;

        public static QualifiedName Parse(string fullName)
        {
            string[] parts = fullName.Split(Qualifier);
            var length = parts.Length - 1;
            var path = new string[length];
            Array.Copy(parts, path, length);
            var leaf = parts[length];
            return new QualifiedName(path, leaf);
        }
    }
}