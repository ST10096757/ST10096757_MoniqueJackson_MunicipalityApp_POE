using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels
{
	public class IssuesViewModel
	{
		/// <summary>
		/// Gets or sets the list of issues.
		/// Holds all the reported issues and is used to share data between different views and controls in the application.
		/// </summary>
		public ObservableCollection<IssuesClass> Issues { get; set; } = new ObservableCollection<IssuesClass>();
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
