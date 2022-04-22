Feature: Admin

as an admin I want to define movies on specific city,cinema,salon and sans 

Background: 
	Given : I loged in as an admin with following information
	| Email                 | Password  |
	| javidleo.ef@gmail.com | 123423211 |


Scenario: Define Movie on website
	When I define a new movie on the website with specific 
	Then everybody should be able to see that on the website

Scenario: Define Salons for Cinema
	When I Defined a new Salon for a Cinema
	Then I Should can Let People See movies inside it 