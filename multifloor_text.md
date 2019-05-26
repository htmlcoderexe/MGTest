
I am about to get to the part of the game where I will have multiple floors connected by the stairs, and now I am at a decision: do I go along with the way most of roguelikes handle the multiple floors, or do I go for something new. Below I've outlined 3 possibilities with a pro/con list each. Any items that matter only or mostly to the developer and are invisible to most players are marked in _italic._

#Traditional approach - only current floor 

The classic. Once you leave the floor, it's saved and unloaded, and time is frozen until the player revisits it. Only the player can move between the floors, with the exception of player allies and pets, sometimes denemies, if they're adjacent to the player when they use the stairs.

Pros:

* _Simplest to implement_

* Expected behaviour by experienced players

* "Easy" way out of a bad situation on a floor most of the time

* Other floors stay exactly the same until player re-enters

* _Floors can be generated as entered_

* Lowest processing requirements

Cons:

* Stale game mechanic

* Breaks continuity of action

* Player companions (pets, summons etc) are a mess to deal with

* _Any objects that depend on time passage (for example, growing plants) would need extra code to "catch up" when the player returns to that floor_

#Modern approach - full simulation

Similar to Dwarf Fortress (as far as I know, internally it is actually turn-based). All floors are run together as a single unit


Pros:

* Allows any objects to easily follow the player 

* More "natural" behaviour, especially for newcomers to the genre

* _Easier to make the world deterministic_

* _Time-based object update naturally_

Cons:

* Comes with the same processing costs as DF (most roguelikes have less detail, though), but once per turn vs 50+ times per second

* _The entire game world must be generated at once_ 

* _Additional logic might be required to prevent enemies to traverse stairs prematurely - like bosses from floor 10 walking up to floor 2 and stomping the player_

* _Pathing, visibility and detection range get complicated_

* The easy escape option is not available to the player anymore, might require rebalancing things depending on the game specifics

#Hybrid approach - current & adjacent floors

A compromise between the two other options, only the floors "above" and "below" the player's current floor are simulated

Pros:

* Still allows interaction between floors

* Can generate world as it is explored

* Only a moderate increase in processing

Cons:

* _Extra logic needed to select floors in case of branching dungeon (like Nethack)_

* Possible edge case weirdness

* Certain objects still need "catch up" logic
