// <copyright file="EdgeTVertexTest.cs" company="Jonathan de Halleux">Copyright http://quickgraph.codeplex.com/</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;

namespace QuickGraph
{
    /// <summary>This class contains parameterized unit tests for Edge`1</summary>
    [TestClass]
    [PexClass(typeof(Edge<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class EdgeTVertexTest
    {
        /// <summary>Test stub for .ctor(!0, !0)</summary>
        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public Edge<TVertex> Constructor<TVertex>(TVertex source, TVertex target)
        {
            Edge<TVertex> target01 = new Edge<TVertex>(source, target);
            Assert.Equals(target01.Source, source);
            Assert.Equals(target01.Target, target);
            return target01;
        }

        /// <summary>Test stub for Source</summary>
        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public void SourceGet<TVertex>([PexAssumeUnderTest]Edge<TVertex> target)
        {
            TVertex result = target.Source;
            Assert.Equals(result, target.Source);
        }

        /// <summary>Test stub for Target</summary>
        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public void TargetGet<TVertex>([PexAssumeUnderTest]Edge<TVertex> target)
        {
            TVertex result = target.Target;
            Assert.Equals(result, target.Target);
        }

        /// <summary>Test stub for ToString()</summary>
        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public string ToString<TVertex>([PexAssumeUnderTest]Edge<TVertex> target)
        { 
            TVertex source = target.Source;
            TVertex target2 = target.Target;
            string s1 = source.ToString();
            string s2 = target2.ToString();
            string result = target.ToString();
            Assert.Equals(s1 + "->" + s2, result);
            return result;
        }
    }
}
