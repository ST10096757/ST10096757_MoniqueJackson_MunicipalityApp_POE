using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	public class ServiceRequestGraph
	{
		private Dictionary<int, List<int>> adjacencyList;

		public ServiceRequestGraph()
		{
			adjacencyList = new Dictionary<int, List<int>>();
		}

		// Add an edge between two service requests (representing a dependency or relationship)
		public void AddEdge(int requestId1, int requestId2)
		{
			if (!adjacencyList.ContainsKey(requestId1))
				adjacencyList[requestId1] = new List<int>();

			if (!adjacencyList.ContainsKey(requestId2))
				adjacencyList[requestId2] = new List<int>();

			adjacencyList[requestId1].Add(requestId2);
			adjacencyList[requestId2].Add(requestId1);  // Assuming undirected graph (no direction)
		}

		// Get the adjacent requests (i.e., the related service requests for a given request ID)
		public List<int> GetAdjacentRequests(int requestId)
		{
			return adjacencyList.ContainsKey(requestId) ? adjacencyList[requestId] : new List<int>();
		}

		// Perform DFS traversal from a given node (request)
		public void DFS(int startRequestId, Action<int> visit)
		{
			var visited = new HashSet<int>();
			DFSUtil(startRequestId, visited, visit);
		}

		private void DFSUtil(int requestId, HashSet<int> visited, Action<int> visit)
		{
			visited.Add(requestId);
			visit(requestId);

			foreach (var neighbor in GetAdjacentRequests(requestId))
			{
				if (!visited.Contains(neighbor))
				{
					DFSUtil(neighbor, visited, visit);
				}
			}
		}
	}
}
