using Gma.CodeVisuals.Generator.DependencyForceGraph.Do;

namespace Gma.CodeVisuals.Generator.Api
{
    public class AnalyseResult
    {
        public Graph Graph { get; }
        public string AssemblyName { get; }

        public AnalyseResult(Graph graph, string name)
        {
            Graph = graph;
            AssemblyName = name;
        }
    }
}
