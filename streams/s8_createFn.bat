aws lambda create-function ^
    --region us-east-2 ^
    --function-name publishNewBark ^
    --zip-file fileb://publishNewBark.zip ^
    --role arn:aws:iam::349960403903:role/service-role/WooferLambdaRole ^
    --handler publishNewBark.handler ^
    --timeout 5 ^
    --runtime nodejs4.3