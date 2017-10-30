using System;

namespace Micro.MVVM.WPFHelper
{
    public class FileChooserCommand : IFileChooserCommand
    {
        public Action<string> Setter { get; set; }
        public IFilePicker FilePicker  { get;}
        private bool _IsRunning;

        public FileChooserCommand(IFilePicker filePicker)
        {
            FilePicker = filePicker;
        }

        public async void Execute()
        {
            if (_IsRunning)
                return;

            _IsRunning = true;
            var res = await FilePicker.ChooseFile();
            Setter?.Invoke(res);
            _IsRunning = false;
        }
    }
}
