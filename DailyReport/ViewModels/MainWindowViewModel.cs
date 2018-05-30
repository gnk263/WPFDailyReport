using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using DailyReport.Common;
using Reactive.Bindings;

namespace DailyReport.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        [Required(ErrorMessage = "格納場所を入力してください。")]
        public ReactiveProperty<string> OutputPath { get; }

        [Required(ErrorMessage = "名前を入力してください。")]
        public ReactiveProperty<string> Name { get; }

        [Required(ErrorMessage = "タイトルを入力してください。")]
        public ReactiveProperty<string> Title { get; }

        [Required(ErrorMessage = "内容を入力してください。")]
        public ReactiveProperty<string> Body { get; }

        public ReactiveCommand SaveTempCommand { get; }
        public ReactiveCommand SaveCommand { get; }

        public MainWindowViewModel()
        {
            OutputPath = new ReactiveProperty<string>()
                .SetValidateAttribute(() => OutputPath);

            Name = new ReactiveProperty<string>()
                .SetValidateAttribute(() => Name);

            Title = new ReactiveProperty<string>()
                .SetValidateAttribute(() => Title);

            Body = new ReactiveProperty<string>()
                .SetValidateAttribute(() => Body);

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