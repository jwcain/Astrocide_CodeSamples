﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiscUtil {

	/// <summary>
	/// A Queue with limited size.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Buffer<T> : Queue<T> {
		
		/// <summary>
		/// Capacity of the buffer
		/// </summary>
		private int? MaxCapacity { get; set; }

		public Buffer() { MaxCapacity = null; }
		public Buffer(int capacity) { MaxCapacity = capacity; }

		/// <summary>
		/// Adds an element to the buffer
		/// </summary>
		/// <param name="newElement"></param>
		public void Add(T newElement) {
			if (this.Count == (MaxCapacity ?? -1)) this.Dequeue(); // no limit if maxCapacity = null
			this.Enqueue(newElement);
		}
	}
}