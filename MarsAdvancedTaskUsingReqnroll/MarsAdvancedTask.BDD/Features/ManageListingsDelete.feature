
Feature: ManageListingsDelete
As a registered user, I want to the delete my skill that I've shared already
So that the users can't see my skills in Manage Listings

	
Scenario: Validate that the user can delete the shared skill from Manage Listings
	Given I navigate to the profile page as a registered user
	When I add a shared skill from "ManageListingsDelete.json" and delete it from Manage Listings
	Then I should see the delete success message
	And the deleted skill should no longer appear in Manage Listings
