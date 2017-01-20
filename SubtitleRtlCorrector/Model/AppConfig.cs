using System.Xml.Serialization;

namespace SubtitleRtlCorrector.Model
{
    [XmlRoot("AppConfig")]
    public class AppConfig
    {
        [XmlElement("SpecialChars")]
        public string SpecialChars { get; set; }
        [XmlElement("SelectedLanguage")]
        public string SelectedLanguage { get; set; }
    }
}