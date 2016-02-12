using System;
using System.Collections.Generic;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate.Dialect.Function;

namespace FinancialThing.Utilities.Test
{
    [TestClass]
    public class RatioEvaluatorTest
    {
        private Mock<IDatabaseRepository<Ratio, Guid>> mockDatabase;
        private List<Data> data;

        [TestInitialize]
        public void Setup()
        {
            mockDatabase = new Mock<IDatabaseRepository<Ratio, Guid>>();
            data = new List<Data>()
            {
                new Data()
                {
                    Dictionary = new Dictionary()
                    {
                        Code = "CORT"
                    },
                    Values = new HashSet<Value>()
                    {
                        new Value()
                        {
                            Year = "2015",
                            DataValue = "10.2"
                        },
                        new Value()
                        {
                            Year = "2014",
                            DataValue = "9"
                        }
                    }
                },
                new Data()
                {
                    Dictionary = new Dictionary()
                    {
                        Code = "SGAAET"
                    },
                    Values = new HashSet<Value>()
                    {
                        new Value()
                        {
                            Year = "2015",
                            DataValue = "14.8"
                        },
                        new Value()
                        {
                            Year = "2014",
                            DataValue = "15"
                        }
                    }
                },
                new Data()
                {
                    Dictionary = new Dictionary()
                    {
                        Code = "DAM"
                    },
                    Values = new HashSet<Value>()
                    {
                        new Value()
                        {
                            Year = "2015",
                            DataValue = "2"
                        },
                        new Value()
                        {
                            Year = "2014",
                            DataValue = "5"
                        }
                    }
                }
            };
        }

        [TestMethod]
        public void TestSimple()
        {
            var formula = "(10.2 + 14.8)*2";
            var ratio = new Ratio()
            {
                Formula = formula
            };
            var ratioEvaluator = new RatioEvaluator(mockDatabase.Object);

            var result = ratioEvaluator.Evaluate(data, ratio);
            Assert.AreEqual(50, result);
        }

        [TestMethod]
        public void TestEvaluate()
        {
            var formula = "(Get([CORT], 0) + Get([SGAAET], 0))*Get([DAM], 0)";
            var ratio = new Ratio()
            {
                Formula = formula
            };

            var ratioEvaluator = new RatioEvaluator(mockDatabase.Object);

            var result = ratioEvaluator.Evaluate(data, ratio);

            Assert.AreEqual(50, result);
        }

        [TestMethod]
        public void TestEvaluateAVG()
        {
            var formula = "Get([CORT], 1)";
            var ratio = new Ratio()
            {
                Formula = formula
            };
            var ratioEvaluator = new RatioEvaluator(mockDatabase.Object);
            var result = ratioEvaluator.Evaluate(data, ratio);
            Assert.AreEqual(9.6.ToString(), result.ToString());
        }


    }
}