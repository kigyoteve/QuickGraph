using QuickGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using QuickGraph.Predicates;
using System.Collections.Generic;

namespace QuickGraph.Tests
{
    
    
    /// <summary>
    ///This is a test class for BidirectionalGraphTest and is intended
    ///to contain all BidirectionalGraphTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BidirectionalGraphTest
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
        public void CloneTest()
        {
            var g = new BidirectionalGraph<int, Edge<int>>();
            g.AddVertexRange(new int[3] {1, 2, 3});
            g.AddEdge(new Edge<int>(1, 2));
            g.AddEdge(new Edge<int>(2, 3));
            g.AddEdge(new Edge<int>(3, 1));

            Assert.AreEqual(3, g.VertexCount);
            Assert.AreEqual(3, g.EdgeCount);

            var h = g.Clone();

            Assert.AreEqual(3, h.VertexCount);
            Assert.AreEqual(3, h.EdgeCount);

            h.AddVertexRange(new int[4] { 10, 11, 12, 13 });
            h.AddEdge(new Edge<int>(10, 11));

            Assert.AreEqual(7, h.VertexCount);
            Assert.AreEqual(4, h.EdgeCount);

            var i = 0;
            foreach (var e in h.Edges)
                i++;

            Assert.AreEqual(4, i);

            Assert.AreEqual(3, g.VertexCount);
            Assert.AreEqual(3, g.EdgeCount);
        }

         [TestMethod()]
        public void LoadGraphFromDot()
         {
             const string dotSource = "digraph { a -> b }";
             var vertexFunc = DotParserAdapter.VertexFactory.Name;
             var edgeFunc = DotParserAdapter.EdgeFactory<string>.VerticesOnly;
             var graph = BidirectionalGraph<string, SEdge<string>>.LoadDot(dotSource, vertexFunc, edgeFunc);
             Assert.IsNotNull(graph);
         }

        [TestMethod()]
        public void removeIsolatedVertices()
        {
            var graph = new BidirectionalGraph<int, IEdge<int>>();
            graph.AddVertex(1);
            var edge = new EquatableEdge<int>(2, 3);
            graph.AddVerticesAndEdge(edge);
            var predicate = new IsolatedVertexPredicate<int, IEdge<int>>(graph);
            graph.RemoveVertexIf(predicate.Test);
            Assert.IsTrue(graph.ContainsVertex(2));
            Assert.IsTrue(graph.ContainsEdge(edge));
            Assert.IsFalse(graph.ContainsVertex(1));
        }

        [TestMethod()]
        public void removeEdgeIfTest()
        {
            var graph = new BidirectionalGraph<int, IEdge<int>>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(new EquatableEdge<int>(1, 2));
            graph.AddEdge(new EquatableEdge<int>(2, 3));
            graph.AddEdge(new EquatableEdge<int>(2, 1));
            graph.RemoveOutEdgeIf(2, (edge) => edge.Target == 1);
            Assert.IsTrue(graph.ContainsEdge(1, 2));
            Assert.IsFalse(graph.ContainsEdge(2, 1));
            Assert.IsTrue(graph.ContainsEdge(2, 3));
            graph.RemoveInEdgeIf(2, (edge) => edge.Source == 1);
            Assert.IsTrue(graph.ContainsEdge(2, 3));
            Assert.IsFalse(graph.ContainsEdge(1, 2));
        }

        [TestMethod()]
        public void isOutEdgeEmptyTest()
        {
            var graph = new BidirectionalGraph<int, IEdge<int>>();
            graph.AddVertex(0);
            Assert.IsTrue(graph.IsOutEdgesEmpty(0));
        }
    }
}
