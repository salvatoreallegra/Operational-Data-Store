Feature: getPredictiveAnalytics



  Scenario Outline: Get Predictive Analytics for Potential Match Recipient
  Given Get predictive analytics for a potential match recipient with a matchId of 777
  And a ptrSequenceNo of 777
  #And Set request credentials for action name Get Predictive Analytics API
  And Establish the expected model for the get predictive analytics api response 
  #And Valid OAuth Bearer Token Password Grant Type
  And Valid Default Headers 
  When Send a GET request to the URI v1/matches/@@match-id@@/potential-recipients/@@PtrSequenceNumber@@
  Then Verify response status code is 200
  And Validate Output Model for get predictive analytics api response
  #And Verify TransactionID from response found in the log file
