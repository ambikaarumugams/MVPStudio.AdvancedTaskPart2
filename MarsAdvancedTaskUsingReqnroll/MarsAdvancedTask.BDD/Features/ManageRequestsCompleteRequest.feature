@ManageRequestsCompleteRequest
Feature: ManageRequestsCompleteRequest


Scenario: UserA completes the received trade request from UserB

  Given I login as "UserA" from "ManageListingsSendRequestLoginDetails.json"
  And I create a share skill using "ManageRequestsShareSkillForCompleteRequest.json"
  And I logout

  When I login as "UserB" from "ManageListingsSendRequestLoginDetails.json"
  And I send a trade request using "ManageRequestsShareSkillForCompleteRequest1.json"
  And I logout

  Then I login as "UserA" from "ManageListingsSendRequestLoginDetails.json"
  And I navigate to Manage Requests and open Received Requests
  When I accept the received request 
  And I complete the received request
  Then I should see the received request as "Completed"
