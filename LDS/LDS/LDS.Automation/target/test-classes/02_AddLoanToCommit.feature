Feature: Add a loan to a commit
In order to add a loan to a commit
As a client
I want to go to the Add Loan to Commit Page

Scenario: Add loans
Given on homepage
When click on commit management
And select a commit
And click edit commit
Then the client can select a loan
And click add loan to commit
And close browser