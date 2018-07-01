Here's the logic so far:

* Everything starts in MegaManager.cs (under gameobject GameManagers).

SETUP:

* MegaManager sets up the Table, the Players and the GridManager.
	* Gridmanager is assigned a Grid from the Table object.
	* Gridmanager creates a tokenbag and fills it with tokens.
	* Gridmanager creates the physical grid and fills it with tokens from the bag.
* MegaManager creates two Players. Each player...
	* instantiates and/or sets up a Deck, DiscardPile, Hand, Health and Damage.
* MegaManager "starts the game" by changing the turnphase to 'Beginning'.
	* This in turn triggers some events:
	* Triggers "end of 'beginning' phase". (This does nothing).
	* Triggers "beginning of 'beginning' phase". This calls BeginningOfTurn() on MegaManager.
		*This in turn changes the current player to the next (from -1 to 0).
			*This activates the player: