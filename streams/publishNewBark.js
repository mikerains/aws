'use strict';
var AWS = require("aws-sdk");
var sns = new AWS.SNS();
sns.setRegion(Region.getRegion(Regions.US_EAST_2));
exports.handler = (event, context, callback) => {

    event.Records.forEach((record) => {
        console.log('Stream record: ', JSON.stringify(record, null, 2));
        
        if (record.eventName == 'INSERT') {
            var who = JSON.stringify(record.dynamodb.NewImage.Username.S);
            var when = JSON.stringify(record.dynamodb.NewImage.Timestamp.S);
            var what = JSON.stringify(record.dynamodb.NewImage.Message.S);
            var params = {
                Subject: 'A new bark from ' + who, 
                Message: 'Woofer user ' + who + ' barked the following at ' + when + ':\n\n ' + what,
                TopicArn: 'arn:aws:sns:us-east-2:349960403903:wooferTopic'
            };
            sns.publish(params, function(err, data) {
                if (err) {
                    console.error("Unable to send message. Error JSON:", JSON.stringify(err, null, 2));
                    callback(err, null);
                } else {
                    console.log("Results from sending message: ", JSON.stringify(data, null, 2));
                    callback(null, data);
                }
            });
        }
    });
    callback(null, `Successfully processed ${event.Records.length} records.`);
};   