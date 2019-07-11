using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Takes unity actions and invokes them after a specified delay.
/// </summary>
public class DelayedEventHandler : IGameUpdate {

	#region Values
	/// <summary>
	/// The interal list of events to track
	/// </summary>
	private static List<DelayedEvent> queue = new List<DelayedEvent>();
	#endregion


	#region Public
	/// <summary>
	/// Adds an event to the list, taking a time delay and a set of actions.
	/// </summary>
	/// <param name="timeDelay"></param>
	/// <param name="callbacks"></param>
	public void AddEvent(float timeDelay, UnityAction[] actions) {
		//Debug.Log("Add Event " +  timeDelay);
		queue.Add(new DelayedEvent(timeDelay, actions));
	}

	public void ClearEvents() {
		//Debug.Log("Clear Events " + queue.Count);
		queue = new List<DelayedEvent>();
	}

	#region Override
	public void GameUpdate() {

		//Loop through the array backwards to avoid dysnc
		for (int i = queue.Count - 1; i >= 0; i--) {

			//Get the event we are checking
			DelayedEvent call = queue[i];

			//Add the passed time to this event
			call.currentTime += Time.deltaTime;
			//Debug.Log("Event Time: "+call.currentTime + "/" + call.maxTime);

			//Check if this events passed time is greater than its call time
			if (call.currentTime > call.maxTime) {
				//Debug.Log("Firing Event " + call.currentTime + "/"+call.maxTime);
				//Invoke each action in this event.
				foreach (UnityAction action in call.actions) {
					action.Invoke();
				}

				//Remove it from the list
				queue.RemoveAt(i);
			}
		}
	}

	public void FixedGameUpdate() {
		//Unused
	}
	#endregion
	#endregion

	#region Private
	/// <summary>
	/// A small class to contain all of the data required for a delayed event
	/// </summary>
	private class DelayedEvent {

		//The amount of time this event is delayed
		public float maxTime;
		
		//The amount of time this event has been waiting
		public float currentTime;

		//The list of actions to invoke after the time delay
		public UnityAction[] actions;

		public DelayedEvent(float maxTime, UnityAction[] actions) {
			currentTime = 0.0f;
			this.maxTime = maxTime;
			this.actions = actions;
		}
	}
	#endregion
}
