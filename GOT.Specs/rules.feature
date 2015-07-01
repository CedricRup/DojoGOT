Feature: RulesOfGameOfThrones

	Nous avons 5 livres de Games Of Thrones qui forment une collection.
	Chaque copie coute 8€
	Si j'achete 2 livres différentes, j'ai le droit à une réduction de 5%
	Si j'achete 3 livres différentes, j'ai le droit à une réduction de 10%
	Si j'achete 4 livres différentes, j'ai le droit à une réduction de 20%
	Si j'achete les 5 livres, j'ai le droit à une réduction de 25%
	Si j'achete 4 livres, et 3 sont diférents, j'ai le droit à une réduction de 10% mais le 4e livre coute toujours 8€. 
	** Winter Is Coming **

Scenario: Validation de l'exercice
	Given j'ai acheté "2" exemplaires du livre numéro "1"
	And j'ai acheté "2" exemplaires du livre numéro "2"
	And j'ai acheté "2" exemplaires du livre numéro "3"
    And j'ai acheté "1" exemplaires du livre numéro "4"
	And j'ai acheté "1" exemplaires du livre numéro "5"
	When je passe à la caisse
	Then le cout total est de 51.20€
