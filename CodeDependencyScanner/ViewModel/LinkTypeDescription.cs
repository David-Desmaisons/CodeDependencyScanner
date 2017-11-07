using Gma.CodeVisuals.Generator.DependencyForceGraph.DataStructures.DependencyGraph;

namespace CodeDependencyScanner.ViewModel
{
    public class LinkTypeDescription
    {
        private readonly DependencyKinds _Kind;

        public int id => (int)_Kind;
        public string name => _Kind.ToString();
        public string inName { get; }
        public string outName { get; }

        public LinkTypeDescription(DependencyKinds kind,string inName, string outName )
        {
            _Kind = kind;
            this.inName = inName;
            this.outName = outName;
        }

        public static LinkTypeDescription[] Descriptions { get; } = 
        {
            new LinkTypeDescription(DependencyKinds.Contains, "contains", "is contained by"),
            new LinkTypeDescription(DependencyKinds.Implements, "implements", "is implemented by"),
            new LinkTypeDescription(DependencyKinds.MethodCall, "calls", "is called by"),
            new LinkTypeDescription(DependencyKinds.Uses, "uses", "is used by")
        };
    }
}