using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	public class CustomPriorityQueue<T>
	{
		private SortedList<int, Queue<T>> _list;

		public CustomPriorityQueue()
		{
			_list = new SortedList<int, Queue<T>>();
		}

		public void Enqueue(int priority, T item)
		{
			if (!_list.ContainsKey(priority))
			{
				_list[priority] = new Queue<T>();
			}
			_list[priority].Enqueue(item);
		}

		public T Dequeue()
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

			return item;
		}

		public int Count => _list.Values.Sum(queue => queue.Count);

		public bool IsEmpty() => _list.Count == 0;
	}
}
