Feature: Check a commit versus a set of business rules
In order to finalize a commit
As a client
I want to go to verify the commit passes the business rules

Scenario: Verify business rules
Given on homepage
And a set of business rules
When a commit is submitted
Then check it against the business rules