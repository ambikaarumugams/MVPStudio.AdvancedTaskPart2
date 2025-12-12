@ManageListingsView
Feature: ManageListingsView

  As a registered user, I want to view the Manage Listings page
  So that I can see the skills I have shared

  Background:
	Given I navigate to the profile page as a registered user

@Valid
Scenario: Validate that the shared skill is displayed in the Manage Listings page   
    When I save a new shared skill using "ShareSkill_UsingSkillExchangeValidInput.json"
    And I click the Manage Listings tab
    Then the added skill should be displayed in the Manage Listings page
    And I open the skill using the View icon
    And I should be able to validate the displayed skill details

Scenario: Validate that the user can chat with others using chat box
    When I save the new skill using "ShareSkill_UsingSkillExchangeValidInput.json" and view the skill using view icon
    And I click the chat box to communicate with others
    Then I should be able to send a message successfully

    Scenario: Validate that the request button is enabled or not
    When I save the new skill using "ShareSkill_UsingSkillExchangeValidInput.json" and view the skill using view icon
    And I click the request button to send request to others
    Then I should be able to send a request if request button is enabled
 





