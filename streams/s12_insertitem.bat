aws dynamodb put-item ^
    --table-name BarkTable ^
    --item Username={S="SPOCKIE SPOCK"},Timestamp={S="2017-05-15:14:32:17"},Message={S="SPOCKADOODLEDOO"}