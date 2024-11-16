using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	/// <summary>
	/// Store data types that will be used for the POE part of the project
	/// </summary>
	public class ServiceRequest
	{
		public string RequestId { get; set; }             // Unique identifier for the request
		public string ResidentName { get; set; }          // Name of the resident reporting the issue
		public string ContactDetails { get; set; }        // Contact info of the resident
		public string RequestType { get; set; }           // Type of service request (e.g., Water Leak, Road Repair)
		public string Description { get; set; }           // Description of the issue/request
		public DateTime SubmissionDate { get; set; }      // Date when the request was submitted
		public string Status { get; set; }                // Status of the request (e.g., Pending, In Progress, Resolved)
		public string Priority { get; set; }              // Priority (e.g., High, Medium, Low)
		public string AssignedStaff { get; set; }         // Staff assigned to handle the request
		public string Location { get; set; }              // Physical location of the issue
		public List<string> ProgressLogs { get; set; }    // List to store updates about the request status
	}
}
