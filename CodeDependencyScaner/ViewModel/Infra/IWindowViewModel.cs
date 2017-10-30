using Neutronium.MVVMComponents;
using System;

namespace CodeVizualization.ViewModel
{
    public interface IWindowViewModel : IDisposable
    {
        ISimpleCommand Close { get; }
        ISimpleCommand Minimize { get; }
        ISimpleCommand Maximize { get; }
        ISimpleCommand Normalize { get; }
    }
}