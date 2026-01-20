@ManageListingsToggleONOFF
Feature: ManageListingsToggleONOFF

  As a registered user
  I want to enable or disable my service listings
  So that I can control their visibility

Scenario: User can toggle listing OFF and ON and verify via search
	Given I login as "UserB" from "ManageListingsSendRequestLoginDetails.json"
	And I create a share skill using "ManageListingsToggleOnOffTestData.json"
	When I navigate to Manage Listings
	And I toggle the listing status to OFF for the stored listing
	Then I should see listing status as "Service has been deactivated" and other users shouldn't see the listings
	When I navigate to Manage Listings
	And I toggle the listing status to ON for the stored listing
	Then I should see listing status as "Service has been activated" and other users should see the listings
