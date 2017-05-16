using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;

namespace dynamodb
{
    public class ConfigManager
    {
        /// <summary>
        /// Use ambient credentials and config
        /// </summary>
        private AmazonDynamoDBClient adbclient = new AmazonDynamoDBClient();

        /// <summary>
        /// Config table
        /// </summary>
        public const string TABLENAME = "APPCONFIG";

        public const string PARTITION_KEY = "ProjectName";

        public const string SORT_KEY = "ConfigurationKey";

        private Dictionary<string, object> fakeCache = new Dictionary<string, object>();


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
            } else
            {
                return null;
            }
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
                    Type type = Type.GetType(doc["ConfigurationType"]);
                    var obj = JsonConvert.DeserializeObject(doc["Value"], type);
                    fakeCache.Add($"{ProjectName}:" + doc["ConfigurationKey"], obj);
                }
            } while (!search.IsDone);
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
