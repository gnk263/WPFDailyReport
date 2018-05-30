using DailyReport.Common;
using Reactive.Bindings;

namespace DailyReport.Models
{
    public class Report : BindableBase
    {
        public ReactiveProperty<string> OutputPath { get; }
        public ReactiveProperty<string> FileName { get; }
        public ReactiveProperty<string> Title { get; }
        public ReactiveProperty<string> Body { get; }

        private readonly SettingsHelper _settings = SettingsHelper.Current;

        public Report()
        {
            OutputPath = new ReactiveProperty<string>(_settings.OutputPath)
                .SetValidateAttribute(() => OutputPath);

            FileName = new ReactiveProperty<string>(_settings.Name)
                .SetValidateAttribute(() => FileName);

            Title = new ReactiveProperty<string>(_settings.Title)
                .SetValidateAttribute(() => Title);

            Body = new ReactiveProperty<string>(_settings.Body)
                .SetValidateAttribute(() => Body);
        }

        public void SaveTemp()
        {
            _settings.OutputPath = OutputPath.Value;
            _settings.Name = FileName.Value;
            _settings.Title = Title.Value;
            _settings.Body = Body.Value;

            _settings.Save();
        }

        public void SaveStorage()
        {
            
        }
    }
}