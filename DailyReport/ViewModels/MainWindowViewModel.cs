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
        public ReactiveProperty<string> FileName { get; }

        [Required(ErrorMessage = "タイトルを入力してください。")]
        public ReactiveProperty<string> Title { get; }

        [Required(ErrorMessage = "内容を入力してください。")]
        public ReactiveProperty<string> Body { get; }

        public ReactiveCommand SaveTempCommand { get; }
        public ReactiveCommand SaveCommand { get; }

        private readonly Report _report = new Report();

        public MainWindowViewModel()
        {
            OutputPath = _report.OutputPath;
            OutputPath.SetValidateAttribute(() => OutputPath);

            FileName = _report.FileName;
            FileName.SetValidateAttribute(() => FileName);

            Title = _report.Title;
            Title.SetValidateAttribute(() => Title);

            Body = _report.Body;
            Body.SetValidateAttribute(() => Body);

            SaveTempCommand = new ReactiveCommand();
            SaveTempCommand.Subscribe(() =>
            {
                _report.SaveTemp();
            });

            SaveCommand = new[]
                {
                    OutputPath.ObserveHasErrors,
                    FileName.ObserveHasErrors,
                    Title.ObserveHasErrors,
                    Body.ObserveHasErrors
                }
                .CombineLatest(x => x.All(y => !y))
                .ToReactiveCommand();
            SaveTempCommand.Subscribe(() =>
            {
                _report.SaveStorage();
            });
        }
    }
}