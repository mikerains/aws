aws lambda create-function ^
--region us-east-2 ^
--function-name CreateThumbnail ^
--zip-file fileb://CreateThumbnail.zip ^
--role arn:aws:iam::349960403903:role/lambda-s3-execution-role ^
--handler CreateThumbnail.handler ^
--runtime nodejs6.10 ^
--profile adminuser ^
--timeout 10 ^
--memory-size 1024