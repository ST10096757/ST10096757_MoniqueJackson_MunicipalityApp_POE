using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	public class MaxHeap
	{
		private List<ServiceRequest> heap;

		public MaxHeap()
		{
			heap = new List<ServiceRequest>();
		}

		// Insert a new request into the heap
		public void Insert(ServiceRequest request)
		{
			heap.Add(request);
			HeapifyUp(heap.Count - 1);
		}

		// Extract the highest priority (top) request
		public ServiceRequest ExtractMax()
		{
			if (heap.Count == 0)
				return null;

			var max = heap[0];
			heap[0] = heap[heap.Count - 1];
			heap.RemoveAt(heap.Count - 1);
			HeapifyDown(0);
			return max;
		}

		private void HeapifyUp(int index)
		{
			while (index > 0 && ComparePriority(heap[index], heap[(index - 1) / 2]) > 0)
			{
				var temp = heap[index];
				heap[index] = heap[(index - 1) / 2];
				heap[(index - 1) / 2] = temp;
				index = (index - 1) / 2;
			}
		}

		private void HeapifyDown(int index)
		{
			int left = 2 * index + 1;
			int right = 2 * index + 2;
			int largest = index;

			if (left < heap.Count && ComparePriority(heap[left], heap[largest]) > 0)
				largest = left;
			if (right < heap.Count && ComparePriority(heap[right], heap[largest]) > 0)
				largest = right;

			if (largest != index)
			{
				var temp = heap[index];
				heap[index] = heap[largest];
				heap[largest] = temp;
				HeapifyDown(largest);
			}
		}

		// Compare priority between two requests
		private int ComparePriority(ServiceRequest request1, ServiceRequest request2)
		{
			var priorityOrder = new Dictionary<string, int>
			{
				{ "High", 3 },
				{ "Medium", 2 },
				{ "Low", 1 }
			};

			return priorityOrder[request1.Priority] - priorityOrder[request2.Priority];
		}

		public bool IsEmpty()
		{
			return heap.Count == 0;
		}
	}
}
