using System.Text.RegularExpressions;

namespace SubtitleCorrectorEngine
{
    public class CorrectorCore
    {
        private readonly string _text;
        private readonly CorrectOption _correctOption;

        public string SpecialChars => _correctOption.SpecialChars ?? Extensions.DefaultSpecialChars;
        public string StickyChars => _correctOption.StickyChars ?? Extensions.DefaultStickyChars;
        public string StartingBracketChars => _correctOption.StartingBracketChars ?? Extensions.DefaultStartingBracketChars;
        public string EndingBracketChars => _correctOption.EndingBracketChars ?? Extensions.DefaultEndingsBracketChars;

        public CorrectorCore(string text, CorrectOption correctOption)
        {
            _text = text;
            _correctOption = correctOption;
        }

        public string Correct()
        {
            if (_correctOption.FileExt == ".ass")
            {
                var pattern = @"(Dialogue: ([^,]*,){9})(.*)";
                var replaced = Regex.Replace(_text, pattern, m => m.Groups[1].Value + correctRtl(m.Groups[3].Value));
                return replaced;
            }
            return correctRtl(_text);
        }

        private string correctRtl(string s)
        {
            if (_correctOption.CorrectSpaces)
            {
                s = s.TrimEnd();
                s = s.RemoveDoubleSpace();
                s = s.RemoveSpacesBeforeStickyChars(StickyChars);
                s = s.AddRequiredSpaceAfterStickyChars(StickyChars);
                s = s.AddRequiredSpaceBeforeStartingBrackets(StartingBracketChars);
                s = s.RemoveSpaceAfterStartingBrackets(StartingBracketChars);
                s = s.RemoveSpaceBeforeEndingBrackets(EndingBracketChars);
                s = s.AddRequiredSpaceAfterEndingBrackets(EndingBracketChars, StickyChars);
            }
            if (_correctOption.CorrectRtl)
            {
                s = s.AddRleToEachNoneAlphabeticChars(SpecialChars);
            }
            return s;
        }

        public string ResetChanges()
        {
            var s = Regex.Replace(_text, @"[\u202B]+", string.Empty); //Removes Right-To-Left Embeding character
            s = Regex.Replace(s, @"[\u200F]+", string.Empty); //Removes Right-To-Left Mark charcter
            return Regex.Replace(s, @"[\u202C]+", string.Empty); //Removes POP DIRECTIONAL FORMATTING (PDF) charcter
        }
    }
}