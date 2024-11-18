using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.MVVM.Models;
using System;
using System.Collections.Generic;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	// Represents a graph structure for storing service requests and edges between them
	public class Graph
	{
		// Dictionary to hold ServiceRequest objects, indexed by RequestId
		public Dictionary<int, ServiceRequest> Requests { get; set; } = new Dictionary<int, ServiceRequest>();

		// Adjacency list to store edges between nodes, each node points to a list of edges
		public Dictionary<int, List<Edge>> AdjacencyList { get; set; } = new Dictionary<int, List<Edge>>();

		// List to store edges in the graph
		public List<Edge> Edges { get; private set; }
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Contructor
		/// Initializes the graph with empty dictionaries for requests and adjacency list
		/// </summary>
		public Graph()
		{
			AdjacencyList = new Dictionary<int, List<Edge>>();
			Requests = new Dictionary<int, ServiceRequest>();
			Edges = new List<Edge>();
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Adds a new ServiceRequest to the graph if it doesn't already exist
		/// </summary>
		/// <param name="request"></param>
		public void AddServiceRequest(ServiceRequest request)
		{
			if (!Requests.ContainsKey(request.RequestId))
			{
				// Add the request to the dictionary
				Requests.Add(request.RequestId, request);
				Console.WriteLine($"ServiceRequest with ID {request.RequestId} added for {request.ResidentName}");
			}
			else
			{
				// Print a message if the request already exists
				Console.WriteLine($"ServiceRequest with ID {request.RequestId} already exists.");
			}
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Adds an edge between two nodes (start and end) with a specified weight
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="weight"></param>
		public void AddEdge(int start, int end, double weight)
		{
			// If start node is not in the adjacency list, add it
			if (!AdjacencyList.ContainsKey(start))
			{
				AdjacencyList[start] = new List<Edge>();
				Console.WriteLine($"Node {start} added to adjacency list.");
			}

			// If end node is not in the adjacency list, add it
			if (!AdjacencyList.ContainsKey(end))
			{
				AdjacencyList[end] = new List<Edge>();
				Console.WriteLine($"Node {end} added to adjacency list.");
			}

			// Create an edge and add it to both start and end nodes
			var edge = new Edge(start, end, weight);
			AdjacencyList[start].Add(edge);
			AdjacencyList[end].Add(edge);

			Console.WriteLine($"Edge added: {start} -> {end} with weight {weight}");
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to add a service request to the graph if it's not already present
		/// </summary>
		/// <param name="request"></param>
		public void AddRequest(ServiceRequest request)
		{
			if (!Requests.ContainsKey(request.RequestId))
			{
				// Add the request to the requests dictionary
				Requests.Add(request.RequestId, request);
			}
			else
			{
				// Print a message if the request is already in the graph
				Console.WriteLine($"Request {request.RequestId} already in graph.");
			}
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Display the contents of the graph: requests and edges
		/// </summary>
		public void DisplayGraph()
		{
			Console.WriteLine($"Total Requests in Graph: {Requests.Count}");
			// Print all service requests
			foreach (var node in Requests)
			{
				Console.WriteLine($"Request {node.Key}: {node.Value.ResidentName}");
			}

			// Print the adjacency list (edges between nodes)
			foreach (var node in AdjacencyList)
			{
				Console.WriteLine($"Node {node.Key}:");
				foreach (var edge in node.Value)
				{
					Console.WriteLine($"   -> {edge.End} with weight {edge.Weight}");
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Computes the Minimum Spanning Tree (MST) using Prim's Algorithm
		/// </summary>
		/// <returns></returns>
		public List<Edge> ComputeMST()
		{
			var mstEdges = new List<Edge>(); // List to store the edges in the MST
			var visited = new HashSet<int>(); // Set to track visited nodes
			var priorityQueue = new CustomPriorityQueue<Edge, double>(); // Priority queue for edges based on weight

			// Start from an arbitrary node (e.g., node 1)
			int startNode = 1;

			// If the start node doesn't exist, return an empty MST
			if (!AdjacencyList.ContainsKey(startNode))
			{
				Console.WriteLine($"Node {startNode} not found in adjacency list.");
				return mstEdges; // Exit early if start node doesn't exist
			}

			visited.Add(startNode); // Mark the start node as visited

			// Enqueue all edges connected to the start node
			foreach (var edge in AdjacencyList[startNode])
			{
				priorityQueue.Enqueue((int)edge.Weight, edge);
			}

			// Loop through the priority queue, building the MST
			while (priorityQueue.Count > 0)
			{
				// Dequeue the edge with the smallest weight
				var edge = priorityQueue.Dequeue();

				// If the destination node has already been visited, skip it
				if (visited.Contains(edge.End)) continue;

				// Add the edge to the MST and mark the destination node as visited
				mstEdges.Add(edge);
				visited.Add(edge.End);

				// Add all edges connected to the newly visited node to the priority queue
				foreach (var nextEdge in AdjacencyList[edge.End])
				{
					if (!visited.Contains(nextEdge.End))
					{
						priorityQueue.Enqueue((int)nextEdge.Weight, nextEdge);
					}
				}
			}

			return mstEdges; // Return the computed Minimum Spanning Tree edges
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
