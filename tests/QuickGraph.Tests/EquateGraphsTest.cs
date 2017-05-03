using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Tests
{
    [TestClass]
    public class EquateGraphsTest
    {
        [TestMethod()]
        public void EquateTestNullNull()
        {
            Assert.IsTrue(EquateGraphs.Equate<int,IEdge<int>>(null, null));
        }

        [TestMethod()]
        public void EquateTestGraphNull()
        {
            var g = new BidirectionalGraph<int, IEdge<int>>();
            Assert.IsFalse(EquateGraphs.Equate<int, IEdge<int>>(g, null));
        }

        [TestMethod()]
        public void EquateTestReferenceEquals()
        {
            var g = new BidirectionalGraph<int, IEdge<int>>();
            Assert.IsTrue(EquateGraphs.Equate<int, IEdge<int>>(g, g));
        }

        [TestMethod()]
        public void EquateTestVertexCount()
        {
            var g = new BidirectionalGraph<int, IEdge<int>>();
            var f = new BidirectionalGraph<int, IEdge<int>>();
            Assert.IsTrue(EquateGraphs.Equate<int, IEdge<int>>(g, f));
            f.AddVertex(1);
            Assert.IsFalse(EquateGraphs.Equate<int, IEdge<int>>(g, f));
        }

        [TestMethod()]
        public void EquateTestEdgeCount()
        {
            var g = new BidirectionalGraph<int, IEdge<int>>();
            var f = new BidirectionalGraph<int, IEdge<int>>();
            g.AddVertex(1);
            g.AddVertex(2);
            f.AddVertex(1);
            f.AddVertex(2);
            Assert.IsTrue(EquateGraphs.Equate<int, IEdge<int>>(g, f));
            g.AddEdge(new EquatableEdge<int>(1, 2));
            Assert.IsFalse(EquateGraphs.Equate<int, IEdge<int>>(g, f));
        }

        [TestMethod()]
        public void EquateTestDifferentVertex()
        {
            var g = new BidirectionalGraph<int, IEdge<int>>();
            var f = new BidirectionalGraph<int, IEdge<int>>();
            g.AddVertex(1);
            f.AddVertex(1);
            Assert.IsTrue(EquateGraphs.Equate<int, IEdge<int>>(g, f));
            g.AddVertex(2);
            f.AddVertex(3);
            Assert.IsFalse(EquateGraphs.Equate<int, IEdge<int>>(g, f));
        }

        [TestMethod()]
        public void EquateTestDifferentEdge()
        {
            var g = new BidirectionalGraph<int, IEdge<int>>();
            var f = new BidirectionalGraph<int, IEdge<int>>();
            g.AddVertex(1);
            f.AddVertex(1);
            g.AddVertex(2);
            f.AddVertex(2);
            g.AddVertex(3);
            f.AddVertex(3);
            g.AddEdge(new EquatableEdge<int>(1, 2));
            f.AddEdge(new EquatableEdge<int>(1, 2));
            Assert.IsTrue(EquateGraphs.Equate<int, IEdge<int>>(g, f));
            g.AddEdge(new EquatableEdge<int>(3, 2));
            f.AddEdge(new EquatableEdge<int>(3, 1));
            Assert.IsFalse(EquateGraphs.Equate<int, IEdge<int>>(g, f));
        }
    }
}
