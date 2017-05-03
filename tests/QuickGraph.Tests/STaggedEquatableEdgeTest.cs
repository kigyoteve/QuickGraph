using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Tests {

	[TestClass]
	public class STaggedEquatableEdgeTest {

		[TestMethod()]
		public void STaggedEquatableEdgeTestEverything() {
			//normal parts
			var e1 = new STaggedEquatableEdge<int, int>(0, 1, 5);
			Assert.AreEqual(0, e1.Source);
			Assert.AreEqual(1, e1.Target);
			Assert.AreEqual(5, e1.Tag);
			Assert.AreEqual(String.Format("{0}->{1}:{2}", 0, 1, 5), e1.ToString());
			bool eventHappened = false;
			e1.TagChanged += (a, b) => { eventHappened = true; };
			e1.Tag = 5;
			Assert.IsFalse(eventHappened);
			e1.Tag = 6;
			Assert.IsTrue(eventHappened);

			//equatable parts
			var e2 = new STaggedEquatableEdge<int, int>(0, 1, 7);
			Assert.IsFalse(e1.Equals(e2));
			Assert.IsFalse(e1.Equals(1));
			Assert.IsFalse(e1 == e2);
			Assert.IsTrue(e1 != e2);
			Assert.IsFalse(e1.GetHashCode() == e2.GetHashCode());
		}
	}
}
