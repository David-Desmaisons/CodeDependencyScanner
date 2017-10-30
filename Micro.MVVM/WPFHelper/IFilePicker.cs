using System.Threading.Tasks;

namespace Micro.MVVM.WPFHelper
{
    public interface IFilePicker
    {
        string Directory { get; set; }
        string[] Extensions { get; set; }
        string ExtensionDescription { get; set; }

        Task<string> ChooseFile();
    }
}
