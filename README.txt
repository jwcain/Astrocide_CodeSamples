NOTICE: This file was written originally with intent to submit it with a built version of the
game. Due to file szie upload restrictions it has not been included.


Astrocide - Coney Dog Games, LLC
Timeline:
	I worked on this project as an intern during the summer of 2018
	
DISCLAIMER:
	I served as the sole code author on this project, but I was an employee of Coney Dog
	and the game was developed following their GDD and under their direction.
	After a discussion with them, they have allowed me to share a built version of the
	game prototype along with code samples.
	
CODE AUTHOR:
  Justin Cain 
  @AffinityForFun
  jwcain@mtu.edu
  jwcain.github.io
  
  
FIL STRUCUTRE:
	In the root folder there are two sub folders "Astrocide-Windows" and "Code samples".
	The code samples folder contains a series of sub folders, each containing a code sample
	and an accompanying README. Of the coding samples provided, the main one I wish to 
	hight light is the DynamicAI system.
	
	
	The Astrocide folder contains a Windows built version of the game. The remainder of this
	README will discuss the general concepts and how to play the game.
	
Astrocide DESCRIPTION:
	"
	A fleet of sentient, chromatic asteroids are hurdling toward your home planet, which is 
	perilously located inside an asteroid belt. The asteroids, which have formed alliances 
	with Martians and Phobian unmanned kamikaze drones, are not pleased with humanity's 
	expansion into The Belt, and they mean to do something about it!
	" - Astrocide GDD
	
	Astrocide is a fixed screen, arcade shooter. The player is tasked with stopping enemies
	from crashing into the ground by piloting a vehicle capable of Firing, Dashing, and 
	dropping a screen-clearing Bomb.

	Enemies attack from the upper regions of the screen, and they act in 4 unique ways.
	Meteor - The meteor is the basic enemy type. It falls from the top of the screen at a fixed
			angle. The Meteor comes in three sizes, small, medium, and large. Medium and large
			meteors have a chance of splitting into smaller sized meteors.
	Drone - The drone can enter the game from the top or the upper third of the side edges.
			The drone flies erratically, actively dodging the players attempt to shoot them.
			When drones nears the ground, it will either crash or attempt to hit the player.
			If it attempts to hit the player the only way to destroy it is with a dash attack.
	Bomb -  The bomb enemy falls from the top of the screen. Their are two versions, small and 
			large. The small bomb falls faster, and makes a small explosion on the ground if it
			makes contact. The large bomb falls slowly, takes two hits to destroy, and makes a 
			larger explosion when it reaches the ground. These explosion's damage the player.		
	UFO -	The UFO enemy is a special enemy. It stays at the top of the screen, moving back
			and forth, and fires bullets at the player. The UFO will occasionally leave the
			screen on its own or stick around until the player destroys it.
			
	The ultimate objective of the game is to reach the highest score possible. Destroying enemies
	rewards points, and enemies colliding with the ground removes points.


HOW TO PLAY:
	The build version uses keyboard to control the game
	Menu navigation - A/W/S/D Are used for expected left/up/down/right movement
	Menu Accept Button - Space-bar, Used to confirm an option in a menu
	
	Movement -A/D Are used for standard left/right movement
	Fire - Space-bar, fires a bullet
	Dash - Left control is used to do a short range, damaging dash attack in the current 
			movement direction. During the dash, the player is invulnerable and can destroy 
			enemies. The dash is then put on cooldown for a few seconds, indicated by a glowing sphere in the center
		o	f the player
	Bomb - R, This fires a screen-clearing bomb. This is only usable when the gauge on the 
			right hand side is full.
			
			
