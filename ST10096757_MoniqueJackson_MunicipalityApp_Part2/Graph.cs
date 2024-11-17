using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	public class Graph
	{
		public Dictionary<int, ServiceRequest> Requests { get; set; } = new Dictionary<int, ServiceRequest>();
		public List<Edge> Edges { get; set; } = new List<Edge>();
		public Dictionary<int, List<Edge>> AdjacencyList { get; set; }

		// Constructor to initialize the adjacency list
		public Graph()
		{
			AdjacencyList = new Dictionary<int, List<Edge>>();
		}

		// Add an edge between two nodes
		public void AddEdge(int start, int end, double weight)
		{
			if (!AdjacencyList.ContainsKey(start))
			{
				AdjacencyList[start] = new List<Edge>();
			}

			if (!AdjacencyList.ContainsKey(end))
			{
				AdjacencyList[end] = new List<Edge>();
			}

			AdjacencyList[start].Add(new Edge(start, end, weight));
			AdjacencyList[end].Add(new Edge(end, start, weight)); // Undirected graph
		}

		public void DisplayGraph()
		{
			foreach (var node in AdjacencyList)
			{
				Console.WriteLine($"Node {node.Key}:");
				foreach (var edge in node.Value)
				{
					Console.WriteLine($"   -> {edge.End} with weight {edge.Weight}");
				}
			}
		}
		// Compute the Minimum Spanning Tree using Prim's Algorithm
		public List<Edge> ComputeMST()
		{
			var mstEdges = new List<Edge>();
			var visited = new HashSet<int>();
			var priorityQueue = new CustomPriorityQueue<Edge, double>();  // Use CustomPriorityQueue with Edge and double

			// Start from an arbitrary node (let's say node 0)
			int startNode = 1;

			// Ensure the node exists in the adjacency list
			if (AdjacencyList.Count == 0)
			{
				throw new InvalidOperationException("Adjacency list is empty. No nodes in the graph.");
			}

			// Log the contents of the adjacency list
			foreach (var entry in AdjacencyList)
			{
				Console.WriteLine($"Node {entry.Key} has {entry.Value.Count} neighbors.");
			}

			visited.Add(startNode);

			// Enqueue all the edges connected to the start node (0)
			foreach (var edge in AdjacencyList[startNode])
			{
				priorityQueue.Enqueue((int)edge.Weight, edge);  // Enqueue edges with their weights
			}

			// Process the priority queue until all reachable nodes are visited
			while (priorityQueue.Count > 0)
			{
				var edge = priorityQueue.Dequeue();
				if (visited.Contains(edge.End)) continue;  // Skip if the end node is already visited

				mstEdges.Add(edge);  // Add the edge to the MST
				visited.Add(edge.End);  // Mark the end node as visited

				// Add all adjacent edges to the priority queue
				foreach (var nextEdge in AdjacencyList[edge.End])
				{
					if (!visited.Contains(nextEdge.End))
					{
						priorityQueue.Enqueue((int)nextEdge.Weight, nextEdge);  // Enqueue the edge with its weight
					}
				}
			}

			return mstEdges;  // Return the list of edges in the MST
		}

	}
}
