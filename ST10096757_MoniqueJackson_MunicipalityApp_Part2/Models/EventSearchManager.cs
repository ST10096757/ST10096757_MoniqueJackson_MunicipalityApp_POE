using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	public class EventSearchManager
	{
		private Queue<string> recentSearches;
		private const int MaxRecentSearches = 10;

		public EventSearchManager()
		{
			recentSearches = new Queue<string>();
		}

		public void AddSearch(string searchTerm)
		{
			if (recentSearches.Count >= MaxRecentSearches)
			{
				recentSearches.Dequeue(); // Remove the oldest search term
			}
			recentSearches.Enqueue(searchTerm); // Add new search term
		}

		public IEnumerable<string> GetRecentSearches()
		{
			return recentSearches;
		}



	}
}
