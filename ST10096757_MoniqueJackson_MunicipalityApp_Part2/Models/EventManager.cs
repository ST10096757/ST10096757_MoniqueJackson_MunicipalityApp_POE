using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	public class EventManager
	{
		private SortedDictionary<DateTime, List<EventsClass>> eventsByDate;
		private HashSet<string> uniqueCategories;
		private HashSet<DateTime> uniqueDates;

		public EventManager()
		{
			eventsByDate = new SortedDictionary<DateTime, List<EventsClass>>();
			uniqueCategories = new HashSet<string>();
			uniqueDates = new HashSet<DateTime>();
		}

		public void AddEvent(EventsClass newEvent)
		{
			if (!eventsByDate.ContainsKey(newEvent.Date))
			{
				eventsByDate[newEvent.Date] = new List<EventsClass>();
				uniqueDates.Add(newEvent.Date); // Add date to unique dates
			}

			uniqueCategories.Add(newEvent.Category); // Add category to unique categories
			eventsByDate[newEvent.Date].Add(newEvent);
		}

		public List<EventsClass> GetEventsByDate(DateTime date)
		{
			if (eventsByDate.TryGetValue(date, out var events))
			{
				return events;
			}
			return new List<EventsClass>();
		}

		public List<EventsClass> GetEventsByCategory(string category)
		{
			var result = new List<EventsClass>();
			foreach (var events in eventsByDate.Values)
			{
				result.AddRange(events.Where(evt => evt.Category.Equals(category, StringComparison.OrdinalIgnoreCase)));
			}
			return result;
		}

		public List<EventsClass> GetAllEvents()
		{
			var allEvents = new List<EventsClass>();
			foreach (var events in eventsByDate.Values)
			{
				allEvents.AddRange(events);
			}
			return allEvents;
		}

		public HashSet<string> GetUniqueCategories()
		{
			return uniqueCategories;
		}

		public HashSet<DateTime> GetUniqueDates()
		{
			return uniqueDates;
		}
	}
}
