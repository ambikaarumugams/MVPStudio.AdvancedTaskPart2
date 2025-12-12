@CertificationsAdd
Feature: CertificationsAdd

As a registered user, I would like to add certification details to my profile page
so that others can see the certifications I have attained.

Background:
	Given I navigate to the profile page as a registered user

Scenario: Validate that the user is able to add certifications using external JSON file
	When I enter certification details from json file with the TestName "AddCertificationDetails_ValidInput"
	Then I should see the success message

Scenario: Validate that the user is not able to add the certification details with invalid certificate or award
	When I enter invalid certification details from json file with the TestName "AddCertificationDetails_InvalidCertificateOrAward"
	Then I should see the error message

Scenario: Validate that the user is not able to add the certification details with invalid certificate from
	When I enter invalid certification details from json file with the TestName "AddCertificationDetails_InvalidCertificateFrom"
	Then I should see the error message

Scenario: Validate that the user is not able to add the certification details with huge string as Certificate or Award
	When I enter lengthy Certificate or Award details from json file with the TestName "AddCertificationDetails_MoreThan250CharactersOfCertificateOrAward"
	Then I should see the error message

Scenario: Validate that the user is not able to add the certification details with huge string as Certificate from
	When I enter lengthy Certificate from details from json file with the TestName "AddCertificationDetails_MoreThan250CharactersOfCertificateFrom"
	Then I should see the error message

Scenario: Validate that the user is not able to add certification details by giving either one or all of the fields empty
	When I leave either one or all the fields empty and give the data from json file with the TestName "AddCertificationDetails_LeaveEitherOneOrAllTheFieldsEmpty"
	Then I should see the error message for empty fields

Scenario: Validate that the user is not able to add the certification details which already exists in the list (Duplicate data)
	When I enter same certification details twice from json file with the TestName "AddCertificationDetails_DuplicateData"
	Then I should see the error message for duplicate data

Scenario: Validate that the user is not able to add certification details when the session has expired
	When I enter certification details from json file after the session has expired with the TestName "AddCertificationDetails_WhenSessionExpired"
	Then I should see the error message for session expired

Scenario: Validate that the user is not able to add the Certificate or Award and Certified From (e.g. Adobe) combinations aren't matching
	When I enter certification details from json file with the TestName "AddCertificationDetails_CertificateOrAwardAndCertificateMismatch"
	Then I should see the error message for certificate and provider mismatch

Scenario: Validate that the user is able to cancel the add process
	When I enter certification details from json file and cancel the add with the TestName "AddCertificationDetails_Cancel"
	Then I should see the certification details shouldn't be added

Scenario: Validate that the user is able to add huge data in the"Certificate or Award" and  "Certified From (e.g. Adobe)" field
	When I enter huge Certificate or Award details to perform add from json file with the TestName "AddCertificationDetails_DestructiveTesting"
	Then I should see the error message