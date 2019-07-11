DynamicAI

CODE AUTHOR:
	Justin Cain 
	@AffinityForFun
	jwcain@mtu.edu
	jwcain.github.io
  
STRUCTURE:
	There are two file, Intelligence and AIBehviour.
	
DESIGN:
	Intelligence serves as a data structure for a collection of AIBehaviour. It also serves
	as a dynamic storage container for any persistent data used by AIBehaviours. Essentially,
	the AIBehaviours are the actions that an AI can take, and the Intelligence serves as the
	instantiated representation of an enemy.
	
	AIBehaviours are static classes that can be created as Unity Assets. These are static
	after compile time but allow for customization. 
	For example, most enemies have an explosion animation when they die. Multiple ExplosionBehaviour 
	assets can be created in the editor, linking to the appropriate animation. The code for executing 
	the animation is the same, and the asset wont change after compilation.
	
	The AIBehaviour base class has been included, but the behaviors themselves have not been as those
	are the design property of Coney Dog.
	
	The general flow is as follows. The Intelligence receives a game update call. This call is distributed
	throughout all behaviours. Each behaviour has a defined chance to ignore the update call (this is often
	not used except for some randomized behaviours). Each AIBehaviour then reads its relevant data from
	the Intelligence that called it (using defaults if not found), operates in some fashion, and then
	writes out data to the Intelligence.
	
	Since all data is stored in one place, behaviours can modify other behaviours data. An example of when
	this was used is the Drone enemy. It has two behaviours for movement, one standard movement and
	one that modifies its movement data to avoid being shot by the player. This allows for the creation of
	more complex behaviours by the interaction of multiple behaviours. 