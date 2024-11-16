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
		private const string FilePath = @"service_requests.json"; // Relative to the application's directory



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

		// Method to load service requests from a file (deserialization)
		public Dictionary<string, ServiceRequest> LoadServiceRequests()
		{
			try
			{
				if (File.Exists(FilePath))
				{
					string json = File.ReadAllText(FilePath);
					var serviceRequests = JsonConvert.DeserializeObject<Dictionary<string, ServiceRequest>>(json);
					return serviceRequests;
				}
				else
				{
					Console.WriteLine("File does not exist.");
					return new Dictionary<string, ServiceRequest>();
				}
			}
			catch (UnauthorizedAccessException)
			{
				Console.WriteLine("Access denied: You do not have permission to access the file.");
				return new Dictionary<string, ServiceRequest>();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return new Dictionary<string, ServiceRequest>();
			}
		}

	}
}
