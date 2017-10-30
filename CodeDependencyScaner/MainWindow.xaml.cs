using CodeVizualization.ViewModel;
using System.Windows;
using System;
using Micro.MVVM.WPFHelper;
using Gma.CodeVisuals.Generator.Api;

namespace CodeVizualization
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
            var command = new FileChooserCommand(new FilePicker());
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
