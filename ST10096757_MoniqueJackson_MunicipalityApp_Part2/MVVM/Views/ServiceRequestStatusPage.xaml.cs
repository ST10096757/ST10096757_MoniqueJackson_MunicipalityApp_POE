using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views
{
	/// <summary>
	/// Interaction logic for ServiceRequestStatusPage.xaml
	/// </summary>
	public partial class ServiceRequestStatusPage : UserControl
	{
		private ServiceRequestViewModel _viewModel;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ServiceRequestStatusPage()
		{
			InitializeComponent();
			_viewModel = new ServiceRequestViewModel();
			this.DataContext = _viewModel; // Set the DataContext for binding
		}

		/// <summary>
		/// Search Button Click - Triggers the ViewModel's search functionality
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			// Trigger the ViewModel's search functionality by setting the SearchQuery property
			//_viewModel.SearchQuery = txtRequestId.Text;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
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
}
