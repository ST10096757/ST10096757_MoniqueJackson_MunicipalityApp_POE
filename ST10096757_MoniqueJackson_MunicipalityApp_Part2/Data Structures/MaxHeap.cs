using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	// MaxHeap class to manage a heap data structure, prioritizing ServiceRequest objects based on their priority.
	public class MaxHeap
	{
		// Internal list to store the heap elements (ServiceRequest objects)
		private List<ServiceRequest> heap;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public MaxHeap()
		{
			heap = new List<ServiceRequest>();
		}

		/// <summary>
		/// Insert a new request into the heap and reheapify to maintain heap property
		/// </summary>
		/// <param name="request"></param>
		public void Insert(ServiceRequest request)
		{
			// Add the request to the end of the heap
			heap.Add(request);

			// Ensure the heap property is maintained (highest priority at the top)
			HeapifyUp(heap.Count - 1);
		}

		/// <summary>
		/// Extract the highest priority request from the heap (the root of the heap)
		/// </summary>
		/// <returns></returns>
		public ServiceRequest ExtractMax()
		{
			// If the heap is empty, return null
			if (heap.Count == 0)
				return null;

			// Get the top (highest priority) request
			var max = heap[0];

			// Replace the top with the last element in the heap
			heap[0] = heap[heap.Count - 1];

			// Remove the last element (now at the top)
			heap.RemoveAt(heap.Count - 1);

			// Reheapify to restore the heap property after removing the top
			HeapifyDown(0);

			// Return the original max request
			return max;
		}
 
		/// <summary>
		/// Reheapify upwards to maintain the heap property after insertion
		/// </summary>
		/// <param name="index"></param>
		private void HeapifyUp(int index)
		{
			// Continue moving up the heap while the current node is greater than its parent
			while (index > 0 && ComparePriority(heap[index], heap[(index - 1) / 2]) > 0)
			{
				// Swap the current node with its parent
				var temp = heap[index];
				heap[index] = heap[(index - 1) / 2];
				heap[(index - 1) / 2] = temp;

				// Move the index to the parent node and continue the process
				index = (index - 1) / 2;
			}
		}
 
		/// <summary>
		/// Reheapify downwards to restore the heap property after extracting the top
		/// </summary>
		/// <param name="index"></param>
		public void HeapifyDown(int index)
		{
			int left = 2 * index + 1;  // Left child index
			int right = 2 * index + 2; // Right child index
			int largest = index;  // Assume current node is the largest

			// If left child exists and is greater than current largest, update largest
			if (left < heap.Count && ComparePriority(heap[left], heap[largest]) > 0)
				largest = left;

			// If right child exists and is greater than current largest, update largest
			if (right < heap.Count && ComparePriority(heap[right], heap[largest]) > 0)
				largest = right;

			// If the largest is not the current node, swap and reheapify down
			if (largest != index)
			{
				var temp = heap[index];
				heap[index] = heap[largest];
				heap[largest] = temp;

				// Recursively call HeapifyDown to restore the heap property
				HeapifyDown(largest);
			}
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Compare the priority between two requests (higher priority means a larger number)
		/// </summary>
		/// <param name="request1"></param>
		/// <param name="request2"></param>
		/// <returns></returns>
		private int ComparePriority(ServiceRequest request1, ServiceRequest request2)
		{
			// Mapping of priority levels to integer values (High > Medium > Low)
			var priorityOrder = new Dictionary<string, int>
			{
				{ "High", 3 },
				{ "Medium", 2 },
				{ "Low", 1 }
			};

			// Return the difference in priority values
			return priorityOrder[request1.Priority] - priorityOrder[request2.Priority];
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Check if the heap is empty (no requests left)
		/// </summary>
		/// <returns></returns>
		public bool IsEmpty()
		{
			// Returns true if the heap is empty, otherwise false
			return heap.Count == 0;
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