Astrocide DESIGN:
	While coding changes to the game I focused on maintaining an always playable state. When 
	planning my code systems design I tried to stage them so that implementation could be done quickly,
	and non disruptive improvements could me made later. To accomplish this, I sat down with the GDD
	and planned out the likely systems that needed to be implemented and broke them down into small,
	daily accomplished tasks. As a consequence to this and using task management tools, I have a log
	of my progress I will summarize below
	
	Sprint 1
		My first major implementation was to restructure how the internal game flow and menu systems worked.
		At the time, everything was hard coded as a linear sequence of Unity scene changes, pressing the menu
		button moved you to the menu scene. While this was functional, it did not make for great control flow.
		I designed a state machine based system, allowing for better contextual code operation. This was a 
		straightforward change, as the game was easily representable by a simple graph. An example graph can be 
		found in StateGraphExample.png. Included in this was a special Update framework I used to allow for
		states to have frame-update code but not have it run while the state is disabled.
		
		I also created an generic object poolers. Since the game will be creating and destroying the same enemies
		many times, it made sense to reuse enemies (and other things) to increase optimization.
		
		Finally, I created a an Input abstraction layer. This abstracted the known game verbs (Move,Fire,Dash,ETC)
		from the code detecting input. This also allowed for input format changing (keyboard/controller) to
		not have effects on other systems.
		
	Sprint 2
		I worked on creating the base player class, modified the controls to work with the new input manager
		and added the player bullets to an object pooler. A good amount of time here was spent researching
		different ways to handle player movement to increase accuracy.
	
	Sprint 3
		The next sprint was spent setting up poolers for all enemy types, hooking them into the proper systems,
		testing them.
		
		After setting up all of the enemies, I realized there was much repeated behavior but not all enemies
		used all of the repeated behavior. This is when I designed CodeExample/DynamicAI to better allow for 
		different behaviour groups.
		
	Sprint 4
		This sprint was spent setting up the overarching game flow. This involved designing how Zones are handled
		and allowing for gameplay to be based off of the current zone rather than static.
		The first player HUD and the ability for the player to die was added.
		The ability to save and load a highscore was added.
		
	Sprint 5
		Sprint 5 was spent designing a power up system for the player. This system was not included in the included
		build as the designers had not figured out the best way to reward power ups yet. This worked by keeping track
		of a collection of active power ups and relating the sum modifications to the player script
		
	Sprint 6
		In sprint 6 I started designing code for menu's as well as actually implementing the menus themselves.
		
	Sprint 7
		In sprint 7 I reworked how the player dash worked. At the time, the 'dash' was a randomized teleport
		with the intent of only being used to move the player out of sticky situations. I did not agree with
		mechanic, as it took control out of the player's hand and they could not plan around it. With my 
		employers permission, I drafted a prototype for a 'Dash' instead. This returned control to the player
		and gave them a second tool to destroy enemies. Because of this change, the AI for drones was updated
		to occasionally chase the player
	
	Sprint 8+
		From here, I was entering my final sprint working with the team, so I focused on finishing systems and
		minimizing future maintenance as I was unsure when the team was going to get another programmer.
		
		After working with the team, we ended up going with a new art style using 3D cubes to represent pixels
		in our pixel art. I created a tool for the designers to convert sprites to a 3D cube representation.
		As part of this, I spent time resigning the UI and the main level backdrop to match this style.
		The tool can be found in CodeExamples/SpriteImporter.
		
		As part of the new visuals, I created code to allow for the pixels to 'fall apart' dynamically when an
		enemy is destroyed.
		
		I implemented a basic statistics collector. It can be found in CodeExamples/Utils.
		
		I created a system to bind Unity coroutines to certain states, so that the coroutines could be started
		freely and they would not continue execution outside of the appropriate state.
		
		Finally, I spent my final weeks working on new systems for enemy spawning. The enemy spawning generates
		random amounts of enemies from the total pool of possible enemies. This often did not lead to varied
		or interesting gameplay sequences.
		
		The solution I devised was to implement a system that awarded players for shooting enemies in a specific
		sequence, to spawn enemies in dynamic patterns instead of randomly, and the allow for ramping intensity
		per enemy wave. I was able to get the rough design of these new algorithms implemented, but I was unable
		to finish them due to running out of time in my internship. The project does not utilize the system.
		
		
	After this, my internship with Coney Dog was completed.
	
EMPLOYER FEEDBACK:
	While discussing using this project for my portfolio, my former employer also included the following letter
	and will be used as a reference in my application.
	
	"
	Name: Tommy Stuart
	Title: Co-founder & CEO of Coney Dog Games, LLC
	Email: tommy@coneygames.com
	Phone Number: (248) 266-1219

	Over this past summer (2018), Justin Cain worked as an intern for our small startup video game studio. After 
	a comprehensive interview process accepting applications from the whole of Husky Games Development, an 
	enterprise group at Michigan Technological University, we determined that Justin was the best candidate.
	Justin was proactive, enthusiastic and multi-talented. That enthusiasm continued throughout the entire 
	internship, as he worked 30-50 hours per week from home. This experience showed me that he is not only 
	talented, but also motivated. 

	At the beginning of the internship, Justin was provided with our game design document, and he quickly set 
	to work breaking it down into tasks on a platform called "HacknPlan" (similar to Trello). His experience 
	working with teams of programmers was something that our company was lacking, and it was appreciated. Among 
	the tasks he undertook were: programming a dynamic AI for the main game loop, creating a pixel-to-voxel 
	converter, and writing a statistics collector. Additionally, Justin wrote reports and regularly updated us 
	on his progress as he worked remotely. 

	Looking back at our interview and hiring process, I would absolutely make the same decision in taking Justin
	on as a team member. He is a valuable asset, with good insight and work ethic.
	"


	
	
	