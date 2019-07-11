Misc Utils

CODE AUTHOR:
	Justin Cain 
	@AffinityForFun
	jwcain@mtu.edu
	jwcain.github.io/Porfolio
	
	
Buffer:
	The buffer is an extended Queue that allows for a set amount of items to be kept before the oldest is discarded.
	This was used to track previous iterations of enemy wave spawns to serve as a data point for generating new waves.

Counter:
	The counter is a Dictionary string->int mapping. It adds the functionality of creating a key automatically in the 
	Dictionary if it does not already have one when used with the index ([]) operation.
		"
			Counter t = new Counter();
			t["newEntry"] = 0; // New entry is created within the indexing.
		"
		
		
StatisticsCollector:
	The statistics collector is a static script that maintains an internal counter. Code can be added throughout the project
	to report a string to the collector, and an internal Counter is updated. There is also code to save the statistics out
	to a file.

DelayedEventHandler:
	The delayed event handler allowed for events to be saved to a timer based queue. The user calls with a time delay and
	an array of UnityActions (essentially function pointers with arguments already supplied). The functions within the 
	array are called after the real-time has passed.
