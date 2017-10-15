Feature: Remove a loan from a commit
In order to remove a loan from a commit
As a client
I want to go to the Remove Loan from Commit Page

Scenario: Remove loans
Given on homepage
When click on commit management
And select a commit
And click edit
Then the client can select a loan to remove
And click remove loan from commit
And close browser