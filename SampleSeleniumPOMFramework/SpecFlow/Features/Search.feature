Feature: Search
Search in Application

@mytag
Scenario: Search in application
	Given Navigate to search site
	And Enter search term
	When Click on Search Image
	Then Verify the section on the Results page
