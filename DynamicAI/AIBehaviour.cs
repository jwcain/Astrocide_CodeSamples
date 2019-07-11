using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {


	/// <summary>
	/// A class that defines a behaviour within the Astrocide AI system. 
	/// A behaviour operates on a gameobject and enables the GmObj to simulate a behaviour such as movement, shooting, etc. 
	/// The AIBehavior object cannot maintain state, but can store it in its parent Intelligence. 
	/// </summary>
	public abstract class AIBehaviour : ScriptableObject {
		#region Variables
		#region MethodChances
		/// <summary>
		/// The chance that this behaviour's GameUpdate code runs
		/// </summary>
		public float gameUpdateChance = 1.0f;

		/// <summary>
		/// The chance that this behaviour's FixedGameUpdate code runs
		/// </summary>
		public float fixedGameUpdateChance = 1.0f;

		/// <summary>
		/// The chance that this behaviour's OnTriggerEnter code runs
		/// </summary>
		public float triggerEnterChance = 1.0f;

		/// <summary>
		/// The chance that this behaviour's OnTriggerExit code runs
		/// </summary>
		public float triggerExitChance = 1.0f;

		/// <summary>
		/// The chance that this behaviour's OnTriggerStay code runs
		/// </summary>
		public float triggerStayChance = 1.0f;
		#endregion
		
		/// <summary>
		/// A list of behaviours that this Behaviour has access to. Describe more below in class definition.
		/// </summary>
		public SubAIBehaviour[] subAIBehaviours;

		/// <summary>
		/// The types of actions on which this behaviour can act on.
		/// </summary>
		public enum AIBehaviourType { GameUpdate, FixedGameUpdate, TriggerEnter, TriggerExit, TriggerStay }
		#endregion

		#region Methods
		#region Public
		/// <summary>
		/// Tells this behaviour that a certain action has happened.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="type"></param>
		/// <param name="other"></param>
		public void CallAIBehaviour(Intelligence context, AIBehaviourType type, Collider other = null) {
			//First roll chance for if we are acting on this behaviour
			if (Random.value <= GetAIBehaviourChance(type)) {
				//Call the main code for this type of action
				RunAIBehaviour(context, type, other);
				//Check each sub behaviour to see if it needs to be run.
				foreach(SubAIBehaviour b in subAIBehaviours) {
					CallSubBehaviours(context, b, type, other);
				}
			}
		}

		/// <summary>
		/// Creates a copy of this object
		/// </summary>
		/// <returns></returns>
		public AIBehaviour Clone() {
			throw new System.NotSupportedException("Should no longer be cloning stating scripts.");
		}
		#endregion

		#region Private
		/// <summary>
		/// Checks a sub behaviour to see if it perfroms regular updates, and if it does call that behaviour on it.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="b"></param>
		/// <param name="type"></param>
		/// <param name="other"></param>
		private void CallSubBehaviours(Intelligence context, SubAIBehaviour b,AIBehaviourType type, Collider other = null) {
			//Switch between all BehaviourTypes, check if they recieve regular updates, and call them if they do
			switch (type) {
				case AIBehaviourType.FixedGameUpdate:
					if (b.regularFixedUpdate)
						b.behaviour.CallAIBehaviour(context, type, other);
					return;
				case AIBehaviourType.GameUpdate:
					if (b.regularGameUdate)
						b.behaviour.CallAIBehaviour(context, type, other);
					return;
				case AIBehaviourType.TriggerEnter:
					if (b.regularTriggerEnter)
						b.behaviour.CallAIBehaviour(context, type, other);
					return;
				case AIBehaviourType.TriggerExit:
					if (b.regularTriggerExit)
						b.behaviour.CallAIBehaviour(context, type, other);
					return;
				case AIBehaviourType.TriggerStay:
					if (b.regularTriggerStay)
						b.behaviour.CallAIBehaviour(context, type, other);
					return;
				default:
					return;
			}
		}

		/// <summary>
		/// Returns the chance for this behaviour happening based on the type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private float GetAIBehaviourChance(AIBehaviourType type) {
			//Determine the correct behaviour chance based on the type. 
			switch (type) {
				case AIBehaviourType.FixedGameUpdate:
					return fixedGameUpdateChance;
				case AIBehaviourType.GameUpdate:
					return gameUpdateChance;
				case AIBehaviourType.TriggerEnter:
					return triggerEnterChance;
				case AIBehaviourType.TriggerExit:
					return triggerExitChance;
				case AIBehaviourType.TriggerStay:
					return triggerStayChance;
				default:
					return 1.0f;
			}
		}
		/// <summary>
		/// Calls the correct behavoir based on the AIBehaviour type.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="collider"></param>
		private void RunAIBehaviour(Intelligence context, AIBehaviourType type, Collider collider = null) {
			//Call the correct behaviour based on the type
			switch (type) {
				case AIBehaviourType.FixedGameUpdate:
					Behaviour_OnFixedGameUpdate(context);
					return;
				case AIBehaviourType.GameUpdate:
					Behaviour_OnGameUpdate(context);
					return;
				case AIBehaviourType.TriggerEnter:
					if (collider == null) Debug.LogError("Invalid call in a AIBehaviour with Triggers.");
					Behaviour_OnTriggerEnter(context, collider);
					return;
				case AIBehaviourType.TriggerExit:
					if (collider == null) Debug.LogError("Invalid call in a AIBehaviour with Triggers.");
					Behaviour_OnTriggerExit(context, collider);
					return;
				case AIBehaviourType.TriggerStay:
					if (collider == null) Debug.LogError("Invalid call in a AIBehaviour with Triggers.");
					Behaviour_OnTriggerStay(context, collider);
					return;
				default:
					return;
			}
		}
		#endregion

		#region Abstract
		/// <summary>
		/// Implements the OnGameUpdate code for this behaviour
		/// </summary>
		/// <param name="context"></param>
		public abstract void Behaviour_OnGameUpdate(Intelligence context);
		/// <summary>
		/// Implements the OnFixedGameUpdate code for this behaviour
		/// </summary>
		/// <param name="context"></param>
		public abstract void Behaviour_OnFixedGameUpdate(Intelligence context);
		/// <summary>
		/// Implements the OnTriggerEnter code for this behaviour
		/// </summary>
		/// <param name="context"></param>
		/// <param name="other"></param>
		public abstract void Behaviour_OnTriggerEnter(Intelligence context, Collider other);
		/// <summary>
		/// Implements the OnTriggerExit code for this behaviour
		/// </summary>
		/// <param name="context"></param>
		/// <param name="other"></param>
		public abstract void Behaviour_OnTriggerExit(Intelligence context, Collider other);
		/// <summary>
		/// Implements the OnTriggerStay code for this behaviour
		/// </summary>
		/// <param name="context"></param>
		/// <param name="other"></param>
		public abstract void Behaviour_OnTriggerStay(Intelligence context, Collider other);

		/// <summary>
		/// Resets any state that this AIBehaviour may have developed during its runtime.
		/// </summary>
		public abstract void Reset(Intelligence context);
		#endregion
		#endregion


		#region Subclasses
		/// <summary>
		/// Wraps a bahaviour as part of this behaviour, with the ability to disable regular behaviour calls
		/// </summary>
		[System.Serializable]
		public class SubAIBehaviour {
			/// <summary>
			/// The sub behaviour
			/// </summary>
			public AIBehaviour behaviour;

			#region Settings
			/// <summary>
			/// Setting to determine if this sub behaviour receives regual FixedUpdate calls
			/// </summary>
			public bool regularFixedUpdate = false;
			/// <summary>
			/// Setting to determine if this sub behaviour receives regual GameUpdate calls
			/// </summary>
			public bool regularGameUdate = false;
			/// <summary>
			/// Setting to determine if this sub behaviour receives regual TriggerEnter calls
			/// </summary>
			public bool regularTriggerEnter = false;
			/// <summary>
			/// Setting to determine if this sub behaviour receives regual TriggerExit calls
			/// </summary>
			public bool regularTriggerExit = false;
			/// <summary>
			/// Setting to determine if this sub behaviour receives regual TriggerStay calls
			/// </summary>
			public bool regularTriggerStay = false;
			#endregion

			#region Methods
			/// <summary>
			/// Creates a clone of this SubAIBehaviour
			/// </summary>
			/// <returns></returns>
			public SubAIBehaviour Clone() {
				SubAIBehaviour t = new SubAIBehaviour();
				t.behaviour = this.behaviour.Clone();
				t.regularFixedUpdate = this.regularFixedUpdate;
				t.regularGameUdate = this.regularGameUdate;
				t.regularTriggerEnter = this.regularTriggerEnter;
				t.regularTriggerExit = this.regularTriggerExit;
				t.regularTriggerStay = this.regularTriggerStay;
				return t;
			}
			#endregion
		}
		#endregion
	}
}
