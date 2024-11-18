using System.Collections.Generic;
using System;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	public class ServiceRequest : IComparable<ServiceRequest>
	{
		public int RequestId { get; set; }
		public string ResidentName { get; set; }
		public string ContactDetails { get; set; }
		public string RequestType { get; set; }
		public string Description { get; set; }
		public DateTime SubmissionDate { get; set; }
		public string Status { get; set; }
		public string Priority { get; set; }
		public string AssignedStaff { get; set; }
		public string Location { get; set; }
		public List<string> ProgressLogs { get; set; }

		// Constructor
		public ServiceRequest(int requestId, string residentName, string contactDetails, string requestType,
							  string description, DateTime submissionDate, string status,
							  string priority, string assignedStaff, string location)
		{
			RequestId = requestId;
			ResidentName = residentName;
			ContactDetails = contactDetails;
			RequestType = requestType;
			Description = description;
			SubmissionDate = submissionDate;
			Status = status;
			Priority = priority;
			AssignedStaff = assignedStaff;
			Location = location;
			ProgressLogs = new List<string>();
		}

		// Compare service requests by SubmissionDate
		public int CompareTo(ServiceRequest other)
		{
			if (other == null) return 1;

			return this.SubmissionDate.CompareTo(other.SubmissionDate);
		}
	}
}
