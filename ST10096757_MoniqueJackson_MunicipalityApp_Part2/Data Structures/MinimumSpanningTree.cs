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

				// If we haven't visited the end node of this edge yet, we add it to the MST
				if (!visited.Contains(edge.End))
				{
					visited.Add(edge.End);  // Mark the end node as visited
					mst.Add(edge);  // Add the edge to the MST

					// Add all the adjacent edges to the priority queue
					foreach (var nextEdge in graph.AdjacencyList[edge.End])
					{
						if (!visited.Contains(nextEdge.End))  // Only consider unvisited nodes
						{
							priorityQueue.Enqueue((int)nextEdge.Weight, nextEdge);  // Enqueue the edge with its weight as the priority
						}
					}
				}
			}

			return mst;  // Return the list of edges that form the MST
		}
	}
}
