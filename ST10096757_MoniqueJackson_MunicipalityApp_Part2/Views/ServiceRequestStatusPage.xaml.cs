using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels;
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
			MyContainer.Children.Clear(); // Clear existing controls (if any)
			MyContainer.Children.Add(userControl); // Add the new UserControl dynamically
		}

		private void PopulateRequestList()
		{
			// Bind the DataGrid to the ServiceRequests list from the ViewModel
			dataGridRequests.ItemsSource = _viewModel.ServiceRequests;
		}

		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			if (int.TryParse(txtRequestId.Text, out int requestId))
			{
				// Use the dictionary to find the request
				if (_viewModel.ServiceRequests.TryGetValue(requestId, out var request))
				{
					lblStatus.Content = $"Status: {request.Status}";
					dataGridRequests.ItemsSource = new[] { request };
				}
				else
				{
					MessageBox.Show("Request ID not found.");
				}
			}
			else
			{
				MessageBox.Show("Invalid Request ID.");
			}
		}

		private void btnFilterByStatus_Click(object sender, RoutedEventArgs e)
		{
			string statusFilter = ((ComboBoxItem)cmbStatusFilter.SelectedItem)?.Content.ToString();
			if (statusFilter != "All")
			{
				// Filter the requests by status
				var filteredRequests = _viewModel.ServiceRequests.Values
					.Where(r => r.Status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase))
					.ToList();
				dataGridRequests.ItemsSource = filteredRequests;
			}
			else
			{
				PopulateRequestList();
			}
		}

		private void btnUpdateStatus_Click(object sender, RoutedEventArgs e)
		{
			if (dataGridRequests.SelectedItem is ServiceRequest selectedRequest)
			{
				// Update the status of the selected request
				_viewModel.UpdateRequestStatus(selectedRequest.RequestId, "In Progress"); // Example status update
				PopulateRequestList(); // Refresh the DataGrid
			}
			else
			{
				MessageBox.Show("Please select a request first.");
			}
		}

	}
}
