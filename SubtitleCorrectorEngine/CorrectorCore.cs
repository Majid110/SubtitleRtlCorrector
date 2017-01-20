using System.Text.RegularExpressions;

namespace SubtitleCorrectorEngine
{
    public class CorrectorCore
    {
        public static string DefaultSpecialChars = ".,،?؟«»()";
        private readonly string _fileText;
        private readonly string _fileExt;
        private readonly string _specialChars;

        public CorrectorCore(string fileText, string specialChars = null, string fileExt = null)
        {
            _fileText = fileText;
            _fileExt = fileExt;
            _specialChars = specialChars ?? DefaultSpecialChars;
        }

        public string Correct()
        {
            if (_fileExt == ".ass")
            {
                var pattern = @"(Dialogue: ([^,]*,){9})(.*)";
                var replaced = Regex.Replace(_fileText, pattern, m => m.Groups[1].Value + correctRtl(m.Groups[3].Value));
                return replaced;
            }
            else
            {
                return correctRtl(_fileText);
            }
        }

        private string correctRtl(string s)
        {
            s = s.TrimEnd();
            s = RemoveSpaceBeforeLastCharacter(s, _specialChars);
            s = AddRleToEachNoneAlphabeticChars(s, _specialChars);
            return s;
        }

        public string ResetChanges()
        {
            return Regex.Replace(_fileText, @"[\u202B]+", string.Empty);
        }

        public static string RemoveSpaceBeforeLastCharacter(string s, string specialChars = null)
        {
            var chars = specialChars ?? DefaultSpecialChars;
            var pattern = string.Format(@"(\s+)([{0}]$)", chars);
            var regex = new Regex(pattern);
            var replacement = "$2";
            return regex.Replace(s, replacement);
        }

        public static string AddRleToEachNoneAlphabeticChars(string s, string specialChars = null)
        {
            var chars = specialChars ?? DefaultSpecialChars;
            var pattern = string.Format(@"([{0}])", chars);
            var rleChar = (char)0x202B;

            var replaced = Regex.Replace(s, pattern, m => rleChar + m.Groups[1].Value);
            return replaced;
        }
    }
}