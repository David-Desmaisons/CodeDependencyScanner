using Gma.CodeVisuals.Generator.Api;
using Gma.CodeVisuals.Generator.DependencyForceGraph;
using Neutronium.MVVMComponents;
using System;
using System.Windows.Input;
using CodeDependencyScanner.ViewModel.Infra;
using Vm.Tools.Dialog.FileChooser;
using Vm.Tools.Async;
using Neutronium.WPF.ViewModel;

namespace CodeDependencyScanner.ViewModel
{
    public class MainViewModel : Vm.Tools.ViewModel 
    {
        private readonly FileChooserCommand _FilePicker;
        private readonly IGraphBuilder _IGraphBuilder;
        private readonly TaskCommand<AnalyseResult, AnalyzesProgress> _GraphLoader;

        public IWindowViewModel Window { get; }

        private string _Path;
        public string Path
        {
            get { return _Path; }
            set
            {
                if (Set(ref _Path, value))
                    _GraphLoader.CanBeExecuted = !string.IsNullOrEmpty(value);
            }
        }

        private GraphViewModel _Graph;
        public GraphViewModel Graph
        {
            get { return _Graph; }
            set { Set(ref _Graph, value); }
        }

        private bool _Computing;
        public bool Computing
        {
            get { return _Computing; }
            set 
            {
                Set(ref _Computing, value);
            }
        }

        private ProgressViewModel _Progress = new ProgressViewModel();
        public ProgressViewModel Progress 
        {
            get { return _Progress; }
            set { Set(ref _Progress, value); }
        }

        private Message _Message = null;
        public Message Message
        {
            get { return _Message; }
            set { Set(ref _Message, value); }
        }

        public ICommandWithoutParameter ChooseFile => _FilePicker;
        public ICommand Compute => _GraphLoader.Run;
        public ISimpleCommand Cancel => _GraphLoader.Cancel;
        public IGraphBuilder IGraphBuilder => _IGraphBuilder;

        public MainViewModel(IGraphBuilder builder, FileChooserCommand filePicker, IWindowViewModel window)
        {
            Window = window;
            _IGraphBuilder = builder;
            _FilePicker = filePicker;
            _FilePicker.Results.Subscribe(path => Path = path.Result);
            _FilePicker.Picker.Extensions = new [] { ".dll", ".exe" };          
            _FilePicker.Picker.ExtensionDescription = "C# assembly";

            _GraphLoader = new TaskCommand<AnalyseResult, AnalyzesProgress>((cancellationToken, progress) => _IGraphBuilder.GenerateFromAssembly(Path, cancellationToken, progress))
            {
                CanBeExecuted = false
            };
            _GraphLoader.Progress.Subscribe(OnProgress);
            _GraphLoader.Results.Subscribe(OnGraphResult);
        }

        private void OnProgress(AnalyzesProgress progress) 
        {
            _Progress.Total  = progress.Max;
            _Progress.Current = progress.Actual;
            _Progress.Message = progress.Message;
            Computing = true;
        }

        private void OnGraphResult(CommandResult<AnalyseResult> result)
        {
            Computing = false;
            if (result.Success)
            {
                Graph = new GraphViewModel(result.Result, LinkTypeDescription.Descriptions);
                return;
            }

            if (result.HasError)
            {
                Message = Message.Error("Error during import", $"Unhandled exception: {result.Exception.Message}");
            }
        }
    }
}
