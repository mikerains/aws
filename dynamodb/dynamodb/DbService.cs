using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;

namespace ConfigService
{
    public class DbService
    {
        public void GetData()
        {
            //AmazonDynamoDBConfig config = new AmazonDynamoDBConfig() { RegionEndpoint = RegionEndpoint.USEast2 };
            //Amazon.Runtime.BasicAWSCredentials cred = new Amazon.Runtime.BasicAWSCredentials("", "");
            //AmazonDynamoDBClient client = new AmazonDynamoDBClient(cred, config);
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();

        }
    }
}
