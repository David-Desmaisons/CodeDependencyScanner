// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph.Do
{
    public class Contains : Dependency
    {
        public Contains(Type source, Type nested)
            : base(source, nested)
        {
        }

        public override DependencyKind Kind
        {
            get { return DependencyKind.Contains; }
        }
    }
}