using Newtonsoft.Json;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Managers
{
	public class ServiceRequestManager
	{
		private const string FilePath = "service_requests.json";

		// Method to serialize and save service requests to a file
		public void SaveServiceRequests(Dictionary<string, ServiceRequest> serviceRequests)
		{
			try
			{
				// Convert the dictionary to a JSON string
				string json = JsonConvert.SerializeObject(serviceRequests, Formatting.Indented);

				// Write the JSON string to a file
				File.WriteAllText(FilePath, json);
				Console.WriteLine("Service requests saved successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while saving service requests: {ex.Message}");
			}
		}
	}
}
