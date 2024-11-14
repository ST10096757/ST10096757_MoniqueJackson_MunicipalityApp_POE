using System;
using System.Windows;
using MaterialDesignThemes.Wpf; 
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels; 
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// This class represents the main window of the application.
	/// </summary>
	public partial class MainWindow : Window
	{
		public bool isDarkTheme { get; set; } // Property to track the current theme (dark or light)
		private readonly PaletteHelper paletteHelper = new PaletteHelper(); // Helper for managing themes

		/// <summary>
		/// Gets the view model that holds the data and logic for issues.
		/// </summary>
		public IssuesViewModel ViewModels { get; private set; }

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default constructor
		/// Initializes the window and sets up the ViewModel.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent(); // Initialize UI components
								   // Initialize the IssuesViewModel and set it as the DataContext for data binding
			ViewModels = new IssuesViewModel();
			DataContext = ViewModels;
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the click event for the theme toggle button.
		/// Toggles between light and dark themes.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">Event arguments</param>
		private void themeToggle_Click(object sender, RoutedEventArgs e)
		{
			ITheme theme = paletteHelper.GetTheme(); // Get the current theme

			// Check if the current theme is dark
			if (isDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
			{
				isDarkTheme = false; // Set the flag to indicate light theme
				theme.SetBaseTheme(Theme.Light); // Change to light theme
			}
			else
			{
				isDarkTheme = true; // Set the flag to indicate dark theme
				theme.SetBaseTheme(Theme.Dark); // Change to dark theme
			}

			paletteHelper.SetTheme(theme); // Apply the new theme
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the click event for the Bill of Rights button.
		/// Opens the PDF document for the Bill of Rights.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">Event arguments</param>
		private void btn_billOfRigh_Click(object sender, RoutedEventArgs e)
		{
			string pdfFileName = "BillOfRights.pdf"; // The name of your PDF file
			string pdfPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pdfFileName); // Construct the file path

			// Start a process to open the PDF file
			System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
			{
				FileName = pdfPath, // Path to the PDF file
				UseShellExecute = true // Use the operating system shell to open the file
			});
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the click event for the exit button.
		/// Closes the application.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">Event arguments</param>
		private void btn_exit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown(); // Shut down the application
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the click event for the Report Issues button.
		/// Opens the Report Issues user control in a new window.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">Event arguments</param>
		private void btn_Issue_Click(object sender, RoutedEventArgs e)
		{
			WindowController windowController = new WindowController(); // Create a new window controller
			windowController.LoadUserControl(new ReportIssues()); // Load the Report Issues user control
			windowController.Show(); // Display the window
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the click event for the Events and Announcements button.
		/// Opens the Events user control in a new window.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">Event arguments</param>
		private void btn_Event_Click(object sender, RoutedEventArgs e)
		{
			WindowController windowController = new WindowController(); // Create a new window controller
			windowController.LoadUserControl(new Events_Announce_UC()); // Load the Events user control
			windowController.Show(); // Display the window
		}

		private void btn_Emergency_Click(object sender, RoutedEventArgs e)
		{
			EmergencyContactsWindow contactsWindow = new EmergencyContactsWindow();
			contactsWindow.ShowDialog();
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
