using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiscUtil {
	/// <summary>
	/// A class that allows for counting. The count of an unrecognized key defaults to 0 and is added to the counter.
	/// </summary>
	public class Counter<T> {

		/// <summary>
		/// The internal dictionary used for tracking values.
		/// </summary>
		private Dictionary<T, int> internalDictionary = new Dictionary<T, int>();

		/// <summary>
		/// The indexer for this class.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int this[T index] {
			get {
				//If we haven't seen this index, add it to the dictionary
				if (internalDictionary.ContainsKey(index) == false) {
					internalDictionary.Add(index, 0);
				}
				return internalDictionary[index];
			}
			set {
				//If we haven't seen this index, add it to the dictionary
				if (internalDictionary.ContainsKey(index) == false) {
					internalDictionary.Add(index, 0);
				}
				internalDictionary[index] = value;
			}
		}

		/// <summary>
		/// Returns an index of all found keys
		/// </summary>
		/// <returns></returns>
		public T[] GetKeys() {
			List<T> foundKeys = new List<T>();
			foreach (T s in internalDictionary.Keys) {
				foundKeys.Add(s);
			}
			return foundKeys.ToArray();
		}

		/// <summary>
		/// Returns all keys with a value above a threshold
		/// </summary>
		/// <param name="threshold"></param>
		/// <returns></returns>
		public T[] GetKeysAboveThreshold(int threshold = 0) {
			List<T> foundKeys = new List<T>();
			foreach (T s in internalDictionary.Keys) {
				if (internalDictionary[s] > threshold)
					foundKeys.Add(s);
			}
			return foundKeys.ToArray();
		}

		/// <summary>
		/// Returns the total count of all elements in the counter
		/// </summary>
		/// <returns></returns>
		public int GetTotal() {
			int total = 0;
			foreach (T s in internalDictionary.Keys) {
				total += internalDictionary[s];
			}

			return total;
		}


		/// <summary>
		/// Converts the counter to a string representation
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			string toRet =  "Counter \n";
			foreach (T key in internalDictionary.Keys) {
				toRet += key.ToString() + ": " + internalDictionary[key] + "\n";
			}
			return toRet;
		}

		/// <summary>
		/// Convert the counter to a string represenation with all values as a ratio to the total
		/// </summary>
		/// <returns></returns>
		public string ToStringRatio() {
			string toRet = "Counter \n";
			float total = (float)GetTotal();
			foreach (T key in internalDictionary.Keys) {
				toRet += key.ToString() + ": " + (internalDictionary[key] / total) + "\n";
			}
			return toRet;
		}
	}
}