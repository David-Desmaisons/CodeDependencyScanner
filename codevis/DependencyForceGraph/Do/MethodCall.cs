// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Reflection;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph.Do
{
    public class MethodCall : Dependency
    {
        private readonly MethodBase m_Method;

        public MethodCall(Type source, Type target, MethodBase method)
            : base(source, target)
        {
            m_Method = method;
        }

        public MethodBase Method => m_Method;

        public override DependencyKind Kind => DependencyKind.MethodCall;

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ m_Method.GetHashCode();
        }

        public override bool Equals(object other)
        {
            var otherMethodCall = other as MethodCall;
            return otherMethodCall != null &&
                   otherMethodCall.m_Method == m_Method &&
                   base.Equals(other);
        }

        public override string ToString()
        {
            return $"{Source.Name,20}=[{Method.Name,20}]=>{Target.Name,-20}";
        }
    }
}