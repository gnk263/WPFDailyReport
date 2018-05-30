using DailyReport.Properties;

namespace DailyReport.Models
{
    public class SettingsHelper
    {
        public static SettingsHelper Current { get; } = new SettingsHelper();

        public string OutputPath { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        private readonly Settings _settings = Settings.Default;


        private SettingsHelper()
        {
            Load();
        }

        public void Load()
        {
            _settings.Reload();

            OutputPath = _settings.OutputPath;
            Name = _settings.Name;
            Title = _settings.Title;
            Body = _settings.Body;
        }

        public void Save()
        {
            _settings.OutputPath = OutputPath;
            _settings.Name = Name;
            _settings.Title = Title;
            _settings.Body = Body;

            _settings.Save();
        }
    }
}