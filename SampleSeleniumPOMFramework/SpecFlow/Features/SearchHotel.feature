Feature: Search Hotel
	Using Destination and and Checkin- checkout dates search a Hotel

@mytag
Scenario: Search Hotel
	Given Enter the destination
	And Enter the Chekin and Checkout Date
	When I click on Search button
	Then the options for entered destination should be displayed on the screen
