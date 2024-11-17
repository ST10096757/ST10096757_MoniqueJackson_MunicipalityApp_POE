using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
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

		// Add an edge between two service requests based on priority
		public void AddEdge(ServiceRequest req1, ServiceRequest req2, double priorityWeight)
		{
			Edges.Add(new Edge(req1.RequestId, req2.RequestId, priorityWeight));
		}

		public List<Edge> FindMinimumSpanningTree()
		{
			// Implement MST (use Kruskal's or Prim's algorithm)
			return new List<Edge>(); // Return MST edges
		}
	}
	//I should probably move this...
	public class Edge
	{
		public int Start { get; set; }
		public int End { get; set; }
		public double Cost { get; set; }

		public Edge(int start, int end, double cost)
		{
			Start = start;
			End = end;
			Cost = cost;
		}
	}
}
