aws dynamodb create-table ^
    --table-name BarkTable ^
    --attribute-definitions AttributeName=Username,AttributeType=S AttributeName=Timestamp,AttributeType=S ^
    --key-schema AttributeName=Username,KeyType=HASH  AttributeName=Timestamp,KeyType=RANGE ^
    --provisioned-throughput ReadCapacityUnits=1,WriteCapacityUnits=1 ^
    --stream-specification StreamEnabled=true,StreamViewType=NEW_AND_OLD_IMAGES

