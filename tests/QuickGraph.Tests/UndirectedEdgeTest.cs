using System;
using System.Linq;
using QuickGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Tests
{
    [TestClass()]
    public class UndirectedEdgeTest
    {
        [TestMethod]
        public void toStringTest()
        {
            UndirectedEdge<int> ue = new UndirectedEdge<int>(0, 1);
            string result = ue.ToString();
            Assert.AreEqual("0<->1", result);
        }
    }
}
