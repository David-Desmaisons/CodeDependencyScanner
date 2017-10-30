// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.TypeTree
{
    internal class Leaf : NodeBase
    {
        public Leaf(string name, int id, Node node) : base(name, id, node)
        {
        }

        public override IEnumerable<INode> children => null;

        public override bool IsLeaf()
        {
            return true;
        }
    }
}