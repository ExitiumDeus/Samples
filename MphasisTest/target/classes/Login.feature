Feature: Logging into a system, validating a role, and returning a page based on the role
In order to determine the page and content seen
As a user with a role
Validate the users role and redirect to the proper page based on the role

Scenario: Login with role A
Given At login page
And User is registered with a role A
When User login
Then Validate role A
And Page redirect based on role A

Scenario: Login with role B
Given At login page
And User is registered with a role B
When User login
Then Validate role B
And Page redirect based on role B