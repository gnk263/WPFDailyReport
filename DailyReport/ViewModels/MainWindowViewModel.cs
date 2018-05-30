using System.Linq;
using System.Reactive.Linq;
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
            OutputPath = new ReactiveProperty<string>();
            Name = new ReactiveProperty<string>();
            Title = new ReactiveProperty<string>();
            Body = new ReactiveProperty<string>();

            SaveTempCommand = new ReactiveCommand();

            SaveCommand = new[]
                {
                    OutputPath.ObserveHasErrors,
                    Name.ObserveHasErrors,
                    Title.ObserveHasErrors,
                    Body.ObserveHasErrors
                }
                .CombineLatest(x => x.All(y => !y))
                .ToReactiveCommand();
        }
    }
}