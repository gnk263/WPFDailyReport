using System.IO;

namespace DailyReport.Models
{
    public class Storage
    {
        private Report _report;

        public Storage(Report report)
        {
            _report = report;
        }

        public bool Save()
        {
            if (!IsExisDirectory(_report.OutputPath.Value))
            {
                return false;
            }

            var saveDirectoryPath = _report.GetReportSaveDirectoryPath();
            CreateReportDirectory(saveDirectoryPath);

            var filePath = _report.GetReportSaveFilePath();

            using (var sw = new StreamWriter(filePath))
            {
                sw.WriteLine(_report.Title);
                sw.WriteLine(_report.Body);
            }

            return true;
        }

        private bool IsExisDirectory(string path)
        {
            return Directory.Exists(path);
        }

        private void CreateReportDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

    }
}