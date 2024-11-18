using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.MVVM.Models;
using System;
using System.Collections.Generic;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	public class Graph
	{
		public Dictionary<int, ServiceRequest> Requests { get; set; } = new Dictionary<int, ServiceRequest>();
		public Dictionary<int, List<Edge>> AdjacencyList { get; set; } = new Dictionary<int, List<Edge>>();
		public List<Edge> Edges { get; private set; }

		// Constructor
		public Graph()
		{
			AdjacencyList = new Dictionary<int, List<Edge>>();
			Requests = new Dictionary<int, ServiceRequest>();
			Edges = new List<Edge>();
		}

		// Add a new ServiceRequest to the Graph
		public void AddServiceRequest(ServiceRequest request)
		{
			if (!Requests.ContainsKey(request.RequestId))
			{
				Requests.Add(request.RequestId, request);
				Console.WriteLine($"ServiceRequest with ID {request.RequestId} added for {request.ResidentName}");
			}
			else
			{
				Console.WriteLine($"ServiceRequest with ID {request.RequestId} already exists.");
			}
		}

		// Add an edge between two nodes (associates ServiceRequest with edge)
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

			var edge = new Edge(start, end, weight);
			AdjacencyList[start].Add(edge);
			AdjacencyList[end].Add(edge); // If it's undirected

			Console.WriteLine($"Edge added: {start} -> {end} with weight {weight}");
		}
		// Method to add a service request to the graph
		public void AddRequest(ServiceRequest request)
		{
			if (!Requests.ContainsKey(request.RequestId))
			{
				Requests.Add(request.RequestId, request);
			}
		}

		// Display the graph
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

		// Compute Minimum Spanning Tree (MST)
		public List<Edge> ComputeMST()
		{
			var mstEdges = new List<Edge>();
			var visited = new HashSet<int>();
			var priorityQueue = new CustomPriorityQueue<Edge, double>();

			// Start from an arbitrary node (for example, node 1)
			int startNode = 1;
			visited.Add(startNode);

			foreach (var edge in AdjacencyList[startNode])
			{
				priorityQueue.Enqueue((int)edge.Weight, edge);
			}

			while (priorityQueue.Count > 0)
			{
				var edge = priorityQueue.Dequeue();
				if (visited.Contains(edge.End)) continue;

				mstEdges.Add(edge);
				visited.Add(edge.End);

				foreach (var nextEdge in AdjacencyList[edge.End])
				{
					if (!visited.Contains(nextEdge.End))
					{
						priorityQueue.Enqueue((int)nextEdge.Weight, nextEdge);
					}
				}
			}

			return mstEdges;
		}
	}
}
