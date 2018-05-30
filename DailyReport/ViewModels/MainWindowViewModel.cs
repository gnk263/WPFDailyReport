using DailyReport.Common;
using Reactive.Bindings;

namespace DailyReport.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ReactiveProperty<string> OutputPath { get; }
        public ReactiveProperty<string> Name { get; }
        public ReactiveProperty<string> Title { get; }
        public ReactiveProperty<string> Body { get; }

        public ReactiveCommand SaveTempCommand { get; }
        public ReactiveCommand SaveCommand { get; }

        public MainWindowViewModel()
        {
            
        }
    }
}