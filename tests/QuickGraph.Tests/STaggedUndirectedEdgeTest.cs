using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Tests {

	[TestClass]
	public class STaggedUndirectedEdgeTest {

		[TestMethod()]
		public void STaggedUndirectedeEdgeTestEverything() {
			//normal parts
			var e1 = new STaggedUndirectedEdge<int, int>(0, 1, 5);
			Assert.AreEqual(0, e1.Source);
			Assert.AreEqual(1, e1.Target);
			Assert.AreEqual(5, e1.Tag);
			Assert.AreEqual(String.Format("{0}<->{1}:{2}", 0, 1, 5), e1.ToString());
			bool eventHappened = false;
			e1.TagChanged += (a, b) => { eventHappened = true; };
			e1.Tag = 5;
			Assert.IsFalse(eventHappened);
			e1.Tag = 6;
			Assert.IsTrue(eventHappened);
		}
	}
}
