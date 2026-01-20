@ManageRequestsSentRequest
Feature: ManageRequestsSentRequest


Scenario: UserB should see the sent trade request status after sending request

  Given I login as "UserA" from "ManageListingsSendRequestLoginDetails.json"
  And I create a share skill using "SentRequestShareSkillData.json"
  And I logout
  When I login as "UserB" from "ManageListingsSendRequestLoginDetails.json"
  And I send a trade request using "TradeRequestData.json"
  And I navigate to Manage Requests
  And I open Sent Requests
  Then I should see the sent request as "Pending"
