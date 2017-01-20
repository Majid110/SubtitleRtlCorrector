using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SubtitleRtlCorrector.Model;

namespace SubtitleRtlCorrector.Helper
{
    public class AppConfigHelper
    {
        private static string _configFilePath = AppDomain.CurrentDomain.BaseDirectory + "app.cfg";

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
                    SelectedLanguage = "en-US",
                    SpecialChars = ".,،?؟«»()"
                };

            using (TextReader reader = new StreamReader(_configFilePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AppConfig));
                return (AppConfig)serializer.Deserialize(reader);
            }
        }

        private static AppConfig _appConfig;
        public static AppConfig Instance
        {
            get { return _appConfig ?? (_appConfig = ReadConfig()); }
            set { _appConfig = value; }
        }
    }
}