using Gma.CodeVisuals.Generator.DependencyForceGraph;
using Gma.CodeVisuals.Generator.DependencyForceGraph.Do;
using System;
using System.Threading;

namespace Gma.CodeVisuals.Generator.Api
{
    public interface IGraphBuilder
    {
        AnalyseResult GenerateFromAssembly(string assemblyPath, CancellationToken cancellationToken, IProgress<AnalyzesProgress> progress);
    }
}
