using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	public class ServiceRequest
	{
		public int RequestId { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }
		public string Priority { get; set; }
		public DateTime DateSubmitted { get; set; }

		public ServiceRequest(int id, string description, string status, string priority, DateTime date)
		{
			RequestId = id;
			Description = description;
			Status = status;
			Priority = priority;
			DateSubmitted = date;
		}
	}
}
