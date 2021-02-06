Feature: Booking
	In order to test the hotel booking form
	As a user
	I want to be able to enter and delete bookings

#Initial setup: To get test system in a consistent starting state, delete all rows before starting.
Scenario: Delete all old bookings
Given I navigate to the booking page
Then I delete all old bookings


Scenario Outline: Make a booking
	Given I navigate to the booking page
	When I enter "<firstname>", "<surname>", "<price>", "<deposit>","<check in date>", "<check out date>"
	Then the displayed values equal "<firstname>", "<surname>", "<price>", "<deposit>","<check in date>", "<check out date>"
	Examples: 
	| firstname | surname | price	| deposit	| check in date | check out date	| 
	| Nobby     | Bloggs  | 100		| true		| 2021-02-10    | 2121-02-15		| 
	| Fred      | Smith   | 99.99	| false		| 2021-02-10	| 2121-02-15		| 


Scenario Outline: Remove a booking
	Given I navigate to the booking page
	When I delete row "<rowNum>"
	Then the number of rows displayed is decreased by one
	Examples:
	| rowNum |
	|   2    |
	