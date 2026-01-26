@ManageRequestsReceiveRequest
Feature: ManageRequestsReceiveRequest

As a registered user, I should able to accept and decline the request

Scenario: UserA accepts the received trade request from UserB

	Given I login as "UserA" from "ManageListingsSendRequestLoginDetails.json"
	And I create a share skill using "ReceiveRequestsShareSkillData.json"
	And I logout

	When I login as "UserB" from "ManageListingsSendRequestLoginDetails.json"
	And I send a trade request using "TradeRequestData.json"
	And I logout

	Then I login as "UserA" from "ManageListingsSendRequestLoginDetails.json"
	And I navigate to Manage Requests and open Received Requests
	When I accept the received request
	Then I should see the received request as "Accepted"

Scenario: UserA declines the received trade request from UserB

	Given I login as "UserA" from "ManageListingsSendRequestLoginDetails.json"
	And I create a share skill using "ReceiveRequestsShareSkillData.json"
	And I logout

	When I login as "UserB" from "ManageListingsSendRequestLoginDetails.json"
	And I send a trade request using "TradeRequestData.json"
	And I logout

	Then I login as "UserA" from "ManageListingsSendRequestLoginDetails.json"
	And I navigate to Manage Requests and open Received Requests
	When I decline the received request
	Then I should see the received request as "Declined"
