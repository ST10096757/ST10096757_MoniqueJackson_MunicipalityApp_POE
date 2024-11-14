using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	// The EventSearchManager class is responsible for managing and storing recent search terms.
	// It allows you to add new searches and retrieve a list of the most recent searches.
	public class EventSearchManager
	{
		// A queue to store recent search terms. It keeps the most recent search terms and removes the oldest ones as new ones are added.
		private Queue<string> recentSearches;

		// Constant that defines the maximum number of recent searches to store.
		private const int MaxRecentSearches = 10;

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Constructor initializes the recent searches queue.
		/// <summary>
		/// Initializes the EventSearchManager and the queue for storing recent searches.
		/// </summary>
		public EventSearchManager()
		{
			// Initialize the queue with a maximum capacity of 10 search terms
			recentSearches = new Queue<string>();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Method to add a new search term to the list of recent searches.
		/// <summary>
		/// Adds a new search term to the recent searches queue.
		/// If the queue has reached the maximum size, the oldest search term will be removed.
		/// </summary>
		/// <param name="searchTerm">The search term to be added to the list of recent searches.</param>
		public void AddSearch(string searchTerm)
		{
			// Check if the queue has reached the maximum number of recent searches
			if (recentSearches.Count >= MaxRecentSearches)
			{
				// Remove the oldest search term (the one at the front of the queue)
				//...now why did I do this again...???
				recentSearches.Dequeue();
			}

			// Add the new search term to the queue (at the end)
			recentSearches.Enqueue(searchTerm);
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Method to retrieve all the recent search terms.
		/// <summary>
		/// Retrieves the list of recent search terms stored in the queue.
		/// </summary>
		/// <returns>An enumerable collection of recent search terms.</returns>
		public IEnumerable<string> GetRecentSearches()
		{
			// Return the queue containing recent search terms
			return recentSearches;
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
