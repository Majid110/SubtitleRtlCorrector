using NUnit.Framework;
using SubtitleCorrectorEngine;

namespace SubtitleCorrectorTest
{
    [TestFixture]
    public class SubtitleEngineTest
    {

        private string subtitleText =
@"
[V4+ Styles]
Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding
Style: Default,AlHurraTxtlight,80,&H00FFFFFF,&H000000FF,&H00000000,&H00000000,0,0,0,0,100,100,0,0,1,2,2,2,10,10,10,1

[Events]
Format: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text
Dialogue: 0,0:00:02.16,0:00:03.78,Default,,0,0,0,,سلام علیکم.
Dialogue: 0,0:00:05.16,0:00:07.18,Default,,0,0,0,,بسم الله , الرحمن الرحيم
Dialogue: 0,0:00:05.16,0:00:07.18,Default,,0,0,0,,بنام خداوند. شما خوب هستید؟ متشکرم!
";

//        [Test]
//        public void Test_Corrector()
//        {
//            var s = File.ReadAllText(@"D:\test.ass");
//            var core = new CorrectorCore(s);
//            var corrected = core.Correct();
//            File.WriteAllText(@"D:\new.ass", corrected, Encoding.UTF8);
//            Assert.IsNotEmpty(corrected);
//        }

        [Test]
        public void Test_Remove_Space_After_Last_Char()
        {
            Assert.AreEqual("asdf.", CorrectorCore.RemoveSpaceBeforeLastCharacter("asdf."));
            Assert.AreEqual("asdf.", CorrectorCore.RemoveSpaceBeforeLastCharacter("asdf ."));
            Assert.AreEqual("asdf.", CorrectorCore.RemoveSpaceBeforeLastCharacter("asdf  ."));
            Assert.AreEqual("asdf.", CorrectorCore.RemoveSpaceBeforeLastCharacter("asdf   ."));
            Assert.AreEqual("as. df.", CorrectorCore.RemoveSpaceBeforeLastCharacter("as. df ."));
            Assert.AreEqual("as . df.", CorrectorCore.RemoveSpaceBeforeLastCharacter("as . df ."));
            Assert.AreEqual("as  . df.", CorrectorCore.RemoveSpaceBeforeLastCharacter("as  . df  ."));

            Assert.AreEqual("سلام.", CorrectorCore.RemoveSpaceBeforeLastCharacter("سلام."));
            Assert.AreEqual("سلام.", CorrectorCore.RemoveSpaceBeforeLastCharacter("سلام ."));
            Assert.AreEqual("سلام.", CorrectorCore.RemoveSpaceBeforeLastCharacter("سلام  ."));
        }

    }
}
