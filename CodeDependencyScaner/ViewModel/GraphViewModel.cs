using Gma.CodeVisuals.Generator.Api;
using Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.DependencyGraph;
using Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.TypeTree;
using System.Collections.Generic;

namespace CodeVizualization.ViewModel
{
    public class GraphViewModel
    {
        public GraphViewModel(AnalyseResult analyseResult, LinkTypeDescription[] linksType)
        {
            var graph = analyseResult.Graph;
            tree = graph.tree;
            links = graph.links;
            this.linksType = linksType;
            AssemblyName = analyseResult.AssemblyName;
        }

        public INode tree { get; }

        public IEnumerable<Edge> links { get; }

        public LinkTypeDescription[] linksType { get; }

        public string AssemblyName { get; }
    }
}
