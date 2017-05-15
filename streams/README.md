The goal here is to trigger a Lambda function when a DynamoDB table is updated.
That Lambda Function can then emil an SQS message that is subscribed by ConfigurationManager running in EC2.



[DynamoDB Streams and AWS Lambda Triggers uding CLI](http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Streams.Lambda.Tutorial.html)

[DynamoDB Stream using Console]
(https://aws.amazon.com/blogs/aws/dynamodb-update-triggers-streams-lambda-cross-region-replication-app/)



