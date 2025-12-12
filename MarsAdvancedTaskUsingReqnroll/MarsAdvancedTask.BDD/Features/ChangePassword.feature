Feature: ChangePassword

As a registered user, I want to change the password of my existing account
So that I can keep my account secure

Background:
	Given I navigate to the profile page as a registered user

Scenario: Validate that the user can change the password with valid inputs
	When I change the password using valid details from the json file "ChangePassword_Valid.json"
	Then the password should be changed successfully

#Scenario: Validate that the user can change the password again using the previous password
#	When I try to change the password again using the previous password from the json file "ChangePassword_ChangeToOldPassword.json" 
#	Then the password should be changed successfully

Scenario: Validate that the user can't change the password by entering an incorrect current password
	When I enter an incorrect current password and try to save from the json file "ChangePassword_InvalidCurrentPassword.json"
	Then the password should not be changed

Scenario: Validate that the user can't set the new password equal to the current password
	When I enter the current password as the new password from the json file "ChangePassword_CurrentPasswordAsNewPassword.json"
	Then the password should not be changed

Scenario: Validate that there is a mismatch between the New password and Confirm password
	When I enter different values for the new password and confirm password from the json file "ChangePassword_MismatchBetweenNewPasswordAndConfirmPassword.json"
	Then the password should not be changed

Scenario: Validate password strength warnings for a weak password
	When I enter a weak password without uppercase letters, numbers or special characters from the json file "ChangePassword_WeakPassword.json"
	Then I should see password strength warnings

Scenario: Validate that the user cannot change the password using only random strings
	When I enter random strings as the new password and confirm password from the json file "ChangePassword_RandomStrings.json"
	Then the password should not be changed

Scenario: Validate that the user cannot change the password using only numbers
	When I enter only numbers as the new password and confirm password from the json file "ChangePassword_RandomNumbers.json"
	Then the password should not be changed

Scenario: Validate that the user cannot change the password using only special characters
	When I enter only special characters as the new password and confirm password from the json file "ChangePassword_SpecialCharacters.json"
	Then the password should not be changed

#Scenario: Validate that the user cannot change the password using only spaces (more than six)
#	When I enter more than six spaces as the new password and confirm password
#	Then the password should not be changed

Scenario: Validate that the user cannot change the password when one or more fields are left empty
	When I leave one or more password fields empty from the json file "ChangePassword_LeaveEitherOneOrAllTheFieldsAreEmpty.json"
	Then the password should not be changed


Scenario: Validate the minimum length requirement of the password
	When I enter a password to test the minimum length requirement from the json file "ChangePassword_MinimumPasswordLength.json"
	Then I should see validation related to minimum length
	 

Scenario: Validate the maximum length requirement of the password
	When I enter a password to test the maximum length requirement from the json file "ChangePassword_MaximumPasswordLength.json"
	Then I should see validation related to maximum length

Scenario: Validate that the user cannot change the password with leading or trailing spaces
	When I enter a password with leading or trailing spaces from the json file "ChangePassword_LeadingTrailingSpaces.json"
	Then the password should not be changed

Scenario: Validate that the user cannot change the password when the session has expired
	When I try to change the password after the session has expired from the json file "ChangePassword_WhenSessionHasExpired.json"
	Then the password should not be changed

#Scenario Outline: Change password using different users
#    When I change the password for "<JSONFile>"
#    Then I should be able to change the password successfully
#Examples:
#    | JSONFile                              |
#    | ChangePassword_Valid_User1.json       |
#    | ChangePassword_Valid_User2.json       |
#    | ChangePassword_Valid_User3.json       |
