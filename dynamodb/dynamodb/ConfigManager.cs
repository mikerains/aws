using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Newtonsoft.Json;

namespace ConfigService
{
    public class ConfigManager
    {
        /// <summary>
        /// Use ambient credentials and config
        /// </summary>
        private AmazonDynamoDBClient _adbclient = null;

        public AmazonDynamoDBClient adbclient
        {
            get
            {
                if (null== _adbclient)
                {
                    var c = new BasicAWSCredentials("AKIAIIBSDFX3DQPWQNOA", "5kvy+8olVHQTF0k3/mdxJn0Lw9kPZ7TyLK8Ym0e3");
                    var adbc = new AmazonDynamoDBConfig();
                    adbc.RegionEndpoint = Amazon.RegionEndpoint.USEast2;
                    _adbclient = new AmazonDynamoDBClient(c, adbc);
                }
                return _adbclient;
            }
        }

        /// <summary>
        /// Config table
        /// </summary>
        public const string TABLENAME = "APPCONFIG";

        public const string PARTITION_KEY = "ProjectName";

        public const string SORT_KEY = "ConfigurationKey";

        private Dictionary<string, object> fakeCache = new Dictionary<string, object>();

        public object Get(string ProjectName, string ConfigurationKey)
        {
            object obj = GetFromCache(ProjectName, ConfigurationKey);
            if (null != obj)
            {
                return obj;
            }
            else
            {
                LoadCache(ProjectName);
            }

            obj = GetFromCache(ProjectName, ConfigurationKey);
            if (null != obj)
            {
                return obj;
            }
            else
            {
                throw new Exception($"{ProjectName}:{ConfigurationKey} Does Not Exist");
            }
        }


        public T Get<T>(string ProjectName, string ConfigurationKey)
            where T : class, new()
        {
            object obj = GetFromCache(ProjectName, ConfigurationKey);
            if (null != obj)
            {
                return obj as T;
            }
            else
            {
                LoadCache(ProjectName);
            }

            obj = GetFromCache(ProjectName, ConfigurationKey);
            if (null != obj)
            {
                return obj as T;
            }
            else
            {
                throw new Exception($"{ProjectName}:{ConfigurationKey} Does Not Exist");
            }
        }

        private object GetFromCache(string projectname, string key)
        {
            if (this.fakeCache.Keys.Contains($"{projectname}:{key}"))
            {
                return this.fakeCache[$"{projectname}:{key}"];
            }
            else
            {
                return null;
            }
        }

        private void LoadCacheFromQuery(string ProjectName)
        {
            QueryResponse response = null;
            do
            {
                var request = new QueryRequest
                {
                    TableName = TABLENAME,
                    KeyConditionExpression = "#pn = :v_ProjectName",
                    ExpressionAttributeNames = new Dictionary<string, string>
                {
                    { "#pn", "ProjectName" }
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                    {":v_ProjectName", new AttributeValue { S =  ProjectName }}},
                    ConsistentRead = true,
                    ExclusiveStartKey = response!=null&&response.LastEvaluatedKey.Count>0?response.LastEvaluatedKey:null
                };
                response = adbclient.Query(request);

                foreach (var doc in response.Items)
                {
                    Type type = Type.GetType(doc["ConfigurationType"].S);
                    var obj = JsonConvert.DeserializeObject(doc["Value"].S, type);
                    fakeCache.Add($"{ProjectName}:" + doc["ConfigurationKey"].S, obj);
                }
            } while (response.LastEvaluatedKey!=null&&response.LastEvaluatedKey.Count>0);
        }

        private void LoadCache(string ProjectName)
        {
            // http://docs.aws.amazon.com/amazondynamodb/latest/gettingstartedguide/GettingStarted.NET.04.html
            //see the example All movies released in 1985
            Table table = GetTableObject(adbclient, TABLENAME);
            Search search = table.Query(ProjectName, new Expression());
            List<Document> docList = new List<Document>();
            do
            {
                docList = search.GetNextSet();
                foreach (var doc in docList)
                {
                    Type type = GetTypeEx(doc["ConfigurationType"].AsString());
                    var obj = JsonConvert.DeserializeObject(doc["Value"].AsString(), type);
                    fakeCache.Add($"{ProjectName}:" + doc["ConfigurationKey"].AsString(), obj);
                }
            } while (!search.IsDone);
        }

        private Type GetTypeEx(string fullTypeName)
        {
            return Type.GetType(fullTypeName) ??
                   AppDomain.CurrentDomain.GetAssemblies()
                            .Select(a => a.GetType(fullTypeName))
                            .FirstOrDefault(t => t != null);
        }


        // -------------------
        private Table GetTableObject(AmazonDynamoDBClient client, string tableName)
        {
            Table table = null;
            try
            {
                table = Table.LoadTable(client, tableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n Error: failed to load the 'Movies' table; " + ex.Message);
                return (null);
            }
            return (table);
        }
    }
}
