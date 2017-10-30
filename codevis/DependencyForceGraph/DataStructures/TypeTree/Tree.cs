// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System.Collections.Generic;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.TypeTree
{
    internal class Tree
    {
        private readonly NodeFactory m_NodeFactory;

        private readonly Node m_Root;

        public Tree(NodeFactory nodeFactory, string rootName)
        {
            m_NodeFactory = nodeFactory;
            m_Root = m_NodeFactory.CreateNode(rootName, null);
        }

        public int Count => m_NodeFactory.Count;

        public IEnumerable<INode> Nodes => m_NodeFactory.Nodes;

        public INode Root => m_Root;

        public INode Add(string fullName)
        {
            var qualifiedName = QualifiedName.Parse(fullName);
            var names = new Queue<string>(qualifiedName.Nodes);
            var node = m_Root.GetOrCreate(names, m_NodeFactory);
            var leaf = m_NodeFactory.CreateLeaf(qualifiedName.Leaf, node);
            node.AddLeaf(leaf);
            return leaf;
        }

        public bool TryGet(string fullName, out INode leaf)
        {
            leaf = null;
            if (string.IsNullOrEmpty(fullName)) return false;
            var qualifiedName = QualifiedName.Parse(fullName);
            var names = new Queue<string>(qualifiedName.Nodes);
            Node node;
            bool nodeFound = m_Root.TryGetNode(names, out node);
            if (!nodeFound) return false;
            Leaf result;
            bool isOk = node.TryGetLeaf(qualifiedName.Leaf, out result);
            leaf = result;
            return isOk;
        }
    }
}