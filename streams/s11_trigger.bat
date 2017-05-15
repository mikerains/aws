Copy
aws lambda create-event-source-mapping ^
    --region us-east-2 ^
    --function-name publishNewBark ^
    --event-source arn:aws:dynamodb:us-east-2:349960403903:table/BarkTable/stream/2017-05-15T19:13:38.714  ^
    --batch-size 1 ^
    --starting-position TRIM_HORIZON