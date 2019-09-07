using System.Text.RegularExpressions;

namespace SubtitleCorrectorEngine
{
    public static class Extensions
    {
        public static string DefaultSpecialChars = ".,،?؟«»()!-";
        public static string DefaultStickyChars = ".,،?؟:؛!;";
        public static string DefaultStartingBracketChars = "({[<«";
        public static string DefaultEndingsBracketChars = ")}]>»";

        public static string AddRleToEachNoneAlphabeticChars(this string s, string specialChars = null)
        {
            var chars = specialChars ?? DefaultSpecialChars;
            var pattern = $@"([{chars}])";
            var rleChar = (char)0x202B;
            var pdfChar = (char)0x202C;

            var replaced = Regex.Replace(s, pattern, m => string.Format("{0}{1}{2}{0}{1}", pdfChar, rleChar, m.Groups[1].Value));
            replaced = replaced.Replace(@"\\N", @"\\N" + rleChar);

            return rleChar + replaced;
        }

        public static string RemoveDoubleSpace(this string s)
        {
            while (s.Contains("  "))
            {
                s = s.Replace("  ", " ");
            }
            return s;
        }

        public static string RemoveSpacesBeforeStickyChars(this string s, string stickyChars = null)
        {
            var chars = stickyChars ?? DefaultStickyChars;
            var pattern = $@"( )+([{chars}])";
            var replaced = s;
            while (Regex.IsMatch(replaced, pattern))
            {
                replaced = Regex.Replace(replaced, pattern, m => m.Groups[2].Value);
            }
            return replaced;
        }

        public static string AddRequiredSpaceAfterStickyChars(this string s, string stickyChars = null)
        {
            var chars = stickyChars ?? DefaultStickyChars;
            var pattern = $@"([{chars}])([^ {chars}])";
            var replaced = s;
            while (Regex.IsMatch(replaced, pattern))
            {
                replaced = Regex.Replace(replaced, pattern, m => $"{m.Groups[1].Value} {m.Groups[2].Value}");
            }
            return replaced;
        }

        public static string AddRequiredSpaceBeforeStartingBrackets(this string s, string startingBrackets = null)
        {
            var chars = startingBrackets ?? DefaultStartingBracketChars;
            var pattern = $@"([^ {chars}])([{chars}])";
            var replaced = s;
            while (Regex.IsMatch(replaced, pattern))
            {
                replaced = Regex.Replace(replaced, pattern, m => $"{m.Groups[1].Value} {m.Groups[2].Value}");
            }
            return replaced;
        }

        public static string RemoveSpaceAfterStartingBrackets(this string s, string startingBrackets = null)
        {
            var chars = startingBrackets ?? DefaultStartingBracketChars;
            var pattern = $@"([{chars}])([ ])+";
            var replaced = s;
            while (Regex.IsMatch(replaced, pattern))
            {
                replaced = Regex.Replace(replaced, pattern, m => $"{m.Groups[1].Value}");
            }
            return replaced;
        }

        public static string RemoveSpaceBeforeEndingBrackets(this string s, string endingBrackets = null)
        {
            var chars = endingBrackets ?? DefaultEndingsBracketChars;
            chars = chars.Replace("]", @"\]");
            var pattern = $@"([ ])+([{chars}])";
            var replaced = s;
            while (Regex.IsMatch(replaced, pattern))
            {
                replaced = Regex.Replace(replaced, pattern, m => $"{m.Groups[2].Value}");
            }
            return replaced;
        }

        public static string AddRequiredSpaceAfterEndingBrackets(this string s, string endingBrackets = null, string stickyChars = null)
        {
            var chars = endingBrackets ?? DefaultEndingsBracketChars;
            var stickies = stickyChars ?? DefaultStickyChars;
            chars = chars.Replace("]", @"\]");
            var pattern = $@"([{chars}])(?=[^ {stickies}{chars}])";
            var replaced = s;
            while (Regex.IsMatch(replaced, pattern))
            {
                replaced = Regex.Replace(replaced, pattern, m => $"{m.Groups[1].Value} ");
            }
            return replaced;
        }
    }
}