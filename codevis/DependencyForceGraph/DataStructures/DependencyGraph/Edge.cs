// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.DependencyGraph
{
    public class Edge
    {
        private readonly DependencyKinds m_Kinds;
        private readonly int m_Source;
        private readonly int m_Target;

        public Edge(int source, int target, DependencyKinds kinds)
        {
            m_Source = source;
            m_Target = target;
            m_Kinds = kinds;
        }

        public int source => m_Source;

        public int target => m_Target;

        public int type => (int)kinds;

        internal DependencyKinds kinds => m_Kinds;

        public IEnumerable<Edge> FlattenFlags()
        {
            return kinds.Flatten().Select(kind => new Edge(source, target, kind));
        }

        protected bool Equals(Edge other)
        {
            return m_Source == other.m_Source && m_Target == other.m_Target && m_Kinds == other.m_Kinds;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Edge) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = m_Source;
                hashCode = (hashCode*397) ^ m_Target;
                hashCode = (hashCode*397) ^ (int) m_Kinds;
                return hashCode;
            }
        }
    }
}