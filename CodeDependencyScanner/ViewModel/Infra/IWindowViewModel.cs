using System;
using Neutronium.MVVMComponents;

namespace CodeDependencyScanner.ViewModel.Infra
{
    public interface IWindowViewModel : IDisposable
    {
        ISimpleCommand Close { get; }
        ISimpleCommand Minimize { get; }
        ISimpleCommand Maximize { get; }
        ISimpleCommand Normalize { get; }
    }
}