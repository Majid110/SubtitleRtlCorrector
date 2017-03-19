using NUnit.Framework;
using SubtitleCorrectorEngine;
using Extensions = SubtitleCorrectorEngine.Extensions;

namespace SubtitleCorrectorTest
{
    [TestFixture]
    public class SubtitleEngineTest
    {

        [Test]
        public void Test_Remove_Double_Spaces()
        {
            Assert.AreEqual("asdf .", "asdf  .".RemoveDoubleSpace());
            Assert.AreEqual("asdf .", "asdf   .".RemoveDoubleSpace());
            Assert.AreEqual("asdf .", "asdf    .".RemoveDoubleSpace());
            Assert.AreEqual(" asdf.", "  asdf.".RemoveDoubleSpace());
            Assert.AreEqual(" asdf.", "   asdf.".RemoveDoubleSpace());
            Assert.AreEqual(" asdf. ", "  asdf. ".RemoveDoubleSpace());
            Assert.AreEqual(" asdf. ", "  asdf.  ".RemoveDoubleSpace());
            Assert.AreEqual(" as df. ", "  as  df.  ".RemoveDoubleSpace());
            Assert.AreEqual(" as df. ", "  as   df.  ".RemoveDoubleSpace());
        }

        [Test]
        public void Test_Remove_Space_Before_Sticky_Char()
        {
            Assert.AreEqual("This is a test.", "This is a test .".RemoveSpacesBeforeStickyChars());
            Assert.AreEqual("This is a test.", "This is a test  .".RemoveSpacesBeforeStickyChars());
            Assert.AreEqual("This is a test.", "This is a test   .".RemoveSpacesBeforeStickyChars());
            Assert.AreEqual("This, is a test.", "This , is a test   .".RemoveSpacesBeforeStickyChars());
            Assert.AreEqual("This, is a test.", "This  , is a test.".RemoveSpacesBeforeStickyChars());
            Assert.AreEqual(".,،?؟:؛!;", " . , ، ? ؟ : ؛ ! ;".RemoveSpacesBeforeStickyChars());
        }

        [Test]
        public void Test_Add_Space_After_Sticky_Char()
        {
            Assert.AreEqual("This, is a test.", "This,is a test.".AddRequiredSpaceAfterStickyChars());
            Assert.AreEqual("This, is; a test;", "This,is;a test;".AddRequiredSpaceAfterStickyChars());
            Assert.AreEqual("This, is; a، test;", "This,is;a،test;".AddRequiredSpaceAfterStickyChars());
            Assert.AreEqual(". , ، ? ؟ : ؛ ! ;", ".,،?؟:؛!;".AddRequiredSpaceAfterStickyChars());
        }

        [Test]
        public void Test_Add_Space_Before_Starting_Brackets()
        {
            Assert.AreEqual("This is (test ).", "This is(test ).".AddRequiredSpaceBeforeStartingBrackets());
            Assert.AreEqual("This is (test).", "This is(test).".AddRequiredSpaceBeforeStartingBrackets());
            Assert.AreEqual("This [is] {for} ((test)).", "This[is]{for}((test)).".AddRequiredSpaceBeforeStartingBrackets());
            Assert.AreEqual("({[<«", "({[<«".AddRequiredSpaceBeforeStartingBrackets());
        }

        [Test]
        public void Test_Remove_Space_After_Starting_Brackets()
        {
            Assert.AreEqual("This is (test ).", "This is ( test ).".RemoveSpaceAfterStartingBrackets());
            Assert.AreEqual("This is(test).", "This is( test).".RemoveSpaceAfterStartingBrackets());
            Assert.AreEqual("This is ((test)).", "This is ( (test)).".RemoveSpaceAfterStartingBrackets());
            Assert.AreEqual("({[<«", "( { [ < « ".RemoveSpaceAfterStartingBrackets());
        }

        [Test]
        public void Test_Remove_Space_Before_Ending_Brackets()
        {
            Assert.AreEqual("This is (test).", "This is (test ).".RemoveSpaceBeforeEndingBrackets());
            Assert.AreEqual("This is(test).", "This is(test ).".RemoveSpaceBeforeEndingBrackets());
            Assert.AreEqual("This is ((test)).", "This is ((test ) ).".RemoveSpaceBeforeEndingBrackets());
            Assert.AreEqual(")}]>»", " ) } ] > »".RemoveSpaceBeforeEndingBrackets());
        }

        [Test]
        public void Test_Add_Space_After_Ending_Brackets()
        {
            Assert.AreEqual("This [is] (test).", "This [is](test).".AddRequiredSpaceAfterEndingBrackets());
            Assert.AreEqual("This[is] (test).", "This[is](test).".AddRequiredSpaceAfterEndingBrackets());
            Assert.AreEqual("This [is] {for} ((test)).", "This [is]{for}((test)).".AddRequiredSpaceAfterEndingBrackets());
            Assert.AreEqual(")}]>»", ")}]>»".AddRequiredSpaceAfterEndingBrackets());
        }
    }
}
