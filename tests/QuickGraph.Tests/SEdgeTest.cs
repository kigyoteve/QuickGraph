using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Tests {

	[TestClass]
	public class SEdgeTest {

		[TestMethod]
		public void SEdgeTestEverything() {

			var e = new SEdge<int>(0, 1);
			Assert.AreEqual(0, e.Source);
			Assert.AreEqual(1, e.Target);
			Assert.AreEqual(String.Format("{0}->{1}", 0, 1), e.ToString());

		}
	}
}
