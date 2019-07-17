using System;
using System.Linq;
using Memory_Map_Builder.Helper_Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memory_Map_Builder_Tests
{
    [TestClass]
    public class ElementExtensionMethodsTests
    {
        [TestMethod]//[Timeout(2000)]
        public void GetElements_Decompose_Properly()
        {
            var elementsValue = (Element) 16;
            var elements = elementsValue.GetElements();

            Assert.AreEqual(2, elements.Black);
        }
        [TestMethod][Timeout(2000)]
        public void GetElements_Decompose_Properly2()
        {
            var elementsValue = (Element) 9;
            var elements      = elementsValue.GetElements();

            Assert.AreEqual(1, elements.Black);
            Assert.AreEqual(1, elements.White);
        }
    }
}
