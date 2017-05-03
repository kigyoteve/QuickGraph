using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace QuickGraph.Tests
{
    [TestClass]
    public class BidirectionalMatrixGraphTest
    {
        private BidirectionalMatrixGraph<IEdge<int>> getTestGraph()
        {
            var graph = new BidirectionalMatrixGraph<IEdge<int>>(4);
            var list = new List<IEdge<int>>();
            list.Add(new EquatableEdge<int>(0, 1));
            list.Add(new EquatableEdge<int>(1, 2));
            list.Add(new EquatableEdge<int>(1, 0));
            list.Add(new EquatableEdge<int>(2, 3));
            graph.AddEdgeRange(list);
            return graph;
        }

        [TestMethod]
        public void testBasicProperties()
        {
            var graph = getTestGraph();
            Assert.IsTrue(graph.ContainsVertex(1));
            Assert.IsFalse(graph.ContainsVertex(4));
            Assert.IsTrue(graph.ContainsEdge(1, 2));
            Assert.IsFalse(graph.ContainsEdge(2, 1));
            Assert.IsFalse(graph.ContainsEdge(0, 3));
            Assert.AreEqual(graph.EdgeCount, 4);
            Assert.AreEqual(graph.VertexCount, 4);
            Assert.AreEqual(graph.OutDegree(0), 1);
            Assert.AreEqual(graph.InDegree(0), 1);
            Assert.AreEqual(graph.Degree(0), 2);
            Assert.IsFalse(graph.IsEdgesEmpty);
            Assert.IsFalse(graph.IsVerticesEmpty);
            Assert.IsFalse(graph.IsInEdgesEmpty(3));
            Assert.IsTrue(graph.IsOutEdgesEmpty(3));
        }

        [TestMethod()]
        public void testRemoveEdge()
        {
            var graph = getTestGraph();

            graph.RemoveEdge(new EquatableEdge<int>(1, 2));
            Assert.IsFalse(graph.ContainsEdge(1, 2));

            graph.ClearEdges(1);
            Assert.IsFalse(graph.ContainsEdge(1, 0));
            Assert.IsFalse(graph.ContainsEdge(0, 1));
            Assert.IsTrue(graph.IsInEdgesEmpty(1));
            Assert.IsTrue(graph.IsOutEdgesEmpty(1));

            graph.RemoveInEdgeIf(3, (edge) => false);
            Assert.IsTrue(graph.ContainsEdge(2, 3));

            graph.RemoveOutEdgeIf(2, (edge) => true);
            Assert.IsFalse(graph.ContainsEdge(2, 3));
        }

        [TestMethod()]
        public void testGraphClear()
        {
            var graph = getTestGraph();
            graph.Clear();
            Assert.AreEqual(graph.EdgeCount, 0);
            Assert.AreEqual(graph.VertexCount, 4);
            Assert.IsTrue(graph.IsEdgesEmpty);
            Assert.IsFalse(graph.IsVerticesEmpty);
        }

        [TestMethod()]
        public void clearEdgesTest()
        {
            var graph = getTestGraph();
            graph.ClearInEdges(3);
            Assert.IsFalse(graph.ContainsEdge(2, 3));
            Assert.IsTrue(graph.ContainsEdge(1, 2));
            graph.ClearOutEdges(1);
            Assert.IsFalse(graph.ContainsEdge(1, 0));
            Assert.IsFalse(graph.ContainsEdge(1, 2));
            Assert.IsTrue(graph.ContainsEdge(0, 1));
        }

        [TestMethod()]
        public void removeEdgeInstanceTest()
        {
            var graph = getTestGraph();
            bool result = graph.RemoveEdge(new EquatableEdge<int>(1, 2));
            Assert.IsTrue(result);
            Assert.IsFalse(graph.ContainsEdge(1, 2));
        }

        [TestMethod()]
        public void tryGetInEdgesTest()
        {
            var graph = getTestGraph();
            IEnumerable<IEdge<int>> list;
            bool result = graph.TryGetInEdges(6, out list);
            Assert.IsFalse(result);
            Assert.IsNull(list);

            result = graph.TryGetInEdges(1, out list);
            Assert.IsTrue(result);
            var enumerator = list.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(enumerator.Current.Source, 0);
            Assert.AreEqual(enumerator.Current.Target, 1);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod()]
        public void tryGetEdgesTest()
        {
            var graph = getTestGraph();
            IEnumerable<IEdge<int>> list;
            bool result = graph.TryGetEdges(0, 2, out list);
            Assert.IsFalse(result);
            Assert.IsNull(list);

            result = graph.TryGetEdges(0, 1, out list);
            Assert.IsTrue(result);
            var enumerator = list.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(enumerator.Current.Source, 0);
            Assert.AreEqual(enumerator.Current.Target, 1);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod()]
        public void tryGetOutEdgesTest()
        {
            var graph = getTestGraph();
            IEnumerable<IEdge<int>> list;
            bool result = graph.TryGetOutEdges(6, out list);
            Assert.IsFalse(result);
            Assert.IsNull(list);

            result = graph.TryGetOutEdges(2, out list);
            Assert.IsTrue(result);
            var enumerator = list.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(enumerator.Current.Source, 2);
            Assert.AreEqual(enumerator.Current.Target, 3);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}
