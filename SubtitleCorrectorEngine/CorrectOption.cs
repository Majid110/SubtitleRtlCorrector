namespace SubtitleCorrectorEngine
{
    public class CorrectOption
    {
        public string SpecialChars { get; set; }
        public string StickyChars { get; set; }
        public string StartingBracketChars { get; set; }
        public string EndingBracketChars { get; set; }
        public string FileExt { get; set; }
        public bool CorrectRtl { get; set; }
        public bool CorrectSpaces { get; set; }
    }
}