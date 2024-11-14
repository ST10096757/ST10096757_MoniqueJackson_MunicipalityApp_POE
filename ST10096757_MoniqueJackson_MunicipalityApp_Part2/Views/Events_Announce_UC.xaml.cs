using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EventManager = ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models.EventManager;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views
{
	/// <summary>
	/// Interaction logic for Events_Announce_UC.xaml
	/// This UserControl displays upcoming events and allows users to search through them.
	/// </summary>
	public partial class Events_Announce_UC : UserControl
	{
		// Declarations to handle events, searches, and notifications
		private EventManager eventManager = new EventManager();
		private EventSearchManager searchManager = new EventSearchManager();
		private NotificationManager notificationManager = new NotificationManager();
		private RecommendationManager recommendationManager = new RecommendationManager();

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Constructor
		/// </summary>
		public Events_Announce_UC()
		{
			InitializeComponent(); // Set up the UI components
			LoadEvents(); // Load initial events into the manager
			DisplayEvents(eventManager.GetAllEvents()); // Show all loaded events in the UI
			LoadUniqueCategories(); // Populate category dropdown
			LoadUniqueDates(); // Populate date dropdown
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Loads predefined events into the EventManager.
		/// </summary>
		private void LoadEvents()
		{
			// Adding events with titles, dates, categories, and descriptions
			// Very bad practice, I know...
			eventManager.AddEvent(new EventsClass { Title = "Community Cleanup", Date = new DateTime(2024, 10, 20), Category = "Volunteer", Description = "Join us for a community cleanup!" });
			eventManager.AddEvent(new EventsClass { Title = "Local Farmer's Market", Date = new DateTime(2024, 10, 25), Category = "Market", Description = "Fresh produce and crafts from local vendors." });
			eventManager.AddEvent(new EventsClass { Title = "Fall Festival", Date = new DateTime(2024, 11, 1), Category = "Festival", Description = "Celebrate the season with fun activities, food, and entertainment." });
			eventManager.AddEvent(new EventsClass { Title = "Art in the Park", Date = new DateTime(2024, 11, 5), Category = "Arts", Description = "Enjoy local artists showcasing their work in a beautiful park setting." });
			eventManager.AddEvent(new EventsClass { Title = "Charity Run", Date = new DateTime(2024, 11, 15), Category = "Sports", Description = "Participate in a charity run to raise funds for local charities." });
			eventManager.AddEvent(new EventsClass { Title = "Book Fair", Date = new DateTime(2024, 11, 20), Category = "Literature", Description = "Browse and purchase books from various genres at our annual book fair." });
			eventManager.AddEvent(new EventsClass { Title = "Winter Concert", Date = new DateTime(2024, 12, 10), Category = "Music", Description = "Enjoy performances by local musicians at our winter concert." });
			eventManager.AddEvent(new EventsClass { Title = "Holiday Parade", Date = new DateTime(2024, 12, 15), Category = "Holiday", Description = "Join us for a festive parade to celebrate the holiday season!" });
			eventManager.AddEvent(new EventsClass { Title = "New Year's Eve Celebration", Date = new DateTime(2024, 12, 31), Category = "Celebration", Description = "Ring in the new year with music, dancing, and fireworks." });
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Displays a list of events in the EventsListBox.
		/// </summary>
		/// <param name="events">List of events to display</param>
		private void DisplayEvents(List<EventsClass> events)
		{
			EventsListBox.Items.Clear(); // Clear the current items
			foreach (var evt in events)
			{
				// Add each event formatted as "Date: Title (Category)"
				EventsListBox.Items.Add($"{evt.Date.ToShortDateString()}: {evt.Title} ({evt.Category})");
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Displays recent searches when the button is clicked.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">Event arguments</param>
		private void ShowRecentSearchesButton_Click(object sender, RoutedEventArgs e)
		{
			// Gets recent searches from the search manager
			var recentSearches = searchManager.GetRecentSearches();

			// Check if there are recent searches
			if (!recentSearches.Any()) 
			{
				MessageBox.Show("You have not searched for any events yet. Please perform a search first.", "No Recent Searches", MessageBoxButton.OK, MessageBoxImage.Information);
				return; // Exit the method early
			}

			// Create a string from the recent searches
			string message = string.Join(Environment.NewLine, recentSearches);

			// Display the recent searches in a message box
			MessageBox.Show("Recent Searches:\n" + message);
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Sends a notification when new events are added
		/// I have no clue if I'm gonna have time to implement this...
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">Event arguments</param>
		private void NotifyButton_Click(object sender, RoutedEventArgs e)
		{
			string message = "New events have been added!"; // Notification message
			notificationManager.AddNotification(message); // Add the notification
			MessageBox.Show(notificationManager.GetLatestNotification()); // Show the latest notification
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Navigates back to the main window when the button is clicked.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">Event arguments</param>
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			// Check if MainActivity is already open
			var existingWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
			if (existingWindow != null)
			{
				existingWindow.Activate(); // Bring the existing window to the front
			}
			else
			{
				MainWindow mainActivity = new MainWindow();
				mainActivity.Show(); // Show the main activity
			}

			// Close the current WindowController
			Window.GetWindow(this)?.Close();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Loads unique categories from the event manager into the category ComboBox.
		/// </summary>
		private void LoadUniqueCategories()
		{
			var uniqueCategories = eventManager.GetUniqueCategories(); // Get unique categories
			foreach (var category in uniqueCategories)
			{
				CategoryComboBox.Items.Add(category); // Add each category to the ComboBox
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Loads unique event dates into the date ComboBox.
		/// </summary>
		private void LoadUniqueDates()
		{
			var uniqueDates = eventManager.GetUniqueDates(); // Get unique dates
			foreach (var date in uniqueDates)
			{
				DateComboBox.Items.Add(date.ToShortDateString()); // Add each date to the ComboBox
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles selection changes in the category and date ComboBoxes.
		/// Disables the other ComboBox based on the selection.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">Event arguments</param>
		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Disable the other ComboBox based on the selected one
			if (sender == CategoryComboBox && CategoryComboBox.SelectedItem != null)
			{
				DateComboBox.IsEnabled = false; // Disable date selection if category is selected
			}
			else if (sender == DateComboBox && DateComboBox.SelectedItem != null)
			{
				CategoryComboBox.IsEnabled = false; // Disable category selection if date is selected
			}
			else
			{
				// If neither is selected, enable both
				CategoryComboBox.IsEnabled = true;
				DateComboBox.IsEnabled = true;
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Starts event search when the search button is clicked.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">Event arguments</param>
		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			SearchEvents(); // Call the method to perform the search
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Searches for events based on selected category and date.
		/// Displays filtered events and updates recent searches.
		/// </summary>
		private void SearchEvents()
		{
			// Check if neither category nor date is selected
			if (CategoryComboBox.SelectedItem == null && DateComboBox.SelectedItem == null)
			{
				MessageBox.Show("Please select at least one category or date to search for events.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return; // Exit the method early
			}

			List<EventsClass> filteredEvents = new List<EventsClass>(); // List to hold filtered events
			string searchQuery = ""; // String to build the search query

			// Check if a category is selected
			if (CategoryComboBox.SelectedItem != null)
			{
				string selectedCategory = CategoryComboBox.SelectedItem.ToString();
				filteredEvents.AddRange(eventManager.GetEventsByCategory(selectedCategory)); // Add events by selected category
				searchQuery += $"Category: {selectedCategory}"; // Append category to search query
			}

			// Check if a date is selected
			if (DateComboBox.SelectedItem != null && DateTime.TryParse(DateComboBox.SelectedItem.ToString(), out DateTime selectedDate))
			{
				filteredEvents.AddRange(eventManager.GetEventsByDate(selectedDate)); // Add events by selected date
				searchQuery += searchQuery.Length > 0 ? ", " : ""; // Add a separator if needed
				searchQuery += $"Date: {selectedDate.ToShortDateString()}"; // Append date to search query
			}

			// Display the filtered events
			DisplayEvents(filteredEvents.Distinct().ToList()); // Use Distinct to avoid duplicates

			// If any events were found, add them to recent searches
			foreach (var evt in filteredEvents)
			{
				searchManager.AddSearch($"{evt.Title} on {evt.Date.ToShortDateString()} ({evt.Category}): {evt.Description}");
				recommendationManager.AddRecommendation(evt.Title);
			}

			// Display recommendations after search
			DisplayRecommendations();
		}

		private void DisplayRecommendations()
		{
			var recommendations = recommendationManager.GetTopRecommendations(5); // Get top 5 recommendations
			RecommendationsListBox.Items.Clear(); // Assume you have a ListBox for recommendations

			foreach (var title in recommendations)
			{
				RecommendationsListBox.Items.Add(title); // Add each recommended event title
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Clears the selections in the ComboBoxes and resets the view.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">Event arguments</param>
		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			// Clear the selections in both ComboBoxes
			CategoryComboBox.SelectedItem = null;
			DateComboBox.SelectedItem = null;

			// Enable both ComboBoxes
			CategoryComboBox.IsEnabled = true;
			DateComboBox.IsEnabled = true;

			// Optionally, display all events
			DisplayEvents(eventManager.GetAllEvents()); // Refresh the event display
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
