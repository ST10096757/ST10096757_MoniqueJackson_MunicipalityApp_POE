using ST10096757_MoniqueJackson_MunicipalityApp_Part2.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	public class MinimumSpanningTree
	{
		// Generate the Minimum Spanning Tree (MST) using Prim's Algorithm
		public List<Edge> GenerateMST(Graph graph, int startRequestId)
		{
			var mst = new List<Edge>();  // List of edges that will form the MST
			var visited = new HashSet<int>();  // Keep track of visited nodes
			var priorityQueue = new CustomPriorityQueue<Edge, double>();  // Custom priority queue for edges

			// Start from the initial request ID
			visited.Add(startRequestId);

			// Add all the edges of the start request to the priority queue
			foreach (var edge in graph.AdjacencyList[startRequestId])
			{
				priorityQueue.Enqueue((int)edge.Weight, edge);  // Use the edge's weight as the priority
			}

			// Perform Prim's Algorithm
			while (priorityQueue.Count > 0)
			{
				var edge = priorityQueue.Dequeue();  // Get the edge with the minimum weight

				if (!visited.Contains(edge.End))
				{
					visited.Add(edge.End);
					mst.Add(edge);  // Add the edge to the MST
					foreach (var nextEdge in graph.AdjacencyList[edge.End])
					{
						if (!visited.Contains(nextEdge.End))
						{
							priorityQueue.Enqueue((int)nextEdge.Weight, nextEdge);  // Enqueue the edge with its weight
						}
					}
				}
			}
			return mst;  // Return the list of edges that form the MST
		}
	}
}
