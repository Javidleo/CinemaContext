Feature: Customer
as a Cusotmer I want to be able buy ticket and watch movies in cinema

Background: 
	Given im a user in cinema website with following information
	| Email                 | Password |
	| javidloe.ef@gmail.com | 1234321  |

Scenario: Watch Movie when Having Ticket
	When I Buy a Ticket and pay for its cost
	Then I should take the pdf file ready to print
