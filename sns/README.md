# SNS

DOCS: http://docs.aws.amazon.com/sns/latest/dg/welcome.html
* has topics for sending messages to SQS, SMS, HTTPS and Mobile Push, but doesn't have examples of .Net Code
* There is a sample Java under "Getting Started" seciton
* to see sample .Net SDK code, refer to the "Developer Guide: SNS with AWS SDK for .NET" link below.
* Also used sample code here: http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/sns-apis-intro.html

[AWS SDK for .NET Version 3 API Reference](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-programming-techniques.html)

# AWS SDK for .NET

Be sure to visit the link to [Programming with the AWS SDK for .NET](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-programming-techniques.html)

http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-setup.html




Documentation: https://aws.amazon.com/documentation/sns/

API: http://docs.aws.amazon.com/sns/latest/api/Welcome.html

API for Subscribe: http://docs.aws.amazon.com/sns/latest/api/API_Subscribe.html




[Dev Guide: SNS with AWS SDK for .NET sample code for both Low-level and Resource API](http://docs.aws.amazon.com/sdk-for-net/v2/developer-guide/sns-apis-intro.html)
* Low-level example links to  [AWS SDK for .Net V3](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/)
* Resource API links to [Programming the AWS Resource APIs for .NET](http://docs.aws.amazon.com/sdk-for-net/v2/developer-guide/resource-level-apis-intro.html#resource-level-apis-intro)

* the Resource APIs page says "The resource APIs are currently provided as a preview. Please be cautious about writing and distributing production-quality code that relies on these resource APIs, especially as the resource APIs may undergo frequent changes during the preview stage."
* To use the Resource API, remove references to AWSSDL.dll, and download the Resource API from resourceAPI-preview at github.com/aws-sdk-net.  See details on the "Resource Level API Intro" page linked above.

A point of confusion is that the documentaiton for the API and for the Resource API both refer to https://docs.aws.amazon.com/sdkfornet/v3/apidocs/Index.html, so it's not clear where the documentation is for the Resource API.
* This page has a SEARCH dropdown that can search Example Code.  Try searching this for "SNS"
* This SDK page has a link to [Getting Started with the AWS SDK for .NET](https://aws.amazon.com/developers/getting-started/net/)

## Code Excerpts
````
  <appSettings>
    <add key="AWSProfileName" value="adminuser" />
    <add key="AWSRegion" value="us-east-2" />
    
    <add key="SnsEndpoint" value="https://w03x57ncg2.execute-api.us-east-2.amazonaws.com/Dev/" />
    <add key="SnsEventsTopicArn" value="arn:aws:sns:us-east-2:349960403903:ParticipationEvent" />
    
    <!-- https://aws.amazon.com/blogs/developer/logging-with-the-aws-sdk-for-net/-->
    <add key="AWSLogging" value="SystemDiagnostics" />
    
  </appSettings>
````


![Subscription](../images/SNS_SubscribeRequestCode.png)

![Confirmation](../images/SNS_PostConfirmationCode.png)

![Confirmed Subscription](../images/SNS_ConfirmedSubscription.png)

![Beanstalk](../images/SNS_BeanstalkApp.png)

![EC2](../images/SNS_EC2_Instance.png)

![ApiGateway](../images/SNS_ApiGatewway.png)

![ApiGateway Headers](../images/SNS_ApiGateway_TransformHeaders.png)


## SNS Samples - from the Developer's Guide, setting dropdown to "Sample Code & Libraries" and searching for "SNS"
* https://aws.amazon.com/search?searchPath=code&searchQuery=SNS&x=0&y=0&this_doc_product=Amazon+Simple+Notification+Service&this_doc_guide=API+Reference&doc_locale=en_us
* [Sample Subscription Confirm and Verify Signature using Amaon's public key](https://aws.amazon.com/code/9387800257078150)





SDK link for SNS API: https://docs.aws.amazon.com/sdkfornet/v3/apidocs/


[Tools for AWS](https://aws.amazon.com/tools/)
[Getting Started With the AWS SDK for .NET](https://aws.amazon.com/developers/getting-started/net/)



