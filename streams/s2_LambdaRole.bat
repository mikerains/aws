
aws iam create-role --role-name WooferLambdaRole ^
    --path "/service-role/" ^
    --assume-role-policy-document file://trust-relationship.json
    
