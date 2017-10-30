// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.TypeTree
{
    internal class Node : NodeBase
    {
        private readonly Dictionary<string, Node> m_ChildNodes;
        private readonly Dictionary<string, Leaf> m_Leafs;

        internal Node(string name, int id, Node node) : base(name, id, node)
        {
            m_ChildNodes = new Dictionary<string, Node>();
            m_Leafs = new Dictionary<string, Leaf>();
        }

        public override IEnumerable<INode> children
        {
            get
            {
                return m_ChildNodes.Values.OrderBy(node => node.text)
                                   .Concat(m_Leafs.Values.OrderBy(l => l.text).Cast<INode>());
            }
        }

        public override bool IsLeaf()
        {
            return false;
        }

        internal Node GetOrCreate(Queue<string> nodeNames, NodeFactory factory)
        {
            if (nodeNames.Count == 0) return this;
            var name = nodeNames.Dequeue();
            Node node;
            bool isOk = m_ChildNodes.TryGetValue(name, out node);
            if (!isOk)
            {
                node = factory.CreateNode(name, this);
                m_ChildNodes.Add(name, node);
            }
            return node.GetOrCreate(nodeNames, factory);
        }

        public void AddLeaf(Leaf leaf)
        {
            m_Leafs.Add(leaf.text, leaf);
        }

        public bool TryGetLeaf(string leaf, out Leaf node)
        {
            return m_Leafs.TryGetValue(leaf, out node);
        }

        public bool TryGetNode(Queue<string> nodeNames, out Node node)
        {
            if (nodeNames.Count == 0)
            {
                node = this;
                return true;
            }

            var name = nodeNames.Dequeue();
            bool isOk = m_ChildNodes.TryGetValue(name, out node);
            if (!isOk)
            {
                return false;
            }
            return node.TryGetNode(nodeNames, out node);
        }
    }
}