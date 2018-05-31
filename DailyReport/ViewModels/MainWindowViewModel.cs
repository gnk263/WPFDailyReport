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

        [Required(ErrorMessage = "フォルダパスを入力してください。")]
        public ReactiveProperty<string> OutputPath { get; }

        [Required(ErrorMessage = "ファイル名を入力してください。")]
        public ReactiveProperty<string> FileName { get; }
        
        public ReadOnlyReactiveProperty<string> OutputFilePath { get; }

        [Required(ErrorMessage = "タイトルを入力してください。")]
        public ReactiveProperty<string> Title { get; }

        [Required(ErrorMessage = "内容を入力してください。")]
        public ReactiveProperty<string> Body { get; }

        public ReactiveProperty<string> Message { get; }

        public ReactiveCommand SaveTempCommand { get; }
        public ReactiveCommand SaveCommand { get; }

        private readonly Report _report = new Report();

        public MainWindowViewModel()
        {
            OutputPath = _report.OutputPath;
            OutputPath.SetValidateAttribute(() => OutputPath);

            FileName = _report.FileName;
            FileName.SetValidateAttribute(() => FileName);

            OutputFilePath = _report.OutputFilePath;

            Title = _report.Title;
            Title.SetValidateAttribute(() => Title);

            Body = _report.Body;
            Body.SetValidateAttribute(() => Body);

            Message = _report.Message;

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
            SaveCommand.Subscribe(() =>
            {
                _report.SaveStorage();
            });
        }
    }
}