using BrigandineGEDataEditor.DataTypes;
using BrigandineGEDataEditor.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrigandineGEDataEditorTests
{
    [TestClass]
    public class ElementExtensionMethodsTests
    {
        [TestMethod] //[Timeout(2000)]
        public void GetElements_Decompose_Properly()
        {
            var elementsValue = (ElementEnum) 16;
            var elements = elementsValue.GetElements();

            Assert.AreEqual(2, elements.Black);
        }

        [TestMethod]
        [Timeout(2000)]
        public void GetElements_Decompose_Properly2()
        {
            var elementsValue = (ElementEnum) 9;
            var elements = elementsValue.GetElements();

            Assert.AreEqual(1, elements.Black);
            Assert.AreEqual(1, elements.White);
        }
    }
}