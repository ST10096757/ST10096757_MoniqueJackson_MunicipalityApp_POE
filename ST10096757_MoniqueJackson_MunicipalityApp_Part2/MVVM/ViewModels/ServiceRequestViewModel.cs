using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2;

public class ServiceRequestViewModel : INotifyPropertyChanged
{
	private Graph _serviceRequestGraph;
	private List<Edge> _mstEdges;

	private BinarySearchTree serviceRequestTree;
	private MaxHeap priorityQueue;
	private ObservableCollection<ServiceRequest> _filteredServiceRequests;
	private string _searchQuery;
	private string _selectedCategory;
	private string _selectedPriority;
	private ServiceRequest _selectedRequest;
	private ServiceRequestGraph serviceRequestGraph;  // New graph to store request relationships

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

	// Selected Category - filters by status or type of request
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

	// Selected Priority - filters by priority level (High, Medium, Low)
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

	// Properties for Categories and Priorities (to bind to ComboBoxes)
	public List<string> Categories { get; set; }
	public List<string> Priorities { get; set; }

	// Initialize the service request tree and collections
	public ServiceRequestViewModel()
	{
		serviceRequestTree = new BinarySearchTree(); // Initialize the BST
		priorityQueue = new MaxHeap(); // Initialize the MaxHeap
		serviceRequestGraph = new ServiceRequestGraph(); // Initialize the Graph
		_filteredServiceRequests = new ObservableCollection<ServiceRequest>();
		Categories = new List<string> { "All", "Pending", "In Progress", "Completed" };
		Priorities = new List<string> { "All", "High", "Medium", "Low" };

		_serviceRequestGraph = new Graph();
		_mstEdges = new List<Edge>();

		LoadServiceRequests();  // Load requests from JSON file
	}

	// Load Service Requests from the ServiceRequestManager
	public void LoadServiceRequests()
	{
		var serviceRequestManager = new ServiceRequestManager();
		var serviceRequests = serviceRequestManager.LoadServiceRequests();  // This dictionary likely uses string keys

		// Create a new dictionary with int keys
		var serviceRequestsIntKey = serviceRequests.ToDictionary(
			kvp => int.Parse(kvp.Key),  // Convert the string key to int
			kvp => kvp.Value
		);

		// Now, serviceRequestsIntKey uses int as the key
		foreach (var request in serviceRequestsIntKey.Values)
		{
			InsertRequestIntoTree(request);  // Insert into Binary Search Tree
			InsertRequestIntoHeap(request);  // Insert into MaxHeap
			AddToGraph(request, serviceRequestsIntKey);  // Add request dependencies to the graph
		}

		FilterRequests();  // Filter the requests after all are loaded
	}

	public void AddToGraph(ServiceRequest request, Dictionary<int, ServiceRequest> serviceRequests)
	{
		// Assuming you're working with a graph that has requests and edges
		// Let's add edges between the current request and other requests based on priority or location

		// Example: Add edges between this request and all other requests
		foreach (var otherRequest in serviceRequests.Values)
		{
			if (request.RequestId != otherRequest.RequestId)
			{
				// Calculate the edge weight (could be based on priority or location)
				double weight = CalculateEdgeWeight(request, otherRequest);

				// Add an edge between these two requests
				_serviceRequestGraph.AddEdge(request, otherRequest, weight);
			}
		}
	}

	private double CalculateEdgeWeight(ServiceRequest req1, ServiceRequest req2)
	{
		// Implement your logic here. For example, based on distance or priority
		// Placeholder: return distance or priority weight
		return Math.Abs(req1.Priority.CompareTo(req2.Priority)); // Example
	}


	// Insert a service request into the BinarySearchTree
	public void InsertRequestIntoTree(ServiceRequest request)
	{
		serviceRequestTree.Insert(request);
	}

	// Insert a service request into the MaxHeap
	public void InsertRequestIntoHeap(ServiceRequest request)
	{
		priorityQueue.Insert(request);
	}

	// Add relationships to the graph (example: request dependencies based on priority)
	public void AddToGraph(ServiceRequest request)
	{
		// Example: For simplicity, connect requests with similar status or priority
		if (request.Priority == "High")
		{
			serviceRequestGraph.AddEdge(request.RequestId, request.RequestId + 1);  // Arbitrary dependency relationship
		}
	}

	// Generate MST based on the graph and display it (you can adapt this part to visualize it)
	public void GenerateAndDisplayMST()
	{
		var mstGenerator = new MinimumSpanningTree();
		var mst = mstGenerator.GenerateMST(serviceRequestGraph, 1);  // Starting from a node (e.g., requestId 1)

		// Optionally display the MST
		foreach (var edge in mst)
		{
			Console.WriteLine($"MST Edge: {edge.Item2} -> {edge.Item3}");
		}
	}

	// Filter requests based on status and priority
	private void FilterRequests()
	{
		var filtered = new List<ServiceRequest>();

		// Create a temporary heap to preserve the original heap's state
		var tempHeap = new MaxHeap();

		while (!priorityQueue.IsEmpty())
		{
			var topRequest = priorityQueue.ExtractMax();

			bool matchesCategory = string.IsNullOrEmpty(SelectedCategory) || SelectedCategory == "All" || topRequest.Status == SelectedCategory;
			bool matchesPriority = string.IsNullOrEmpty(SelectedPriority) || SelectedPriority == "All" || topRequest.Priority == SelectedPriority;

			if (matchesCategory && matchesPriority)
			{
				filtered.Add(topRequest);
			}

			tempHeap.Insert(topRequest);
		}

		while (!tempHeap.IsEmpty())
		{
			priorityQueue.Insert(tempHeap.ExtractMax());
		}

		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(filtered);
	}

	// Method to perform search operation
	private void SearchRequests()
	{
		var filtered = new List<ServiceRequest>();

		// If the search query is a valid integer (RequestId)
		if (int.TryParse(SearchQuery, out int requestId))
		{
			// Search by RequestId using the Binary Search Tree
			var result = serviceRequestTree.Find(requestId);
			if (result != null)
			{
				filtered.Add(result);  // If found, add it to the filtered list
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


	// Event for INotifyPropertyChanged
	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
