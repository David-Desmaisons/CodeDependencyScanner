// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System.Collections.Generic;
using Gma.CodeVisuals.Generator.DependencyForceGraph.Do;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.DependencyGraph
{
    internal class AdjacencyMatrix
    {
        private readonly DependencyKinds[,] m_Matrix;

        public AdjacencyMatrix(int count)
        {
            m_Matrix = new DependencyKinds[count, count];
        }

        public bool Add(int source, int target, DependencyKind kind, out Edge edge)
        {
            return Add(source, target, kind.ToFlags(), out edge);
        }

        public bool Add(int source, int target, DependencyKinds kinds, out Edge edge)
        {
            edge = null;
            var value = m_Matrix[source, target];
            if (value == kinds) return false;
            m_Matrix[source, target] = value | kinds;
            edge = new Edge(source, target, kinds);
            return true;
        }

        public IEnumerable<Edge> All()
        {
            for (int i = 0; i < m_Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m_Matrix.GetLength(1); j++)
                {
                    var value = m_Matrix[i, j];
                    if (value == DependencyKinds.None) continue;
                    var rawEdge = new Edge(i, j, value);
                    foreach(var edge in rawEdge.FlattenFlags())
                    {
                        yield return edge;
                    }
                }
            }
        }
    }
}