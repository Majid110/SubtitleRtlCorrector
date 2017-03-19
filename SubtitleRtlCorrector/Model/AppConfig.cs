using System.Xml.Serialization;

namespace SubtitleRtlCorrector.Model
{
    [XmlRoot("AppConfig")]
    public class AppConfig
    {
        public static string CurrentVersion = "1.1";

        [XmlElement("AppVersion")]
        public string AppVersion { get; set; }
        [XmlElement("SpecialChars")]
        public string SpecialChars { get; set; }
        [XmlElement("SelectedLanguage")]
        public string SelectedLanguage { get; set; }
        [XmlElement("StickyChars")]
        public string StickyChars { get; set; }
        [XmlElement("StartingBracketChars")]
        public string StartingBracketChars { get; set; }
        [XmlElement("EndingBracketChars")]
        public string EndingBracketChars { get; set; }
    }
}