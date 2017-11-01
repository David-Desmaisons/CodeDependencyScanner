// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.DependencyGraph;
using Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.TypeTree;
using Gma.CodeVisuals.Generator.DependencyForceGraph.Do;
using System.Threading;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph
{
    internal class Analyzer
    {
        private readonly Action<AnalyzesProgress> m_OnProgres;
        private AdjacencyMatrix m_Matrix;
        private Tree m_Tree;

        public Analyzer()
            : this(null)
        {
        }

        public Analyzer(Action<AnalyzesProgress> onProgres)
        {
            m_OnProgres = onProgres;
            m_Tree = new Tree(new NodeFactory(), string.Empty);
            Progress = AnalyzesProgress.Started();
        }

        public AnalyzesProgress Progress { get; private set; }

        public Graph GetGraph()
        {
            var tree = m_Tree.Root;
            var links = m_Matrix.All();
            var graph =new Graph(tree, links);
            return graph;
        }

        public Graph Analyze(IEnumerable<Assembly> assemblies, string rootName, CancellationToken cancellationToken)
        {
            var types = assemblies
                .SelectMany(a => a.Types())
                .ToArray();
            Analyze(types, rootName, cancellationToken);
            return GetGraph();
        }

        private void Analyze(Type[] types, string rootName, CancellationToken cancellationToken)
        {
            m_Tree = BuildTree(types, rootName);
            var nodesById = m_Tree.Nodes.Reverse().ToArray();
            m_Matrix = new AdjacencyMatrix(nodesById.Length);
            m_OnProgres?.Invoke(new AnalyzesProgress($"{types.Length} types found.", 0, 1, false));
            if (types.Length == 0) return;
            DoAnalyzeTasks(types, cancellationToken);
        }

        private static Tree BuildTree(IEnumerable<Type> types, string rootName)
        {
            var nodeFactory = new NodeFactory();
            var tree = new Tree(nodeFactory, rootName);

            foreach (var type in types)
            {
                var fullName = type.FullName;
                tree.Add(fullName);
            }
            return tree;
        }

        private void DoAnalyzeTasks(Type[] types, CancellationToken cancellationToken)
        {
            DoAnalyzeTask(types, AnalyzerExtensions.Uses, "Analyzing uses", cancellationToken);
            DoAnalyzeTask(types, AnalyzerExtensions.Implements, "Analyzing implements", cancellationToken);
            DoAnalyzeTask(types, AnalyzerExtensions.Contains, "Analyzing contain", cancellationToken);
            DoAnalyzeTask(types, AnalyzerExtensions.Calls, "Analyzing calls", cancellationToken);
        }


        private void DoAnalyzeTask(Type[] types, Func<Type, IEnumerable<Dependency>> getter, string taskName, CancellationToken cancellationToken)
        {
            for (int i = 0; i < types.Length; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var type = types[i];
                foreach (var dependecy in getter(type))
                {
                    Edge edge;
                    Add(dependecy, out edge);
                }
                m_OnProgres?.Invoke(new AnalyzesProgress(taskName, i, types.Length, false));
            }
            m_OnProgres?.Invoke(new AnalyzesProgress(taskName, types.Length, types.Length, false));
        }

        private void Add(Dependency dependency, out Edge edge)
        {
            edge = null;
            INode source;
            bool sourceFound = m_Tree.TryGet(dependency.Source.FullName, out source);
            if (!sourceFound) return;

            INode target;
            bool targetFound = m_Tree.TryGet(dependency.Target.FullName, out target);
            if (!targetFound) return;

            m_Matrix.Add(source.id, target.id, dependency.Kind, out edge);
        }
    }
}