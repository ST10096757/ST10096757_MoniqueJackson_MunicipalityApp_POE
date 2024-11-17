using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// Interaction logic for ViewIssue_UC.xaml
	/// </summary>
	public partial class ViewIssue_UC : UserControl
	{
		private IssuesViewModel _viewModel;

		public ViewIssue_UC(IssuesViewModel viewModel)
		{
			InitializeComponent();
			_viewModel = viewModel;
			DataContext = _viewModel; // Set the DataContext for binding
			LoadIssues(); // Load issues from the ViewModel
		}

		private void LoadIssues()
		{
			// Access the Issues property directly via DataContext
			if (_viewModel != null)
			{
				IssuesListBox.ItemsSource = _viewModel.Issues; // Bind the ListBox to the list of issues
			}
			else
			{
				// Show an error message if the view model is not set
				MessageBox.Show("ViewModel is null.");
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			var parentWindow = Window.GetWindow(this) as WindowController;
			parentWindow?.LoadUserControl(new ReportIssues());
		}
	}
}


