using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using DailyReport.Common;
using DailyReport.Models;
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

        private readonly SettingsHelper _settings = SettingsHelper.Current;

        public MainWindowViewModel()
        {
            OutputPath = new ReactiveProperty<string>(_settings.OutputPath)
                .SetValidateAttribute(() => OutputPath);

            Name = new ReactiveProperty<string>(_settings.Name)
                .SetValidateAttribute(() => Name);

            Title = new ReactiveProperty<string>(_settings.Title)
                .SetValidateAttribute(() => Title);

            Body = new ReactiveProperty<string>(_settings.Body)
                .SetValidateAttribute(() => Body);

            SaveTempCommand = new ReactiveCommand();
            SaveTempCommand.Subscribe(() =>
            {
                _settings.OutputPath = OutputPath.Value;
                _settings.Name = Name.Value;
                _settings.Title = Title.Value;
                _settings.Body = Body.Value;

                _settings.Save();
            });

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