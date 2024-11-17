using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	public class MinimumSpanningTree
	{
		public List<Tuple<int, int, int>> GenerateMST(ServiceRequestGraph graph, int startRequestId)
		{
			var mst = new List<Tuple<int, int, int>>();  // List of edges (request1, request2, weight)
			var visited = new HashSet<int>();
			var priorityQueue = new CustomPriorityQueue<Tuple<int, int, int>>();  // Use the custom priority queue

			visited.Add(startRequestId);
			foreach (var neighbor in graph.GetAdjacentRequests(startRequestId))
			{
				priorityQueue.Enqueue(1, Tuple.Create(1, startRequestId, neighbor)); // Add edges with arbitrary weight 1
			}

			while (priorityQueue.Count > 0)
			{
				var edge = priorityQueue.Dequeue();
				var weight = edge.Item1;
				var request1 = edge.Item2;
				var request2 = edge.Item3;

				if (!visited.Contains(request2))
				{
					visited.Add(request2);
					mst.Add(edge);

					foreach (var neighbor in graph.GetAdjacentRequests(request2))
					{
						if (!visited.Contains(neighbor))
						{
							priorityQueue.Enqueue(1, Tuple.Create(1, request2, neighbor));
						}
					}
				}
			}

			return mst;
		}
	}
}
