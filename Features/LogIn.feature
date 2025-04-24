Feature: Log in submission
    
    
    Scenario Outline: Login with different credentials
        Given I am on the signin page
        When I enter "<username>" as the username
        And I enter "<password>" as the password
        And I press the log in button
        Then I should be logged in
        
        
        Examples: 
        | username | password |
        | admin1   | admin1   |
        | admin1   | admin1   |
        
        
        