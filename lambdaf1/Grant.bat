aws lambda add-permission ^
--function-name CreateThumbnail ^
--region us-east-2 ^
--statement-id CASCORPUSA-INVOKE-CreateThumbnail ^
--action "lambda:InvokeFunction" ^
--principal s3.amazonaws.com ^
--source-arn arn:aws:s3:::com.cascorpusa.sample.photo ^
--source-account 349960403903 ^
--profile adminuser