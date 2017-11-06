using System;
using System.Windows;
using CodeDependencyScanner.ViewModel;
using CodeDependencyScanner.ViewModel.Infra;
using Gma.CodeVisuals.Generator.Api;
using Neutronium.WPF.ViewModel;
using Vm.Tools.Dialog;

namespace CodeDependencyScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitDataContext();
        }

        private void InitDataContext()
        {
            var command = new ChooserCommand<IFilePicker>(new FilePicker());
            var builder = new GraphBuilder();
            var window = new WindowViewModel(this);
            DataContext = new MainViewModel(builder, command, window);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.HtmlView.Dispose();
        }
    }
}
