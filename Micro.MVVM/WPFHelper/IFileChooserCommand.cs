using Neutronium.MVVMComponents;
using System;

namespace Micro.MVVM.WPFHelper
{
    public interface IFileChooserCommand: ISimpleCommand
    {
        Action<string> Setter { get; set; }

        IFilePicker FilePicker { get; }
    }
}
