using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Tests
{
    [TestClass]
    public class EdgeListGraphTest
    {
        [TestMethod()]
        public void EdgeListGraphTestProperty()
        {
            var g = new EdgeListGraph<int, IEdge<int>>();
            Assert.IsTrue(g.IsDirected);
            Assert.IsTrue(g.AllowParallelEdges);
            Assert.IsTrue(g.IsEdgesEmpty);
            Assert.IsTrue(g.IsVerticesEmpty);
            Assert.AreEqual(g.VertexCount, 0);
            Assert.AreEqual(g.EdgeCount, 0);
            var h = new EdgeListGraph<int, IEdge<int>>(false,false);
            Assert.IsFalse(h.IsDirected);
            Assert.IsFalse(h.AllowParallelEdges);
            h.AddVerticesAndEdge(new EquatableEdge<int>(1, 2));
            Assert.IsFalse(h.IsEdgesEmpty);
            Assert.IsFalse(h.IsVerticesEmpty);
            Assert.AreEqual(h.VertexCount, 2);
            Assert.AreEqual(h.EdgeCount, 1);
            var hv = h.Vertices.ToList<int>();
            Assert.AreEqual(hv[0], 1);
            Assert.AreEqual(hv[1], 2);
            var he = h.Edges.ToList<IEdge<int>>();
            Assert.AreEqual(he[0], new EquatableEdge<int>(1, 2));
        }

        [TestMethod()]
        public void EdgeListGraphTestAddStuff()
        {
            var g = new EdgeListGraph<int, IEdge<int>>();
            g.AddEdge(new Edge<int>(0, 1));
            Assert.AreEqual(2, g.VertexCount);
            Assert.AreEqual(1, g.EdgeCount);
            g.AddEdge(new Edge<int>(0, 1));
            Assert.AreEqual(2, g.VertexCount);
            Assert.AreEqual(2, g.EdgeCount);
            g.AddEdge(new EquatableEdge<int>(0, 2));
            Assert.AreEqual(3, g.VertexCount);
            Assert.AreEqual(3, g.EdgeCount);
            g.AddVerticesAndEdge(new Edge<int>(3, 4));
            Assert.AreEqual(5, g.VertexCount);
            Assert.AreEqual(4, g.EdgeCount);
            g.AddEdgeRange(new List<IEdge<int>> { new Edge<int>(4, 5) });
            Assert.AreEqual(6, g.VertexCount);
            Assert.AreEqual(5, g.EdgeCount);
            g.AddVerticesAndEdgeRange(new List<IEdge<int>> { new Edge<int>(4, 5) });
            Assert.AreEqual(6, g.VertexCount);
            Assert.AreEqual(6, g.EdgeCount);
        }

        [TestMethod()]
        public void EdgeListGraphTestAddStuffNoParalel()
        {
            var g = new EdgeListGraph<int, IEdge<int>>(true,false);
            g.AddEdge(new Edge<int>(0, 1));
            Assert.AreEqual(2, g.VertexCount);
            Assert.AreEqual(1, g.EdgeCount);
            g.AddEdge(new Edge<int>(0, 1));
            Assert.AreEqual(2, g.VertexCount);
            Assert.AreEqual(1, g.EdgeCount);
        }

        [TestMethod()]
        public void EdgeListGraphTestAddStuffNoParalelEquateable()
        {
            var g = new EdgeListGraph<int, EquatableEdge<int>>(true, false);
            g.AddEdge(new EquatableEdge<int>(0, 1));
            Assert.AreEqual(2, g.VertexCount);
            Assert.AreEqual(1, g.EdgeCount);
            g.AddEdge(new EquatableEdge<int>(0, 1));
            Assert.AreEqual(2, g.VertexCount);
            Assert.AreEqual(1, g.EdgeCount);
        }

        [TestMethod()]
        public void EdgeListGraphTestContains()
        {
            var g = new EdgeListGraph<int, IEdge<int>>(true, false);
            var e = new Edge<int>(0, 1);
            g.AddEdge(e);
            Assert.IsTrue(g.ContainsEdge(e));
            Assert.IsTrue(g.ContainsEdge(new Edge<int>(0, 1)));
            Assert.IsFalse(g.ContainsEdge(new Edge<int>(2, 1)));
            Assert.IsTrue(g.ContainsVertex(0));
            Assert.IsTrue(g.ContainsVertex(1));
            Assert.IsFalse(g.ContainsVertex(2));
        }

        [TestMethod()]
        public void EdgeListGraphTestContainsEquateable()
        {
            var g = new EdgeListGraph<int, EquatableEdge<int>>(true, false);
            g.AddEdge(new EquatableEdge<int>(0, 1));
            Assert.IsTrue(g.ContainsEdge(new EquatableEdge<int>(0, 1)));
            Assert.IsFalse(g.ContainsEdge(new EquatableEdge<int>(2, 1)));
            Assert.IsTrue(g.ContainsVertex(0));
            Assert.IsTrue(g.ContainsVertex(1));
            Assert.IsFalse(g.ContainsVertex(2));
        }
    }
}
