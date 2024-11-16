using Newtonsoft.Json;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Managers
{
	public class ServiceRequestManager
	{
		// Update this path to point to the file in the output directory
		private const string FilePath = "Resources\\service_requests.json";  // Relative file path

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
				// Check if the file exists before attempting to load
				if (File.Exists(FilePath))
				{
					// Read the JSON string from the file
					string json = File.ReadAllText(FilePath);

					// Deserialize the JSON into a dictionary of service requests
					var serviceRequests = JsonConvert.DeserializeObject<Dictionary<string, ServiceRequest>>(json);
					return serviceRequests;
				}
				else
				{
					Console.WriteLine("No service requests file found.");
					return new Dictionary<string, ServiceRequest>();  // Return an empty dictionary if the file doesn't exist
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while loading service requests: {ex.Message}");
				return new Dictionary<string, ServiceRequest>();  // Return an empty dictionary in case of errors
			}
		}
	}
}
