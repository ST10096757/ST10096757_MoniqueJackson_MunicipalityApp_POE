using ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views;
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
using System.Windows.Shapes;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	/// <summary>
	/// Interaction logic for WindowController.xaml
	/// </summary>
	public partial class WindowController : Window
	{
		public IssuesViewModel ViewModels { get; private set; }
		public WindowController()
		{
			InitializeComponent();
			ViewModels = new IssuesViewModel();
			LoadUserControl(new ReportIssues()); // Default load
												 
		}

		public void LoadUserControl(UserControl userControl)
		{
			ContentControlHost.Content = userControl;

			if (userControl is ViewIssue_UC viewIssueControl)
			{
				//viewIssueControl.LoadIssues(ViewModels.Issues);
			}
		}



		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if(e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
        }

		private void OpenViewIssue()
		{
			LoadUserControl(new ViewIssue_UC(ViewModels)); // Pass the ViewModels instance
		}

		

	}
}
