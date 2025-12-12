@ManageListingsEdit
Feature: ManageListingsEdit

As a registered user, I want to the edit my skill that I've shared already
So that the users can see my updated skills in Manage Listings

Background:
	Given I navigate to the profile page as a registered user

Scenario: Validate that the user can update the shared skill with skill exchange option using Manage Listings Edit icon 
	When I update the shared skill with skill exchange option using edit icon in the Manage listings from the json file "ManageListingsEdit_UsingSkillExchangeValidInput.json"
	Then the skills should be updated successfully

Scenario: Validate that the user can update the shared skill with credit option using Manage Listings Edit icon 
	When I update the shared skill with credit option in the Manage listings from the json file "ManageListingsEdit_UsingCreditValidInput.json"
	Then the skills should be updated successfully

Scenario: Validate that the user can update the shared skill title with random strings using Manage Listings Edit icon 
	When I update the shared skill title using random strings in the Manage listings from the json file "ManageListingsEdit_UsingRandomStrings.json"
	Then the skills shouldn't be updated successfully

Scenario: Validate that the user can update the shared skill title with special characters using Manage Listings Edit icon 
	When I update the shared  title using special characters in the Manage listings from the json file "ManageListingsEdit_UsingSpecialCharacters.json"
	Then the user should see the error message

Scenario: Validate that the user can update the shared skill title with first character as a white space using Manage Listings Edit icon 
	When I update the shared skill title with first character as a white space in the Manage listings from the json file "ManageListingsEdit_UsingFirstCharacterAsAWhiteSpace.json"
	Then the user should see the error message

Scenario: Validate that the user can update the shared skill title with first character as a number using Manage Listings Edit icon 
	When I update the shared skill title with first character as a number in the Manage listings from the json file "ManageListingsEdit_UsingFirstCharacterAsANumber.json"
	Then the skills should be updated successfully

	
Scenario: Validate that the user can update the shared skill title with 100 characters using Manage Listings Edit icon 
	When I update the shared skill title with  characters in the Manage listings from the json file "ManageListingsEdit_Title100Characters.json"
	Then the skills should be updated successfully
