using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	public class RecommendationManager
	{
		private Dictionary<string, int> eventRecommendations; // Stores event titles and their recommendation score

		public RecommendationManager()
		{
			eventRecommendations = new Dictionary<string, int>();
		}

		public void AddRecommendation(string eventTitle)
		{
			if (eventRecommendations.ContainsKey(eventTitle))
			{
				eventRecommendations[eventTitle]++;
			}
			else
			{
				eventRecommendations[eventTitle] = 1;
			}
		}

		public List<string> GetTopRecommendations(int count)
		{
			return eventRecommendations.OrderByDescending(x => x.Value)
									   .Take(count)
									   .Select(x => x.Key)
									   .ToList();
		}
	}
}
