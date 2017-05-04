using System;
using System.Linq;
using QuickGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Predicates;

namespace QuickGraph.Tests
{
    /// <summary>
    ///This is a test class for UndirectedGraph and is intended
    ///to contain all UndirectedGraph Unit Tests
    ///</summary>
    [TestClass()]
    public class UndirectedGraphTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void ContainsEdgeTest1()
        {
            var u = new UndirectedGraph<int, IEdge<int>>();
            var e12 = new SEquatableUndirectedEdge<int>(1, 2);
            var f12 = new SEquatableUndirectedEdge<int>(1, 2);

            bool exceptionOccurred = false;
            try
            {
                new SEquatableUndirectedEdge<int>(2, 1);
            }
            catch (ArgumentException e)
            {
                exceptionOccurred = true;
            }
            Assert.IsTrue(exceptionOccurred);

            u.AddVerticesAndEdge(e12);

            ContainsEdgeAssertions(u, e12, f12, null, null);
        }

        [TestMethod()]
        public void ContainsEdgeTest2()
        {
            var u = new UndirectedGraph<int, IEdge<int>>();
            var e12 = new EquatableEdge<int>(1, 2);
            var f12 = new EquatableEdge<int>(1, 2);
            var e21 = new EquatableEdge<int>(2, 1);
            var f21 = new EquatableEdge<int>(2, 1);

            u.AddVerticesAndEdge(e12);

            ContainsEdgeAssertions(u, e12, f12, e21, f21);
        }

        public static void ContainsEdgeAssertions(IUndirectedGraph<int, IEdge<int>> g,
            IEdge<int> e12,
            IEdge<int> f12,
            IEdge<int> e21,
            IEdge<int> f21)
        {
            Assert.AreEqual(1, g.AdjacentDegree(1));
            Assert.AreEqual(1, g.AdjacentDegree(2));
            Assert.AreEqual(1, g.AdjacentEdges(1).Count());
            Assert.AreEqual(1, g.AdjacentEdges(2).Count());

            // e12 must be present in u, because we added it.
            Assert.IsTrue(g.ContainsEdge(e12));

            // f12 is also in u, because e12 == f12.
            Assert.IsTrue(g.ContainsEdge(f12));

            // e21 and f21 are not in u, because ContainsEdge has semantics that 
            // if it returns true for an edge, that edge must be physically present in 
            // the collection of edges inside u.
            if (e21 != null) Assert.IsFalse(g.ContainsEdge(e21));
            if (f21 != null) Assert.IsFalse(g.ContainsEdge(f21));

            // there must be an edge between vertices 1, 2.
            Assert.IsTrue(g.ContainsEdge(1, 2));

            // there is also an edge between vertices 2, 1, because the graph is undirected.
            Assert.IsTrue(g.ContainsEdge(2, 1));

            // obviously no edge between vertices 1, 3, as vertex 3 is not even present in the graph.
            Assert.IsFalse(g.ContainsEdge(1, 3));
        }

        [TestMethod()]
        public void removeIsolatedVertices()
        {
            var graph = new UndirectedGraph<int, IEdge<int>>();
            graph.AddVertex(1);
            var edge = new EquatableEdge<int>(2, 3);
            graph.AddVerticesAndEdge(edge);
            graph.RemoveVertexIf(graph.IsAdjacentEdgesEmpty);
            Assert.IsTrue(graph.ContainsVertex(2));
            Assert.IsTrue(graph.ContainsEdge(edge));
            Assert.IsTrue(graph.ContainsEdge(2, 3));
            Assert.IsTrue(graph.ContainsEdge(3, 2));
            Assert.IsFalse(graph.ContainsVertex(1));
        }

        [TestMethod()]
        public void toBidirectionalGraphTest()
        {
            var graph = new UndirectedGraph<int, IEdge<int>>();
            graph.AddVertex(1);
            var edge = new EquatableEdge<int>(2, 3);
            graph.AddVerticesAndEdge(edge);
            var bidirectionalGraph = graph.ToBidirectionalGraph();
            Assert.IsTrue(bidirectionalGraph.ContainsVertex(1));
            Assert.IsTrue(bidirectionalGraph.ContainsVertex(2));
            Assert.IsTrue(bidirectionalGraph.ContainsVertex(3));
            Assert.IsTrue(bidirectionalGraph.ContainsEdge(2, 3));
            Assert.IsFalse(bidirectionalGraph.ContainsEdge(3, 2));
        }

        [TestMethod()]
        public void removeAdjacentEdgeIfTest()
        {
            var graph = new UndirectedGraph<int, IEdge<int>>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(new EquatableEdge<int>(1, 2));
            graph.AddEdge(new EquatableEdge<int>(1, 3));
            graph.RemoveAdjacentEdgeIf(1, (edge) => edge.Target == 2);
            Assert.IsTrue(graph.ContainsEdge(1, 3));
            Assert.IsFalse(graph.ContainsEdge(1, 2));
        }

        [TestMethod()]
        public void clearTest()
        {
            var graph = new UndirectedGraph<int, IEdge<int>>();
            graph.AddVerticesAndEdge(new EquatableEdge<int>(1, 2));
            graph.AddVerticesAndEdge(new EquatableEdge<int>(2, 3));
            Assert.AreEqual(graph.EdgeCount, 2);
            Assert.AreEqual(graph.VertexCount, 3);
            Assert.IsFalse(graph.IsEdgesEmpty);
            Assert.IsFalse(graph.IsVerticesEmpty);
            graph.Clear();
            Assert.AreEqual(graph.EdgeCount, 0);
            Assert.AreEqual(graph.VertexCount, 0);
            Assert.IsTrue(graph.IsEdgesEmpty);
            Assert.IsTrue(graph.IsVerticesEmpty);
        }

        [TestMethod()]
        public void IsVerticesEmptyTest()
        {
            var g = new UndirectedGraph<int, IEdge<int>>();
            Assert.IsTrue(g.IsVerticesEmpty);
        }

        [TestMethod]
        public void RemoveVertexIfTest()
        {
            var g  = new UndirectedGraph<int, IEdge<int>>();
            g.AddVertex(0);
            g.AddVertex(1);
            int result = g.RemoveVertexIf((vertex) => true);
            Assert.AreEqual(result, 2);
        }
    }
}
