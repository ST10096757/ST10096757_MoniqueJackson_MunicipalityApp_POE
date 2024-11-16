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
	private MaxHeap priorityQueue;
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
		priorityQueue = new MaxHeap(); // Initialize the MaxHeap
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

		// Insert each service request into both the BinarySearchTree and the MaxHeap
		foreach (var request in serviceRequests.Values)
		{
			InsertRequestIntoTree(request);  // Insert into the BST
			InsertRequestIntoHeap(request);  // Insert into the MaxHeap
		}

		FilterRequests();  // Initially apply filtering after data is loaded
	}

	// Insert a service request into the BinarySearchTree
	public void InsertRequestIntoTree(ServiceRequest request)
	{
		serviceRequestTree.Insert(request);  // Insert into the BST
	}

	// Insert a service request into the MaxHeap
	public void InsertRequestIntoHeap(ServiceRequest request)
	{
		priorityQueue.Insert(request);  // Insert into the MaxHeap
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

		// Clone the heap and process the elements one by one
		var tempHeap = new MaxHeap();

		// Copy all elements to a temporary heap (so we don't modify the original heap during extraction)
		while (!priorityQueue.IsEmpty())
		{
			var topRequest = priorityQueue.ExtractMax();  // Extract the highest priority request
			filtered.Add(topRequest);  // Add to the filtered list
			tempHeap.Insert(topRequest);  // Reinsert into the temporary heap
		}

		// Filter based on category and priority
		var filteredRequests = filtered.Where(req =>
		{
			bool matchesCategory = string.IsNullOrEmpty(SelectedCategory) || SelectedCategory == "All" || req.Status == SelectedCategory;
			bool matchesPriority = string.IsNullOrEmpty(SelectedPriority) || SelectedPriority == "All" || req.Priority == SelectedPriority;
			return matchesCategory && matchesPriority;
		}).ToList();

		// Update the ObservableCollection with the filtered list
		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(filteredRequests);
	}

	// Extract the highest priority service request from the heap
	public ServiceRequest GetTopPriorityRequest()
	{
		return priorityQueue.ExtractMax();  // Get the highest priority request from the heap
	}

	// Event for INotifyPropertyChanged
	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
