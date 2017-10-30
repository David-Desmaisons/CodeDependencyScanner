// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.DependencyGraph
{
    [Flags]
    public enum DependencyKinds
    {
        None = 0,
        MethodCall = 1,
        Uses = 2,
        Implements = 4,
        Contains = 8
    }
}