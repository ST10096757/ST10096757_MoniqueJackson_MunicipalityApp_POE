using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	public class IssuesClass
	{
		/// <summary>
		/// Gets or sets the location of the issue.
		/// </summary>
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets the category of the issue.
		/// </summary>
		public string Category { get; set; }

		/// <summary>
		/// Gets or sets the description of the issue.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the list of file paths attached to the issue.
		/// </summary>
		public List<string> AttachedFiles { get; set; }

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Initializes a new instance of the <see cref="IssuesClass"/> class.
		/// Sets up the AttachedFiles property with a new list to avoid null reference issues.
		/// </summary>
		public IssuesClass()
		{
			// Initialize the list of attached files to ensure it is not null
			AttachedFiles = new List<string>();
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
