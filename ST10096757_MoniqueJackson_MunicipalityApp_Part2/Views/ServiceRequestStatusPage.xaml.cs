using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels;
using System;
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

		public ServiceRequestStatusPage()
		{
			InitializeComponent();
			_viewModel = new ServiceRequestViewModel();
			this.DataContext = _viewModel; // Set the DataContext for binding
			PopulateRequestList();
		}

		// Method to load a user control dynamically
		public void LoadUserControl(UserControl userControl)
		{
			// Assuming there's a container like a Grid or StackPanel to load the UserControl into
			//MyContainer.Children.Clear(); // Clear existing controls (if any)
			//MyContainer.Children.Add(userControl); // Add the new UserControl dynamically
		}

		// Populates the request list in the ListView by binding to the ViewModel's FilteredServiceRequests
		private void PopulateRequestList()
		{
			// The ListView is bound to FilteredServiceRequests automatically
			// so no need to set the ItemsSource manually here anymore.
		}

		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			// Trigger the ViewModel's search functionality by setting the SearchQuery property
			_viewModel.SearchQuery = txtRequestId.Text;

			// You don't need to manually filter the ListView anymore. It will update automatically 
			// when the SearchQuery property changes.
		}

		private void cmbStatusFilter_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			// Trigger the ViewModel's filtering functionality by setting the SelectedCategory
			if (cmbStatusFilter.SelectedItem is ComboBoxItem selectedItem)
			{
				_viewModel.SelectedCategory = selectedItem.Content.ToString();
			}
		}

		//private void btnUpdateStatus_Click(object sender, RoutedEventArgs e)
		//{
		//	if (_viewModel.SelectedRequest != null)
		//	{
		//		// Update the status of the selected request
		//		_viewModel.UpdateRequestStatus(_viewModel.SelectedRequest.RequestId, "In Progress"); // Example status update
		//	}
		//	else
		//	{
		//		MessageBox.Show("Please select a request first.");
		//	}
		//}
	}
}
