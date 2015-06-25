Feature: RulesOfGameOfThrones

	Nous avons 5 livres de Games Of Thrones qui forment une collection.
	Chaque copie coute 8€
	Si j'achete 2 livres différentes, j'ai le droit à une réduction de 5%
	Si j'achete 3 livres différentes, j'ai le droit à une réduction de 10%
	Si j'achete 4 livres différentes, j'ai le droit à une réduction de 20%
	Si j'achete les 5 livres, j'ai le droit à une réduction de 25%
	Si j'achete 4 livres, et 3 sont diférents, j'ai le droit à une réduction de 10% mais le 4e livre coute toujours 8€. 
	** Winter Is Coming **

Scenario Outline: Si mon panier contient les même livres, je n'es tpas de reduction possible
	Given j'ai acheté <NbLivre> livres identiques
	When je passe à la caisse	
	Then le cout total est de <Total> sans réduction
	Examples: 
	| NbLivre | Total |
	| 2       | 16    |
	| 3       | 24    |
	| 4       | 32    |
	| 5       | 40    |

Scenario Outline: Si mon panier contient des livres différentes, j'ai le droit a une reduction
	Given j'ai acheté <NbLivre> livres différentes
	When je passe à la caisse	
	Then le cout total est de <Total> sans réduction
	And le cout est de <Reduc> avec réduction
	Examples: 
	| NbLivre | Total | Reduc |
	| 2       | 16    | 15.2  |
	| 3       | 24    | 21.6  |
	| 4       | 32    | 25.6  |
	| 5       | 40    | 30.0  |

Scenario: Si j'achete 4 livres, et 3 sont diférents, j'ai le droit à une réduction de 10% mais le 4e livre coute toujours 8€.
	Given j'ai acheté 3 livres différentes
	And un dernier livre identique à l'un des trois
	When je passe à la caisse
	Then le cout total est de 32 sans réduction
	And le cout est de 29.6  avec réduction

Scenario: Je doit obtenir la meilleur redution possible
	Given j'ai acheté 2 exemplaires du livre numéro 1
	And j'ai acheté 2 exemplaires du livre numéro 2
	And j'ai acheté 2 exemplaires du livre numéro 3
    And j'ai acheté 1 exemplaires du livre numéro 4
	And j'ai acheté 1 exemplaires du livre numéro 5
	When je passe à la caisse
	Then le cout total est de 64 sans réduction
	And le cout est de 51.2 avec réduction