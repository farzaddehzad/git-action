Feature: Navigation

    Scenario: Basic navigation
        Given I am on the login page
        When I enter "admin" as the username
        And I enter "admin" as the password
        And I click the login button
        Then I should be logged in successfully
        
    Scenario Outline: Login with different credentials
        Given I am on the login page
        When I enter "<username>" as the username
        And I enter "<password>" as the password
        And I click the login button
        Then I should be logged in successfully

        Examples:
          | username | password |
          | admin1    | admin1    |
          | admin1     | admin1   |