using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using ConfigService;

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
            ConfigManager cm = new ConfigManager();
            var raw = cm.Get("Project1", "Key1");
            Assert.IsNotNull(raw);
            Rank rank = raw as Rank;
            Assert.IsNotNull(rank, raw.GetType().Name + "is not of Type Rank");
            Assert.AreEqual(rank.Title, "Captain");
        }

        [Test]
        public void CreateTable()
        {
            // Arrange

            TableManager tm = new TableManager();
            tm.CreateConfigTable();
        }
    }
}
