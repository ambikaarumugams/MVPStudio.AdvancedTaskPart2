@Description
Feature: Description

As a registered user, I would like to add description to my profile page
So that others can see about my background

Background:
	Given I navigate to the profile page as a registered user

@Valid @Positive
Scenario: Validate that the user can add valid description details
	When I enter the text in the description using edit icon from the json file "Description_ValidInput.json"
	Then the text should be saved successfully

@Invalid @Negative
Scenario Outline: Validate that the user can't add invalid description details
	When I enter invalid description text using the edit icon from "<JSON File>"
	Then I should see an appropriate validation error message

Examples:
	| JSON File                               |
	| Description_UsingRandomStrings.json     |
	| Description_UsingRandomNumbers.json     |
	| Description_UsingSpecialCharacters.json |
	| Description_UsingAlphanumerics.json     |
	| Description_UsingSpace.json             |

@Invalid
Scenario: Validate that the user leaves the field empty
	When I leave the description field empty from the json "Description_LeaveTheFieldEmpty.json"
	Then I should see the appropriate message

@Usability
Scenario: Validate that the placeholder text is displayed inside the description textbox
	When I click the edit icon
	And I read the placeholder text from the description textbox
	Then it should match the expected placeholder text from "Description_GetPlaceholderText.json"

@Boundary
Scenario: Validate the boundary check by entering different length of the text
	When I enter the text with different range using the edit icon from "Description_BoundaryCheck.json"
	Then I should see the appropriate validation message

@Invalid @Negative
Scenario: Validate that the user can add the text using leading and trailing spaces
	When I enter the text using leading and trailing spaces from the json file "Description_LeadingAndTrailingSpaces.json"
	Then I should see the appropriate message

@Destructive
Scenario: Validate that the user can't add excessively long data in the description field
	When I enter excessively long data in the description field from the json file "Description_DestructiveData.json"
	Then I should see the appropriate message
 