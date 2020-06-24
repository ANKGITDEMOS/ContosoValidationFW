using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using ContosoValidationFW;
using ContosoValidationFW.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace ContosoValidationFW.Tests
{
    [TestClass]
    public class EDSValidator_Tests
    {

        ILogger _logger;

        [TestInitialize]
        public void TestInitialize()
        {

            var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("",LogLevel.Information)
                    .AddConsole();
            });
            _logger = loggerFactory.CreateLogger<EDSValidator_Tests>();
        }

        [TestMethod]
        public void ValidateNodesTest()
        {
            string json = File.ReadAllText(@"ValidateJSon.json");
            EDSValidator validator = new EDSValidator( _logger);
            var result = validator.ValidateNodes(json);
            Assert.IsTrue(result.NodeExecutionTimeResult.Count() == 5, "Validation passed.");
        }


    }
}
