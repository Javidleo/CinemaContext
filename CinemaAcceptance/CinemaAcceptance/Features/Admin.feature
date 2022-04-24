Feature: Admin

as an admin I want to define movies on specific city,cinema,salon and sans 

Background: 
	Given : I loged in as an admin with following information
	| Email                 | Password  |
	| javidleo.ef@gmail.com | 123423211 |


Scenario: Define Movie on website
	When I define a new movie on the website with specific 
	Then everybody should be able to see that on the website

Scenario: Deactive Cinema for Fixing
	And I FindOut a Big Problem in Cinema has happend 
	When I DeActive Cinema For Fixing 
	Then No Admin Should Not Can Add Movies for this Cinema

Scenario: Deactive Salon for Fixing 
	And I know there is a big problem in Salon 
	When i deactive salon 
	Then no admin should not can add movies for this salon 

Scenario: Deactive Chair
	And i know one of chairs broked or something 
	When i deactive chair 
	Then any customer should not can rent it for watching movie