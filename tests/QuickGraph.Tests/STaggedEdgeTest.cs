using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Tests
{
    [TestClass]
    public class STaggedEdgeTest
    {
        [TestMethod()]
        public void STaggedEdgeTestEverything()
        {
            var e = new STaggedEdge<int, int>(0,1,5);
            Assert.AreEqual(0, e.Source);
            Assert.AreEqual(1, e.Target);
            Assert.AreEqual(5, e.Tag);
            Assert.AreEqual(String.Format("{0}->{1}:{2}", 0, 1, 5), e.ToString());
            bool eventHappened = false;
            e.TagChanged += (a,b) => { eventHappened = true; };
            e.Tag = 5;
            Assert.IsFalse(eventHappened);
            e.Tag = 1511;
            Assert.IsTrue(eventHappened);
        }
    }
}
