@CertificationsDelete
Feature: CertificationsDelete

As a registered user, I would like to delete certification details from my profile page
So that others can't see about my educational background

Background:
	Given I navigate to the profile page as a registered user

Scenario: Validate that the user is able to delete the certification details
	When I delete certification details with the TestName "DeleteCertificationDetails_ValidInput"
	Then I should see the success message for delete

Scenario: Validate that the user is not able to delete certification details when the session has expired
	When I delete certification details from json file after the session has expired with the TestName "DeleteCertificationDetails_WhenSessionExpired"
	Then I should login again to perform cleanup
	Then I should see the error message to delete for session expired
	