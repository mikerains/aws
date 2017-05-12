aws lambda invoke ^
--invocation-type Event ^
--function-name CreateThumbnail ^
--region us-east-2 ^
--payload file://manualtest.txt ^
--profile adminuser ^
outputfile.txt