namespace CodeDependencyScanner.ViewModel.Infra
{
    public class ProgressViewModel : Vm.Tools.ViewModel
    {
        private int _Current;
        public int Current
        {
            get { return _Current; }
            set { Set(ref _Current, value); }
        }

        private int _Total;
        public int Total
        {
            get { return _Total; }
            set { Set(ref _Total, value); }
        }

        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { Set(ref _Message, value); }
        }
    }
}
