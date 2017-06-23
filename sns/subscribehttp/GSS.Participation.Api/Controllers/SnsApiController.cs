using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AU = Amazon.SimpleNotificationService.Util;
using AM = Amazon.SimpleNotificationService.Model;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using NLog.AWS.Logger;
using NLog.Config;
using NLog;
using NLog.Targets;
using AWS.Logger.Core;
using System.Web.Http.Cors;
using System.Text;
using System.Web;

namespace GSS.Participation.Api.Controllers
{
    /// <summary>
    /// http://docs.aws.amazon.com/sns/latest/dg/welcome.html
    /// 
    /// </summary>
    /// 
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SnsApiController : ApiController
    {
        public IHttpActionResult GetTest(string name)
        {
            try
            {
                ConfigureNLog();
            } catch (Exception ex)
            {
                return BadRequest("ERROR DURING NLOG CONFIG " + ex.ToString());
            }

            try
            {
                NLog.LogManager.GetLogger("aws").Info($"Test NLog message for GetTest with name {name}");
            } catch (Exception ex)
            {
                return BadRequest("GOT EXCEPTION DURING GetCurrentClassLogger, ToString=" + ex.ToString());
            }

            try
            {
                NLog.LogManager.GetLogger("aws").Info($"Test NLog message for GetTest with name {name}");
            }
            catch (Exception ex)
            {
                return BadRequest("GOT EXCEPTION DURING GetLogger, ToString=" + ex.ToString());
            }

            try
            {
                Console.WriteLine("GetTest writing to console!");
                TraceSource ts = new TraceSource("Amazon");
                ts.TraceInformation($"GetTest was called witg data {name}", name);
                ts.Flush();
                ts.Close();

                var endpoint = ConfigurationManager.AppSettings["SnsEndpoint"];
                var topicarn = ConfigurationManager.AppSettings["SnsEventsTopicArn"];
                return Ok("HELLO " + name + " arn=" + topicarn + " endpoint=" + endpoint);
            } catch (Exception ex)
            {
                return BadRequest("ERROR DURING FINAL PHASE " + ex.ToString());
            }

            
        }
        public IHttpActionResult GetSubscription()
        {
            try
            {
                ConfigureNLog();
                NLog.LogManager.GetLogger("aws").Info("GetSubscription Entered");
                var endpoint = ConfigurationManager.AppSettings["SnsEndpoint"];
                var topicarn = ConfigurationManager.AppSettings["SnsEventsTopicArn"];
                var client = new Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceClient();
                var request = new AM.SubscribeRequest
                {
                    Protocol = "https",
                    Endpoint = endpoint,
                    TopicArn = topicarn
                };
                var response = client.Subscribe(request);
                NLog.LogManager.GetLogger("aws").Info("GetSubscription returned response SubscriptionArn=" + response.SubscriptionArn);
                return Ok(response.SubscriptionArn); //TODO - stuff this in database, on PostConfirmation verify response topic is pending  
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                try
                {
                    NLog.LogManager.GetLogger("aws").Info("GetSubscription got error " + ex.ToString());
                } catch(Exception ex2)
                {

                }
                return BadRequest("AN ERROR HAPEPNED: " + msg);
            }
        }
        public IHttpActionResult PostSubscription(string TopicName)
        {
            return Ok(TopicName);
        //    try
        //    {
        //        var endpoint = ConfigurationManager.AppSettings["SnsEndpoint"];
        //        var topicarn = ConfigurationManager.AppSettings["SnsEventsTopicArn"];
        //        var client = new Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceClient();
        //        var request = new AM.SubscribeRequest
        //        {
        //            Protocol = "HTTP",
        //            Endpoint = endpoint,
        //            TopicArn = topicarn
        //        };
        //        var response = client.Subscribe(request);
        //        return Ok(response.SubscriptionArn); //TODO - stuff this in database, on PostConfirmation verify response topic is pending  
        //    } catch (Exception ex)
        //    {
        //        string msg = ex.ToString();
        //        return BadRequest(msg);
        //    }
        }
        
        public async Task<IHttpActionResult> Post()
        {
            try
            {
                ConfigureNLog();
            }
            catch (Exception ex)
            {
                return BadRequest("ERROR DURING NLOG CONFIG " + ex.ToString());
            }
            try {
                NLog.LogManager.GetLogger("aws").Info($"Generic Post Method called");
                string result = await Request.Content.ReadAsStringAsync();
                NLog.LogManager.GetLogger("aws").Info($"Generic Post Method Body=" + result);
                return Ok();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                NLog.LogManager.GetLogger("aws").Info($"PostConfirmation exception " + msg);
                return BadRequest("AN ERROR HAPEPNED: " + msg);
            }

        }
        private async Task<IHttpActionResult> PostConfirmation(ConfirmationMessage response)
        {
            //TODO - verify auth - http://docs.aws.amazon.com/sns/latest/dg/SendMessageToHttp.verify.signature.html
            //TODO - verify this confirmation is expected topicarn recorded in PostSubscription
            try
            {
                ConfigureNLog();
            }
            catch (Exception ex)
            {
                return BadRequest("ERROR DURING NLOG CONFIG " + ex.ToString());
            }
            try
            {
                

                NLog.LogManager.GetLogger("aws").Info($"PostConfirmation called");
                string confirmResponse = JsonConvert.SerializeObject(response);
                NLog.LogManager.GetLogger("aws").Info($"PostConfirmation payload = " + confirmResponse);
                string serialized = SerializeRequest(Request, response);

                NLog.LogManager.GetLogger("aws").Info("SERIALIZED REQUEST " + serialized);

                HttpClient httpclient = new HttpClient();
                NLog.LogManager.GetLogger("aws").Info($"PostConfirmation invoking httpClient.GetAsync");
                var result = await httpclient.GetAsync(response.SubscribeURL);
                NLog.LogManager.GetLogger("aws").Info($"PostConfirmation reading response");
                var msg = await result.Content.ReadAsStringAsync();

                NLog.LogManager.GetLogger("aws").Info($"PostConfirmation returning " + msg);
                return Ok(msg + "<br/>" + confirmResponse);
            } catch (Exception ex)
            {
                string msg = ex.ToString();
                NLog.LogManager.GetLogger("aws").Info($"PostConfirmation exception " + msg);
                return BadRequest("AN ERROR HAPEPNED: " + msg);
            }
        }

        //public IHttpActionResult PostNotification(Notification Message)
        //{
        //    try
        //    {
        //        string msg = SerializeRequest(Request, Message);
        //        return Ok(msg);
        //    } catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }

        //}

        private string SerializeRequest(HttpRequestMessage Request, Object Message)
        {
            StringBuilder sb = new StringBuilder("HEADERS");
            Request.Headers.ToList().ForEach(h => {
                sb.Append(h.Key);
                h.Value.ToList().ForEach(v => {
                    var val = HttpUtility.UrlDecode(v);
                    sb.Append(val).Append("; ");
                });
            });
            sb.AppendLine("---------------------");
            sb.AppendLine("BODY");
            Message.GetType().GetProperties(System.Reflection.BindingFlags.Public).ToList().ForEach(p =>
            {
                sb.Append(p.Name).AppendLine(p.GetValue(Message).ToString());
            });
            return sb.ToString();
        }

        static void ConfigureNLog()
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);

            var awsTarget = new AWSTarget()
            {
                LogGroup = "NLog.ProgrammaticConfigurationExample",
                Region = "us-east-2"
            };
            config.AddTarget("aws", awsTarget);

            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleTarget));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, awsTarget));

            LogManager.Configuration = config;
        }
    }
    public class ConfirmationMessage
    {
        public string Type { get; set; }
        public string MessageId { get; set; }
        public string Token { get; set; }
        public string TopicArn { get; set; }
        public string Message { get; set; }
        public string SubscribeURL { get; set; }
        public string Timestamp { get; set; }

    }

    public class Notification
    {
        public string Message { get; set; }
        public string MessageId { get; set; }
        public string Signature { get; set; }
        public string SignatureVersion { get; set; }
        public string SigningCertURL { get; set; }
        public string Subject { get; set; }
        public string Timestamp { get; set; }
        public string TopicArn { get; set; }
        public string Type { get; set; }
        public string UnsubscribeURL { get; set; }

        //public string SubscribeURL { get; set; }

        //public string Token { get; set; }

    }

}
