using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	public class CustomPriorityQueue<TElement, TPriority>
	{
		private SortedList<int, Queue<TElement>> _list;  // Use TElement instead of T
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Contructor
		/// </summary>
		public CustomPriorityQueue()
		{
			_list = new SortedList<int, Queue<TElement>>();  // Initialize the list of queues
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Enqueue method now accepts TElement
		/// </summary>
		/// <param name="priority"></param>
		/// <param name="item"></param>
		public void Enqueue(int priority, TElement item)
		{
			if (!_list.ContainsKey(priority))
			{
				_list[priority] = new Queue<TElement>();  // Use TElement here
			}
			_list[priority].Enqueue(item);
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Dequeue method now returns TElement
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public TElement Dequeue()
		{
			if (_list.Count == 0)
			{
				throw new InvalidOperationException("Queue is empty.");
			}

			// Get the highest priority (lowest integer key) and dequeue from it
			var highestPriority = _list.Keys[0];
			var item = _list[highestPriority].Dequeue();

			// If the queue for that priority is empty, remove the entry
			if (_list[highestPriority].Count == 0)
			{
				_list.Remove(highestPriority);
			}

			return item;  // Return TElement
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		public int Count => _list.Values.Sum(queue => queue.Count);  // Sum of all items in the queues

		public bool IsEmpty() => _list.Count == 0;
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
