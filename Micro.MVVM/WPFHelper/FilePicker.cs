using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Micro.MVVM.WPFHelper
{
    public class FilePicker : IFilePicker
    {
        public string Directory { get; set; }
        public string ExtensionDescription { get; set; }
        public string[] Extensions { get; set; }

        public Task<string> ChooseFile()
        {
            var tcs = new TaskCompletionSource<string>();
            Task.Run(() => ShowDialog(tcs));
            return tcs.Task;
        }

        private void ShowDialog(TaskCompletionSource<string> tcs)
        {
            var dlg = new OpenFileDialog
            {
                InitialDirectory = Directory
            };

            if (Extensions?.Length > 0)
            {
                dlg.DefaultExt = Extensions[0];
                var files = String.Join("; ", Extensions.Select(ext => $"*{ext}"));
                dlg.Filter = $"{ExtensionDescription} ({files})|{files}";
            }
            dlg.FileOk += (o, e) => Dlg_FileOk(o as OpenFileDialog, e, tcs);
            dlg.ShowDialog();
            tcs.TrySetResult(null);
        }

        private void Dlg_FileOk(OpenFileDialog dia, CancelEventArgs e, TaskCompletionSource<string> tcs)
        {
            tcs.TrySetResult(dia.FileName);
        }
    }
}
