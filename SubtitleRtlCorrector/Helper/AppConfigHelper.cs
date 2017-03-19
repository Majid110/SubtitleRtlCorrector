using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SubtitleCorrectorEngine;
using SubtitleRtlCorrector.Model;

namespace SubtitleRtlCorrector.Helper
{
    public class AppConfigHelper
    {
        private static readonly string _configFilePath = AppDomain.CurrentDomain.BaseDirectory + "app.cfg";

        public static void SaveConfig(AppConfig config)
        {
            var xsSubmit = new XmlSerializer(typeof(AppConfig));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, config);
                    var xml = sww.ToString(); // Your XML
                    File.WriteAllText(_configFilePath, xml, Encoding.UTF8);
                }
            }
        }

        public static AppConfig ReadConfig()
        {
            if (!File.Exists(_configFilePath))
                return new AppConfig()
                {
                    AppVersion = AppConfig.CurrentVersion,
                    SelectedLanguage = "en-US",
                    SpecialChars = Extensions.DefaultSpecialChars,
                    StickyChars = Extensions.DefaultStickyChars,
                    StartingBracketChars = Extensions.DefaultStartingBracketChars,
                    EndingBracketChars = Extensions.DefaultEndingsBracketChars
                };

            using (TextReader reader = new StreamReader(_configFilePath))
            {
                var serializer = new XmlSerializer(typeof(AppConfig));
                var config = (AppConfig)serializer.Deserialize(reader);
                return updateConfigurationIfNeeded(config);
            }
        }

        private static AppConfig updateConfigurationIfNeeded(AppConfig config)
        {
            if (string.IsNullOrEmpty(config.AppVersion) && AppConfig.CurrentVersion == "1.1")
            {
                config.AppVersion = AppConfig.CurrentVersion;
                config.StickyChars = Extensions.DefaultStickyChars;
                config.StartingBracketChars = Extensions.DefaultStartingBracketChars;
                config.EndingBracketChars = Extensions.DefaultEndingsBracketChars;
                if (!config.StickyChars.Contains("!"))
                {
                    config.SpecialChars += "!";
                }
                if (!config.StickyChars.Contains("-"))
                {
                    config.SpecialChars += "-";
                }
            }
            return config;
        }

        private static AppConfig _appConfig;
        public static AppConfig Instance
        {
            get { return _appConfig ?? (_appConfig = ReadConfig()); }
            set { _appConfig = value; }
        }
    }
}