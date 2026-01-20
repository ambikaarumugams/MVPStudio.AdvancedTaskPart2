@ManageListingsSendRequest
Feature: ManageListingsSendRequest

 As a registered user, I want to send a request to trade skills
 So that I can trade skills with other users
 
Scenario: UserA creates listing and UserB sends trade request
	Given I login as "UserA" from "ManageListingsSendRequestLoginDetails.json"
	And I create a share skill using "ManageListingsSendRequestTestData1.json"
	And I logout
	When I login as "UserB" from "ManageListingsSendRequestLoginDetails.json"
	And I send a trade request using "TradeRequestData.json"
	Then I should see trade request success message

Scenario: UserB creates listing and UserA sends trade request
	Given I login as "UserB" from "ManageListingsSendRequestLoginDetails.json"
	And I create a share skill using "ManageListingsSendRequestTestData2.json"
	And I logout
	When I login as "UserA" from "ManageListingsSendRequestLoginDetails.json"
	And I send a trade request using "TradeRequestData.json"
	Then I should see the trade request success message
  
Scenario: UserA should see the received trade request
	Given I login as "UserA" from "ManageListingsSendRequestLoginDetails.json"
	When I navigate to the received trade requests page
	Then I should see the trade request from the notifications

Scenario: User cannot send trade request to their own listing
	Given I login as "UserA" from "ManageListingsSendRequestLoginDetails.json"
	And I create a share skill using "ManageListingsSendRequestTestData1.json"
	When I open my own listing
	Then the Send Request button should be disabled
	And I send a trade request using "TradeRequestData.json"
	Then I shouldn't end the trade request


