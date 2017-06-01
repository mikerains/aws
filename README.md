# DynamoDB Streams

[DynamoDB Update Triggers (Streams + Lambda) + Cross-Region Replication App](https://aws.amazon.com/blogs/aws/dynamodb-update-triggers-streams-lambda-cross-region-replication-app/)

[Capturing Table Activity with DynamoDB Streams](http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Streams.html)

# AWS Lambda SDK's
[Javascript SDK](http://docs.aws.amazon.com/AWSJavaScriptSDK/latest/AWS/S3.html)

[AWS CLI for Lambda](http://docs.aws.amazon.com/cli/latest/reference/lambda/index.html#available-commands)


# AWS Lambda Samples
[Use Cases - Examples of how to use Lambdas]
(http://docs.aws.amazon.com/lambda/latest/dg/use-cases.html)

[Mobile Back-end Android App and Lambda Function](http://docs.aws.amazon.com/lambda/latest/dg/with-on-demand-custom-android-example.html)

The folder [lanbdaf1](./lambdaf1) demonstrates [using Lambda functions with S3 Buckets](http://docs.aws.amazon.com/lambda/latest/dg/with-s3-example.html) based on capturing upload of a picture to an S3 bucket which triggers a Lambda Functions to Create a Thumbnail.  See the project [ReadMe](./lambdaf1/README.md).

[Step 4 of Tutorial Gets Into Serverless Deployment Packages](http://docs.aws.amazon.com/lambda/latest/dg/with-s3-example-use-app-spec.html)

[Deploying Lambda Based Applications](http://docs.aws.amazon.com/lambda/latest/dg/deploying-lambda-apps.html)
 -- [Github Resources for Deploying](https://github.com/awslabs/serverless-application-model/blob/master/versions/2016-10-31.md)

[Simple Microservice using Lambda and API Gateway](http://docs.aws.amazon.com/lambda/latest/dg/with-on-demand-https-example-configure-event-source_1.html)

[Simplified Serverless App Delpoyment and Management - Introduction article Oct 2016](https://aws.amazon.com/blogs/compute/introducing-simplified-serverless-application-deplyoment-and-management/)

[Github resources for Oct 2016 Article](https://github.com/awslabs/serverless-application-model/blob/master/examples/2016-10-31/s3_processor/template.yaml)

The folder [sam](./sam) is a work-through of [Create Your Own Serverless Application](http://docs.aws.amazon.com/lambda/latest/dg/serverless-deploy-wt.html) 

[Cloud Formations Docs](http://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/Welcome.html)

The folder [dynamodb](./dynamodb) is a .Net Application prototype for a "Configuration Manager" which retrieves Application Settings from a Dynamo Table.  The Table must have a PartitionKey and a Sort (Range) Key.  Those keys are provided by a client, colloquially known as ProjectName and ConfigurationKey.  The app is deployed to EC2 by using beanstalk.  The [Console](https://us-east-2.console.aws.amazon.com/elasticbeanstalk/home?region=us-east-2#/applications) was created by following the Beanstalk wizard to create a .Net IIS Container.  It was deployed to by using the Visual Studio AWS Tools which provide a "Deploy to AWS" in the context menu of the Web Project in the VS Solution Tree.

[Creating a Beanstalk .Net App](http://docs.aws.amazon.com/elasticbeanstalk/latest/dg/create_deploy_NET.quickstart.html)

To deploy I used the step [Deploy](https://aws.amazon.com/blogs/developer/deploy-an-existing-asp-net-core-web-api-to-aws-lambda/), I didn't use the steps about configuring Servless or Lambda functions, just the "Deploy" step in the context menu of my Web App Project.

I would like to try this demo on [Serverless Application in Visual Studio](https://aws.amazon.com/blogs/developer/aws-serverless-applications-in-visual-studio/) sometime



# Beanstalk
[My First Elastic Beanstalk Application](https://us-west-2.console.aws.amazon.com/elasticbeanstalk/home?region=us-west-2#/application/versions?applicationName=My%20First%20Elastic%20Beanstalk%20Application)

[Creating and Deploying Beanstalk Apps](http://docs.aws.amazon.com/elasticbeanstalk/latest/dg/create_deploy_Java.html)


[IAM](https://console.aws.amazon.com/iam/home?region=us-west-2#/users)


http://docs.aws.amazon.com/AmazonS3/latest/API/sig-v4-examples-using-sdks.html

http://docs.aws.amazon.com/AmazonS3/latest/API/sig-v4-header-based-auth.html


