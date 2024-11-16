using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Managers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Linq;

public class ServiceRequestViewModel : INotifyPropertyChanged
{
	// Change the key type to string
	private Dictionary<string, ServiceRequest> _serviceRequests;
	private ObservableCollection<ServiceRequest> _filteredServiceRequests;
	private string _searchQuery;
	private string _selectedCategory;
	private ServiceRequest _selectedRequest;

	// Properties for data binding
	public ObservableCollection<ServiceRequest> FilteredServiceRequests
	{
		get { return _filteredServiceRequests; }
		set
		{
			_filteredServiceRequests = value;
			OnPropertyChanged(nameof(FilteredServiceRequests));
		}
	}

	public string SearchQuery
	{
		get { return _searchQuery; }
		set
		{
			_searchQuery = value;
			OnPropertyChanged(nameof(SearchQuery));
			FilterRequests(); // Re-filter when search query changes
		}
	}

	public string SelectedCategory
	{
		get { return _selectedCategory; }
		set
		{
			_selectedCategory = value;
			OnPropertyChanged(nameof(SelectedCategory));
			FilterRequests(); // Re-filter when category changes
		}
	}

	public ServiceRequest SelectedRequest
	{
		get { return _selectedRequest; }
		set
		{
			_selectedRequest = value;
			OnPropertyChanged(nameof(SelectedRequest));
		}
	}

	public bool IsRequestSelected => SelectedRequest != null;

	public List<string> Categories { get; set; }

	// Constructor
	public ServiceRequestViewModel()
	{
		_serviceRequests = new Dictionary<string, ServiceRequest>(); // Use string as the key
		_filteredServiceRequests = new ObservableCollection<ServiceRequest>();
		Categories = new List<string> { "All", "Pending", "In Progress", "Completed", "High", "Medium", "Low" };

		// Initialize with data from the JSON
		InitializeServiceRequests();
	}

	private void InitializeServiceRequests()
	{
		// Load service requests from the file
		var serviceRequestManager = new ServiceRequestManager();
		var loadedRequests = serviceRequestManager.LoadServiceRequests(); // Returns Dictionary<string, ServiceRequest>

		// Populate the dictionary with the loaded requests
		foreach (var request in loadedRequests)
		{
			_serviceRequests[request.Key] = request.Value;
		}

		// Initially show all requests
		FilterRequests();
	}

	// Filter requests based on search query and selected category
	private void FilterRequests()
	{
		var filtered = _serviceRequests.Values.AsEnumerable(); // Use Values to get all ServiceRequest objects

		// Filter by search query (case-insensitive)
		if (!string.IsNullOrEmpty(SearchQuery))
		{
			filtered = filtered.Where(r => r.Description.IndexOf(SearchQuery, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		// Filter by category (status or priority)
		if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "All")
		{
			filtered = filtered.Where(r => r.Status == SelectedCategory || r.Priority == SelectedCategory);
		}

		// Update the filtered list
		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(filtered);
	}

	// Event for INotifyPropertyChanged
	public event PropertyChangedEventHandler PropertyChanged;
	protected virtual void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}

