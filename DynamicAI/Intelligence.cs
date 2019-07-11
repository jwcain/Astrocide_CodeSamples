using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
	/// <summary>
	/// A collection of behaviours that works on the GameUpdate system.
	/// </summary>
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(Collider))]
	public class Intelligence : MonoBehaviour, IGameUpdate, IPrefabPoolable {

		#region Values 
		/// <summary>
		/// The list of behaviours that run this Intelligence
		/// </summary>
		public AIBehaviour[] behaviours;

		/// <summary>
		/// The name this intelligence will identify as.
		/// </summary>
		public string intelligenceName;

		/// <summary>
		/// Stores this Intelligence prefab pooler, to be able to comply with IPrefabPoolable
		/// </summary>
		private PrefabPooler pooler;

		/// <summary>
		/// The tracked internal data for this intelligence
		/// </summary>
		private Dictionary<System.Type, Dictionary<string, System.Object>> internalData = new Dictionary<System.Type, Dictionary<string, object>>();
		#endregion

		#region Methods
		/// <summary>
		/// Calls game update on all behaviours
		/// </summary>
		public void GameUpdate() {
			foreach (AIBehaviour b in behaviours)
				b.CallAIBehaviour(this, AIBehaviour.AIBehaviourType.GameUpdate);
		}

		/// <summary>
		/// Calls fixed game update on all behaviours
		/// </summary>
		public void FixedGameUpdate() {
			foreach (AIBehaviour b in behaviours)
				b.CallAIBehaviour(this, AIBehaviour.AIBehaviourType.FixedGameUpdate);
		}

		/// <summary>
		/// Finds all behaviours in this intelligence of the specified type. Does not search sub behaviours
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public AIBehaviour[] FindBehaviours(System.Type type) {
			List<AIBehaviour> foundBehaviours = new List<AIBehaviour>();
			//Loop through all behaviours, and check if they are of the provided type or derive from the provided type.
			foreach (AIBehaviour b in behaviours)
				if (type.IsAssignableFrom(b.GetType()))
					foundBehaviours.Add(b);

			//Return the found behavious as an array
			return foundBehaviours.ToArray();
		}

		/// <summary>
		/// Resets the internal data of this Intelligence
		/// </summary>
		public void ResetData() {
			//Have all of the behaviours undo what then need to.
			foreach (AIBehaviour b in behaviours)
				b.Reset(this);

			//Wipe the data.
			internalData = new Dictionary<System.Type, Dictionary<string, object>>();
		}

		/// <summary>
		/// Sets a value into internal storage via a type and key
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void SetData<T>(string key, T value) {
			if (internalData.ContainsKey(typeof(T)) == false) {
				internalData.Add(typeof(T), new Dictionary<string,object>());
			}
			internalData[typeof(T)][key] = (object)value;
		}

		/// <summary>
		/// Access a value from storage via a type and key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool GetData<T>(string key, out T output) {
			output = default(T);
			if (typeof(T) == null)
				return false;

			if (internalData.ContainsKey(typeof(T)) == false) {
				return false;
			}
			else {
				Dictionary<string, object> foundDict = internalData[typeof(T)];
				if (foundDict.ContainsKey(key) == false) {
					return false;
				}
				else {
					output = (T)foundDict[key];
					return true;
				}
			}
		}

		/// <summary>
		/// Stores the provided pooler as the owner of this PoolableObject.
		/// </summary>
		/// <param name="pooler"></param>
		void IPrefabPoolable.SetPooler(PrefabPooler pooler) {
			this.pooler = pooler;
		}

		/// <summary>
		/// Returns this PoolableObject's pooler
		/// </summary>
		/// <returns></returns>
		PrefabPooler IPrefabPoolable.GetPooler() {
			return pooler;
		}

		#region MonoBehaviour
		/// <summary>
		/// Calls TriggerEnter on all behaviours
		/// </summary>
		public void OnTriggerEnter(Collider other) {
			foreach (AIBehaviour b in behaviours)
				b.CallAIBehaviour(this, AIBehaviour.AIBehaviourType.TriggerEnter, other);
		}

		/// <summary>
		/// Calls TriggerExit update on all behaviours
		/// </summary>
		public void OnTriggerExit(Collider other) {
			foreach (AIBehaviour b in behaviours)
				b.CallAIBehaviour(this, AIBehaviour.AIBehaviourType.TriggerExit, other);

		}

		/// <summary>
		/// Calls TriggerStay update on all behaviours
		/// </summary>
		public void OnTriggerStay(Collider other) {
			foreach (AIBehaviour b in behaviours)
				b.CallAIBehaviour(this, AIBehaviour.AIBehaviourType.TriggerStay, other);
		}
		#endregion
		#endregion

	}
}