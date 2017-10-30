// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System.Collections.Generic;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.TypeTree
{
    internal class NodeFactory
    {
        private readonly Stack<INode> m_Nodes;

        public NodeFactory()
        {
            m_Nodes = new Stack<INode>();
        }

        public int Count => m_Nodes.Count;

        public IEnumerable<INode> Nodes => m_Nodes;

        public Node CreateNode(string name, Node parent)
        {
            var node = new Node(name, Count, parent);
            m_Nodes.Push(node);
            return node;
        }

        public Leaf CreateLeaf(string name, Node parent)
        {
            var leaf = new Leaf(name, Count, parent);
            m_Nodes.Push(leaf);
            return leaf;
        }
    }
}