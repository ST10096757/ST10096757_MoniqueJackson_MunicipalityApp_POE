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
			_viewModel.SearchQuery = txtRequestId.Text;
		}

		/// <summary>
		/// Status Filter ComboBox Selection Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmbStatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Trigger the ViewModel's filtering functionality by setting the SelectedCategory
			if (cmbStatusFilter.SelectedItem is ComboBoxItem selectedItem)
			{
				_viewModel.SelectedCategory = selectedItem.Content.ToString();
			}
		}

		/// <summary>
		/// Priority Filter ComboBox Selection Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmbPriorityFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Trigger the ViewModel's filtering functionality by setting the SelectedPriority
			if (cmbPriorityFilter.SelectedItem is ComboBoxItem selectedItem)
			{
				_viewModel.SelectedPriority = selectedItem.Content.ToString();
			}
		}
	}
}
