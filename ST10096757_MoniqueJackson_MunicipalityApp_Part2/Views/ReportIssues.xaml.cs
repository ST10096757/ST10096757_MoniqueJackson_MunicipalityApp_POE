using Microsoft.Win32;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views
{
	/// <summary>
	/// Interaction logic for ReportIssues.xaml
	/// </summary>
	public partial class ReportIssues : UserControl
	{
		// ViewModel for binding issues data
		private IssuesViewModel ViewModels => (IssuesViewModel)Application.Current.MainWindow.DataContext;

		// List to store all reported issues
		private List<IssuesClass> issues;

		// Store file paths temporarily
		private List<string> currentAttachedFiles;

		// Gather data from the form
		string location { get; set; }
		string category { get; set; }
		string description { get; set; }

		// Timer for delaying progress updates
		private DispatcherTimer _updateTimer;
		private const int UpdateDelayMilliseconds = 1000; // 1 second delay
		public ReportIssues()
		{
			InitializeComponent();
			issues = new List<IssuesClass>(); // Initialize the list of issues
			currentAttachedFiles = new List<string>(); // Initialize the list for attached files

			// Initialize the timer
			_updateTimer = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(UpdateDelayMilliseconds)
			};
			_updateTimer.Tick += (s, e) => UpdateProgress();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the event when the user clicks the attach media button.
		/// Opens a file dialog to select files and updates the list of attached files.
		/// Restarts the timer to delay progress update.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void AttachMediaButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Title = "Select Media",
				Filter = "All Files (*.*)|*.*",
				Multiselect = true
			};

			if (openFileDialog.ShowDialog() == true)
			{
				// Store the selected file paths
				currentAttachedFiles = new List<string>(openFileDialog.FileNames);

				// Debugging output
				foreach (var file in currentAttachedFiles)
				{
					Debug.WriteLine($"Attached file: {file}");
				}

				// Provide feedback to the user
				MessageBox.Show("Files attached: " + string.Join(", ", currentAttachedFiles));

				// Restart the update timer to delay progress update
				RestartUpdateTimer();
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the event when the text in the location text box changes.
		/// Restarts the timer to delay progress update.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void LocationTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			// Restart the timer to delay progress update
			RestartUpdateTimer();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the event when the selection in the category combo box changes.
		/// Restarts the timer to delay progress update.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Restart the timer to delay progress update
			RestartUpdateTimer();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the event when the text in the description rich text box changes.
		/// Restarts the timer to delay progress update.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void DescriptionRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			// Restart the timer to delay progress update
			RestartUpdateTimer();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Stops and restarts the update timer to delay the progress update.
		/// </summary>
		private void RestartUpdateTimer()
		{
			_updateTimer.Stop();
			_updateTimer.Start();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Updates the progress bar based on the current input state.
		/// Calculates the progress based on location, category, description, and file attachments.
		/// Ensures the progress bar value does not exceed 100%.
		/// </summary>
		private void UpdateProgress()
		{
			int progress = 0;

			// Check if LocationInput is filled
			if (!string.IsNullOrEmpty(LocationTextBox.Text))
			{
				progress += 25; // 25% for Location
			}

			// Check if a category is selected
			if (CategoryComboBox.SelectedItem != null)
			{
				progress += 25; // 25% for Category
			}

			// Check if Description is provided
			TextRange textRange = new TextRange(DescriptionRichTextBox.Document.ContentStart, DescriptionRichTextBox.Document.ContentEnd);
			if (!string.IsNullOrWhiteSpace(textRange.Text))
			{
				progress += 25; // 25% for Description
			}

			// Check if a media attachment is uploaded
			if (currentAttachedFiles.Any())
			{
				progress += 25; // 25% for Media Attachment
			}

			// Ensure the progress bar's value is within the valid range
			ReportingProgressBar.Value = Math.Min(progress, 100); // Max value should be 100%
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the event when the user clicks the submit button.
		/// Gathers data from the form, validates input, creates a new issue, and adds it to the ViewModel.
		/// Displays a message box with the submitted information and clears the form.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			// Gather data from the form
			location = LocationTextBox.Text;
			category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
			description = new TextRange(DescriptionRichTextBox.Document.ContentStart, DescriptionRichTextBox.Document.ContentEnd).Text.Trim();

			// Validate input
			if (string.IsNullOrEmpty(location) || string.IsNullOrEmpty(category) || string.IsNullOrWhiteSpace(description))
			{
				MessageBox.Show("Please fill out all fields.");
				return;
			}

			// Create a new issue and add it to the list
			IssuesClass newIssue = new IssuesClass
			{
				Location = location,
				Category = category,
				Description = description
			};

			// Add file paths to the issue
			newIssue.AttachedFiles.AddRange(currentAttachedFiles);

			// Add the issue to the list of reported issues in the ViewModel
			ViewModels.Issues.Add(newIssue);

			// Clear the media preview ItemsControl
			MediaPreviewItemsControl.Items.Clear();

			// Populate media previews
			foreach (var filePath in currentAttachedFiles)
			{
				MediaPreviewItemsControl.Items.Add(new MediaItem { FilePath = filePath });
			}

			// Set visibility of the media preview
			MediaPreviewItemsControl.Visibility = MediaPreviewItemsControl.Items.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

			// Update the user data display
			LocationTextBlock.Text = location;
			CategoryTextBlock.Text = category;
			DescriptionTextBlock.Text = description;

			// Make the user data section visible
			UserDataStackPanel.Visibility = Visibility.Visible;

			// Clear the form
			ClearForm();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the event when the user clicks the back button.
		/// Shows a confirmation message box if there is unsaved data and navigates back to the menu.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			// Gather data from the form
			location = LocationTextBox.Text;
			category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
			description = new TextRange(DescriptionRichTextBox.Document.ContentStart, DescriptionRichTextBox.Document.ContentEnd).Text.Trim();

			if (!string.IsNullOrEmpty(location) || !string.IsNullOrEmpty(category) || !string.IsNullOrWhiteSpace(description))
			{
				// Show a confirmation message box
				MessageBoxResult result = MessageBox.Show(
					"Are you sure you want to leave without Submitting?\nAny data entered will be lost",
					"Confirm Exit",
					MessageBoxButton.YesNo,
					MessageBoxImage.Question
				);

				// Check if the user clicked 'Yes'
				if (result == MessageBoxResult.Yes)
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
			}
			else
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
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Clears the form fields, attached files list, and updates the progress bar.
		/// </summary>
		private void ClearForm()
		{
			// Clear the form fields
			LocationTextBox.Clear();
			CategoryComboBox.SelectedIndex = -1;
			DescriptionRichTextBox.Document.Blocks.Clear();

			// Clear the attached files list
			currentAttachedFiles.Clear();

			// Update the progress bar to reflect that no files are attached
			UpdateProgress();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Handles the event when the user clicks the view issues button.
		/// Navigates to the ViewIssue_UC user control to display the list of reported issues.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void ViewIssuesButton_Click(object sender, RoutedEventArgs e)
		{
			//I have no clue why this isn't working...

			var parentWindow = Window.GetWindow(this) as WindowController;

			// Pass the IssuesViewModel instance when navigating
			var viewModel = parentWindow?.ViewModels;
			// Gather data from the form
			location = LocationTextBox.Text;
			category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
			description = new TextRange(DescriptionRichTextBox.Document.ContentStart, DescriptionRichTextBox.Document.ContentEnd).Text.Trim();

			if (!string.IsNullOrEmpty(location) || !string.IsNullOrEmpty(category) || !string.IsNullOrWhiteSpace(description))
			{
				// Show a confirmation message box
				MessageBoxResult result = MessageBox.Show(
					"Are you sure you want to leave without submitting?\nAny data entered will be lost",
					"Confirm Exit",
					MessageBoxButton.YesNo,
					MessageBoxImage.Question
				);

				// Check if the user clicked 'Yes'
				if (result == MessageBoxResult.Yes)
				{
					// Navigate to the ViewIssue_UC user control
					parentWindow?.LoadUserControl(new ViewIssue_UC(viewModel));
				}
			}
			else
			{
				// Navigate to the ViewIssue_UC user control directly
				parentWindow?.LoadUserControl(new ViewIssue_UC(viewModel));
			}
		}

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{

        }
	}
}
