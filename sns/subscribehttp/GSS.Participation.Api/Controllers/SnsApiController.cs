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

namespace GSS.Participation.Api.Controllers
{
    /// <summary>
    /// http://docs.aws.amazon.com/sns/latest/dg/welcome.html
    /// 
    /// </summary>
    public class SnsApiController : ApiController
    {
        public IHttpActionResult GetTest(string name)
        {
            return Ok("HELLO " + name);
        }
        public IHttpActionResult PostSubscription(string TopicName)
        {
            var endpoint = ConfigurationManager.AppSettings["SnsEndpoint"];
            var topicarn= ConfigurationManager.AppSettings["SnsEventsTopicArn"];
            var client = new Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceClient();
            var request = new AM.SubscribeRequest {
                Protocol = "HTTP",
                Endpoint = endpoint,
                TopicArn = topicarn
            };
            var response = client.Subscribe(request);
            return Ok(response.SubscriptionArn); //TODO - stuff this in database, on PostConfirmation verify response topic is pending  
        }
        public async Task<IHttpActionResult> PostConfirmation(AM.ConfirmSubscriptionResponse response)
        {
            //TODO - verify auth - http://docs.aws.amazon.com/sns/latest/dg/SendMessageToHttp.verify.signature.html

            HttpClient httpclient = new HttpClient();
            var result = await httpclient.GetAsync(response.SubscriptionArn);
            var msg = await result.Content.ReadAsStringAsync();
            return Ok(msg);
        }
    }
}
