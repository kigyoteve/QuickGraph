using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Tests {

	[TestClass]
	public class SReversedEdgeTest {

		[TestMethod]
		public void SReversedEdgeTestEverything() {

			var originalEdge1 = new SEquatableEdge<int>(0, 1);
			var reversedEdge1 = new SReversedEdge<int, SEquatableEdge<int>>(originalEdge1);
			Assert.AreEqual(1, reversedEdge1.Source);
			Assert.AreEqual(0, reversedEdge1.Target);
			Assert.AreEqual(String.Format("R({0}->{1})", 0, 1), reversedEdge1.ToString());

			//equatable parts
			var originalEdge2 = new SEquatableEdge<int>(2, 3);
			var reversedEdge2 = new SReversedEdge<int, SEquatableEdge<int>>(originalEdge2);
			Assert.IsFalse(reversedEdge1.Equals(reversedEdge2));
			Assert.IsFalse(reversedEdge1.Equals(1));
			Assert.IsFalse(reversedEdge1 == reversedEdge2);
			Assert.IsTrue(reversedEdge1 != reversedEdge2);
			Assert.IsFalse(reversedEdge1.GetHashCode() == reversedEdge2.GetHashCode());

		}
	}
}
