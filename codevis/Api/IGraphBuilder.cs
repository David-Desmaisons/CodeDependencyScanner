using Gma.CodeVisuals.Generator.DependencyForceGraph;
using System;
using System.Threading;

namespace Gma.CodeVisuals.Generator.Api
{
    public interface IGraphBuilder
    {
        AnalyseResult GenerateFromAssembly(string assemblyPath, CancellationToken cancellationToken, IProgress<AnalyzesProgress> progress);
    }
}
