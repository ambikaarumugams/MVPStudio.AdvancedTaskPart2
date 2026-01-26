@EducationAdd
Feature: EducationAdd

As a registered user, I would like to add education details into my profile page
So that others can see about my educational background

Background:
	Given I navigate to the profile page as a registered user

Scenario: Validate that the user is able to add education details using external JSON file
	When I enter education details from json file with the TestName "AddEducationDetails_ValidInput"
	Then I should see the success message

Scenario: Validate that the user is not able to add the education details with invalid College/University Name
	When I enter invalid education details from json file with the TestName "AddEducationDetails_InvalidCollegeUniversityName"
	Then I should see the error message

Scenario: Validate that the user is not able to add the education details with invalid Degree
	When I enter invalid education details from json file with the TestName "AddEducationDetails_InvalidDegree"
	Then I should see the error message

Scenario: Validate that the user is not able to add the education details with huge College/University Name
	When I enter education details from json file with the TestName "AddEducationDetails_MoreThan250CharactersOfCollegeUniversityName"
	Then I should see the error message for adding huge string

Scenario: Validate that the user is not able to add the education details with huge Degree Name
	When I enter education details from json file with the TestName "AddEducationDetails_MoreThan250CharactersOfDegreeName"
	Then I should see the error message for adding huge string

Scenario: Validate that the user is not able to add education details by giving either one or all of the fields empty
	When I leave either one or all the fields empty and give the data from json file with the TestName "AddEducationDetails_LeaveEitherOneOrAllTheFieldsEmpty"
	Then I should see the error message for empty fields

Scenario: Validate that the user is not able add same education details multiple times
	When I enter same education details twice from json file with the TestName "AddEducationDetails_DuplicateData"
	Then I should see the error message for duplicate data

Scenario: Validate that the user is not able to add education details when the session has expired
	When I enter education details from json file after the session has expired with the TestName "AddEducationDetails_WhenSessionExpired"
	Then I should see the error message for session expired

Scenario: Validate that the user is not able add education details with valid input
	When I enter education details from json file with the TestName "AddEducationDetails_NegativeTestingWithValidInput"
	Then I should see the error message for adding education details

Scenario: Validate that the user is able to cancel the add process
	When I enter education details from the Json file with the test name "AddEducationDetails_Cancel"
	Then I should see the education details shouldn't be added

Scenario: Validate that the user is not able to add huge data in the education field
	When I enter education details for destructive testing from json file with the TestName "AddEducationDetails_DestructiveTesting"
	Then I should see the error message for huge data

