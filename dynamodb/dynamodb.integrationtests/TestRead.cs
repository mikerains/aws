using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;

namespace dynamodb.integrationtests
{
    [TestFixture]
    public class TestRead
    {
        private AmazonDynamoDBClient ddbclient = null;

        [SetUp]
        public void Setup()
        {
            ddbclient = new AmazonDynamoDBClient();

        }


        [Test]
        public void TestMethod()
        {
            // Arrange

            Assert.Pass("Your first passing test");
        }
    }
}
