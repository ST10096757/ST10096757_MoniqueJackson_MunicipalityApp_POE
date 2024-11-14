using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	// The RecommendationManager class is responsible for managing event recommendations.
	// It tracks the recommendation score for each event and provides the ability to retrieve the most recommended events.
	public class RecommendationManager
	{
		// A dictionary that stores event titles as keys and their corresponding recommendation scores as values.
		private Dictionary<string, int> eventRecommendations;

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Constructor initializes the dictionary for storing event recommendations.
		/// <summary>
		/// Initializes the RecommendationManager with an empty dictionary for event recommendations.
		/// </summary>
		public RecommendationManager()
		{
			// Initialize the dictionary where keys are event titles and values are their recommendation scores.
			eventRecommendations = new Dictionary<string, int>();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Method to add a recommendation for an event.
		/// <summary>
		/// Adds a recommendation to an event. If the event already exists, its recommendation score is incremented.
		/// If it doesn't exist, the event is added with an initial recommendation score of 1.
		/// </summary>
		/// <param name="eventTitle">The title of the event being recommended.</param>
		public void AddRecommendation(string eventTitle)
		{
			// If the event has already been recommended (exists in the dictionary), increment its score
			if (eventRecommendations.ContainsKey(eventTitle))
			{
				eventRecommendations[eventTitle]++;
			}
			// If the event is not in the dictionary, add it with an initial score of 1
			else
			{
				eventRecommendations[eventTitle] = 1;
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Method to retrieve the top recommended events, sorted by their recommendation score.
		/// <summary>
		/// Retrieves the top N recommended events, sorted by their recommendation score in descending order.
		/// </summary>
		/// <param name="count">The number of top recommended events to retrieve.</param>
		/// <returns>A list of event titles, ordered by recommendation score (most recommended first).</returns>
		public List<string> GetTopRecommendations(int count)
		{
			// Sort the dictionary by the recommendation score (value), in descending order,
			// then take the top 'count' events and select their titles (keys).
			return eventRecommendations.OrderByDescending(x => x.Value)   // Sort by score (descending)
									   .Take(count)                      // Take the top 'count' events
									   .Select(x => x.Key)               // Select only the event titles
									   .ToList();                        // Return as a list
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
