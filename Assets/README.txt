Here's the logic so far:

GAME RULES:
Region Master is a deckbuilding game, with a an abstract area control kinda thing going on, in which players race to deal enough damage to the opposing player to reduce their life total to 0.

On the table is a 8x8 grid with random tokens - red, green and blue. This is mana the players need to collect.
Each player starts with a deck of 9 cards - 3 cards for each color. Most cards in the beginning will give you Markers. Markers are used to capture a token in the grid; play a card that gives you a blue marker, and you can capture (put a Marker on) a blue token.

If, at any point in your turn, your Markers cover an (enclosed) Area, you collect all those tokens. An Area is defined by a couple of things:
(1) An area is first off basically an "island" of one color; a cluster of tokens in the same color, linked by vertical or horizontal adjacency.
(2) Tokens marked by your opponent are not counted towards an Area; they're basically holes. This can mean an Area can be split into segments, each segment a seperate Area that can be captured.

As soon as you can't add any more Markers to an Area, the Area your Markers cover is collected. The tokens are removed from the grid and added to your pool. The pool is where your collected tokens are kept until spent (or wasted at the end of your turn).

Better cards can be bought and added to your deck from the shop (the six cards on the table). Each card has a cost at the top. If you can afford it with the tokens in your pool, you can drag the card into your discard pile to buy it (and automatically spent the tokens). A new card will take its place in the shop.

At the end of your turn, all the cards you played and all the cards left in your hand are discarded (i.e. they go to your discard pile), and you draw a new hand of three cards and pass the turn to your opponent.

Later on, cards will add damage. The little sword will show how much damage you can deal. Click the opponent's health in the upper left corner to deal the damage.

SETUP:

* Everything starts in MegaManager.cs (under gameobject GameManagers).
* MegaManager sets up AbilityResolver (static script for handling card abilities). 
* MegaManager sets up the Table, the Players and the GridManager.
	* Gridmanager is assigned a Grid from the Table object.
	* Gridmanager creates the physical grid to put tokens in.
* MegaManager creates two Players. Each player...
	* instantiates and/or sets up a Deck, DiscardPile, Hand, Health and Damage.
* MegaManager "starts the game":
	* Random player is chosen to start the game.
	* Player is activated (game shows their hand, deck, life, etc.)
	* Player draws three cards.
	* StartGame event is evoked:
		- GridManager creates a tokenbag and fills it with tokens.
		- GridManager fills the grid with tokens from the bag.

TODO:
- Try and merge the "play" phase and "buy" phase and see how it plays. It feels like more interesting cards can come from this.
- Cards need to set special states into motion (eg. "destroy a marker", "choose and discard a card", etc).