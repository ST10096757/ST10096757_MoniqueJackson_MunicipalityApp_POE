using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	// The EventManager class is responsible for managing a collection of events.
	// It organises events by date, allows querying events by category or date,
	// and tracks unique categories and dates.
	public class EventManager
	{
		// A dictionary that stores events by their date. Each date maps to a list of events occurring on that date.
		private SortedDictionary<DateTime, List<EventsClass>> eventsByDate;

		// A set to store unique categories of events. This ensures that no duplicate categories are stored.
		private HashSet<string> uniqueCategories;

		// A set to store unique event dates. This ensures that no duplicate dates are stored.
		private HashSet<DateTime> uniqueDates;

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Constructor initializes the data structures for managing events.
		/// <summary>
		/// Initializes the EventManager with empty collections for events, unique categories, and unique dates.
		/// </summary>
		public EventManager()
		{
			// Initialize the SortedDictionary, HashSet for categories, and HashSet for dates
			eventsByDate = new SortedDictionary<DateTime, List<EventsClass>>();
			uniqueCategories = new HashSet<string>();
			uniqueDates = new HashSet<DateTime>();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Method to add a new event to the manager.
		/// <summary>
		/// Adds a new event to the event manager, organising it by date and category.
		/// </summary>
		/// <param name="newEvent">The event to be added.</param>
		public void AddEvent(EventsClass newEvent)
		{
			// If the date does not already exist in the dictionary, add it with an empty list of events
			if (!eventsByDate.ContainsKey(newEvent.Date))
			{
				eventsByDate[newEvent.Date] = new List<EventsClass>();
				// Add the event's date to the set of unique dates
				uniqueDates.Add(newEvent.Date);
			}

			// Add the event's category to the set of unique categories (duplicates will be ignored)
			uniqueCategories.Add(newEvent.Category);

			// Add the event to the list of events for that particular date
			eventsByDate[newEvent.Date].Add(newEvent);
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Method to retrieve all events that occur on a specific date.
		/// <summary>
		/// Retrieves all events for a specific date.
		/// </summary>
		/// <param name="date">The date to search for events.</param>
		/// <returns>A list of events occurring on the specified date.</returns>
		public List<EventsClass> GetEventsByDate(DateTime date)
		{
			// Check if the dictionary contains events for the given date
			if (eventsByDate.TryGetValue(date, out var events))
			{
				// Return the list of events for that date
				return events;
			}
			// Return an empty list if no events exist for the given date
			return new List<EventsClass>();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Method to retrieve all events of a specific category.
		/// <summary>
		/// Retrieves all events for a specific category across all dates.
		/// </summary>
		/// <param name="category">The category to filter events by.</param>
		/// <returns>A list of events belonging to the specified category.</returns>
		public List<EventsClass> GetEventsByCategory(string category)
		{
			// Initialize an empty list to store events of the specified category
			var result = new List<EventsClass>();

			// Iterate over all events in the dictionary, checking each event's category
			foreach (var events in eventsByDate.Values)
			{
				// Add events that match the given category to the result list
				result.AddRange(events.Where(evt => evt.Category.Equals(category, StringComparison.OrdinalIgnoreCase)));
			}

			// Return the list of events for the given category
			return result;
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Method to retrieve all events in the manager.
		/// <summary>
		/// Retrieves all events in the event manager, regardless of date or category.
		/// </summary>
		/// <returns>A list of all events stored in the manager.</returns>
		public List<EventsClass> GetAllEvents()
		{
			// Initialize an empty list to store all events
			var allEvents = new List<EventsClass>();

			// Iterate over all events in the dictionary and add them to the result list
			foreach (var events in eventsByDate.Values)
			{
				allEvents.AddRange(events);
			}

			// Return the complete list of events
			return allEvents;
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Method to retrieve all unique event categories.
		/// <summary>
		/// Retrieves all unique categories of events stored in the event manager.
		/// </summary>
		/// <returns>A set containing all unique categories of events.</returns>
		public HashSet<string> GetUniqueCategories()
		{
			// Return the set of unique categories
			return uniqueCategories;
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Method to retrieve all unique event dates.
		/// <summary>
		/// Retrieves all unique dates of events stored in the event manager.
		/// </summary>
		/// <returns>A set containing all unique dates of events.</returns>
		public HashSet<DateTime> GetUniqueDates()
		{
			// Return the set of unique event dates
			return uniqueDates;
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
