using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace ConfigService
{
    /// <summary>
    /// This would not be deployed to production. 
    /// Used by prototype to create table and document the schema being investigated.
    /// </summary>
    public class TableManager
    {

        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        /// <summary>
        /// This would not be deployed to production. 
        /// Used by prototype to create table and document the schema being investigated.
        /// </summary>
        public void CreateConfigTable()
        {
            Console.WriteLine("\n*** Creating table ***");
            var request = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>()
            {
                new AttributeDefinition
                {
                    AttributeName = ConfigManager.PARTITION_KEY,
                    AttributeType = "S"
                },
                new AttributeDefinition
                {
                    AttributeName = ConfigManager.SORT_KEY,
                    AttributeType = "S"
                }
            },
                KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = ConfigManager.PARTITION_KEY,
                    KeyType = "HASH" //Partition key
                },
                new KeySchemaElement
                {
                    AttributeName = ConfigManager.SORT_KEY,
                    KeyType = "RANGE" //Sort key
                }
            },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                },
                TableName = ConfigManager.TABLENAME
            };

            var response = client.CreateTable(request);

            var tableDescription = response.TableDescription;
            Console.WriteLine("{1}: {0} \t ReadsPerSec: {2} \t WritesPerSec: {3}",
                      tableDescription.TableStatus,
                      tableDescription.TableName,
                      tableDescription.ProvisionedThroughput.ReadCapacityUnits,
                      tableDescription.ProvisionedThroughput.WriteCapacityUnits);

            string status = tableDescription.TableStatus;
            Console.WriteLine(ConfigManager.TABLENAME + " - " + status);

            WaitUntilTableReady(ConfigManager.TABLENAME);
        }

        private static void WaitUntilTableReady(string tableName)
        {
            string status = null;
            // Let us wait until table is created. Call DescribeTable.
            do
            {
                System.Threading.Thread.Sleep(5000); // Wait 5 seconds.
                try
                {
                    var res = client.DescribeTable(new DescribeTableRequest
                    {
                        TableName = tableName
                    });

                    Console.WriteLine("Table name: {0}, status: {1}",
                              res.Table.TableName,
                              res.Table.TableStatus);
                    status = res.Table.TableStatus;
                }
                catch (ResourceNotFoundException)
                {
                    // DescribeTable is eventually consistent. So you might
                    // get resource not found. So we handle the potential exception.
                }
            } while (status != "ACTIVE");
        }
    }
}
