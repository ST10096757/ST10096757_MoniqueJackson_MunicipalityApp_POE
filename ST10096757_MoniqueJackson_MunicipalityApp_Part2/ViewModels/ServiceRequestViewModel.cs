using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2;
using System.ComponentModel;

public class ServiceRequestViewModel : INotifyPropertyChanged
{
	private BinarySearchTree serviceRequestTree;
	private ObservableCollection<ServiceRequest> _filteredServiceRequests;
	private string _searchQuery;
	private string _selectedCategory;
	private string _selectedPriority;
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
			SearchRequests();  // Trigger the search when the query changes
		}
	}

	public string SelectedCategory
	{
		get { return _selectedCategory; }
		set
		{
			_selectedCategory = value;
			OnPropertyChanged(nameof(SelectedCategory));
			FilterRequests();  // Trigger filtering when category changes
		}
	}

	public string SelectedPriority
	{
		get { return _selectedPriority; }
		set
		{
			_selectedPriority = value;
			OnPropertyChanged(nameof(SelectedPriority));
			FilterRequests();  // Trigger filtering when priority changes
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

	// Initialize the service request tree and collections
	public ServiceRequestViewModel()
	{
		serviceRequestTree = new BinarySearchTree(); // Initialize the BST
		_filteredServiceRequests = new ObservableCollection<ServiceRequest>();
		LoadServiceRequests();  // Load requests from JSON file
	}

	// Load Service Requests from the ServiceRequestManager
	public void LoadServiceRequests()
	{
		// Create an instance of ServiceRequestManager
		var serviceRequestManager = new ServiceRequestManager();

		// Load the requests from the JSON file
		var serviceRequests = serviceRequestManager.LoadServiceRequests();

		// Insert each service request into the BinarySearchTree
		foreach (var request in serviceRequests.Values)
		{
			InsertRequest(request);  // Insert into the BST
		}

		FilterRequests();  // Initially apply filtering after data is loaded
	}

	// Insert a service request into the BinarySearchTree
	public void InsertRequest(ServiceRequest request)
	{
		serviceRequestTree.Insert(request);  // Insert into the BST
	}

	// Search requests by RequestId using the BinarySearchTree
	private void SearchRequests()
	{
		var filtered = new List<ServiceRequest>();

		if (int.TryParse(SearchQuery, out int requestId))
		{
			// Search by RequestId in the tree
			var result = serviceRequestTree.Find(requestId);
			if (result != null)
			{
				filtered.Add(result);  // Add to the filtered list if found
			}
		}
		else
		{
			// If the search query isn't a valid integer, show all requests (in sorted order)
			serviceRequestTree.InOrderTraversal(req => filtered.Add(req));
		}

		// Update the ObservableCollection to trigger UI update
		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(filtered);
	}

	// Filter requests based on status and priority
	private void FilterRequests()
	{
		var filtered = new List<ServiceRequest>();

		serviceRequestTree.InOrderTraversal(req =>
		{
			bool matchesCategory = string.IsNullOrEmpty(SelectedCategory) || SelectedCategory == "All" || req.Status == SelectedCategory;
			bool matchesPriority = string.IsNullOrEmpty(SelectedPriority) || SelectedPriority == "All" || req.Priority == SelectedPriority;

			if (matchesCategory && matchesPriority)
			{
				filtered.Add(req);
			}
		});

		// Update the ObservableCollection with the filtered list
		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(filtered);
	}

	// Event for INotifyPropertyChanged
	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
