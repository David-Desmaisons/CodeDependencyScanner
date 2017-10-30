// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.TypeTree
{
    [DebuggerDisplay("{text}:{id}}")]
    internal abstract class NodeBase : INode
    {
        private readonly int m_Id;
        private readonly string m_Name;
        private readonly NodeBase m_Parent;

        internal NodeBase(string name, int id, NodeBase parent)
        {
            m_Name = name;
            m_Id = id;
            m_Parent = parent;
        }

        internal NodeBase parent => m_Parent;

        public int id => m_Id;

        public string text => m_Name;

        public abstract IEnumerable<INode> children { get; }
        public abstract bool IsLeaf();
    }
}