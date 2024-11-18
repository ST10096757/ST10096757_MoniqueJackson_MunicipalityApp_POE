using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.MVVM.Models;
using System.Windows.Input;
using System.Text;
using System.Runtime.CompilerServices;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Data_Structures;

public class ServiceRequestViewModel : INotifyPropertyChanged
{
	private Graph _serviceRequestGraph;
	private List<Edge> _mstEdges;
	public ICommand GenerateMSTCommand { get; set; }

	// Keep the automatic property for ServiceRequests with initialization
	public ObservableCollection<ServiceRequest> ServiceRequests { get; set; } = new ObservableCollection<ServiceRequest>();

	private BinarySearchTree serviceRequestTree;
	private MaxHeap priorityQueue;
	private ObservableCollection<ServiceRequest> _filteredServiceRequests;
	private string _searchQuery;
	private string _selectedCategory;
	private string _selectedPriority;
	private ServiceRequest _selectedRequest;

	// Red-Black Tree for sorting by Submission Date
	private RedBlackTree<ServiceRequest> _serviceRequestTreeByDate;

	// Command to trigger the sorting
	public ICommand SortByDateCommand { get; set; }


	public Graph Graph { get; set; }

	public ObservableCollection<ServiceRequest> FilteredServiceRequests
	{
		get => _filteredServiceRequests;
		set => SetProperty(ref _filteredServiceRequests, value);
	}

	public string SearchQuery
	{
		get => _searchQuery;
		set
		{
			if (SetProperty(ref _searchQuery, value))
				SearchRequests();  // Trigger search when the query changes
		}
	}

	public string SelectedCategory
	{
		get => _selectedCategory;
		set
		{
			if (SetProperty(ref _selectedCategory, value))
				FilterRequests();  // Trigger filtering when category changes
		}
	}

	public string SelectedPriority
	{
		get => _selectedPriority;
		set
		{
			if (SetProperty(ref _selectedPriority, value))
				FilterRequests();  // Trigger filtering when priority changes
		}
	}

	public ServiceRequest SelectedRequest
	{
		get => _selectedRequest;
		set => SetProperty(ref _selectedRequest, value);
	}

	public bool IsRequestSelected => SelectedRequest != null;

	public List<string> Categories { get; set; }
	public List<string> Priorities { get; set; }

	private string _filterText;

	public string FilterText
	{
		get => _filterText;
		set
		{
			if (_filterText != value)
			{
				_filterText = value;
				OnPropertyChanged(nameof(FilterText));
				/*FilterServiceRequests();*/  // Call the method to filter service requests
			}
		}
	}

	private string _graphDisplayText;
	private string _progressLogsString;
	private ICommand _displayGraphCommand;

	public string GraphDisplayText
	{
		get => _graphDisplayText;
		set
		{
			_graphDisplayText = value;
			OnPropertyChanged(nameof(GraphDisplayText));
		}
	}

	public string ProgressLogsString
	{
		get => _progressLogsString;
		set
		{
			_progressLogsString = value;
			OnPropertyChanged(nameof(ProgressLogsString));
		}
	}

	public ICommand DisplayGraphCommand
	{
		get => _displayGraphCommand;
		set
		{
			_displayGraphCommand = value;
			OnPropertyChanged(nameof(DisplayGraphCommand));
		}
	}

	public List<Edge> MstEdges
	{
		get => _mstEdges;
		set
		{
			if (_mstEdges != value)
			{
				_mstEdges = value;
				OnPropertyChanged(nameof(MstEdges));
			}
		}
	}


	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	public ServiceRequestViewModel()
	{
		// Initializing Data Structures
		serviceRequestTree = new BinarySearchTree();
		priorityQueue = new MaxHeap();
		_serviceRequestGraph = new Graph();
		_filteredServiceRequests = new ObservableCollection<ServiceRequest>();

		// Initialize the Graph object which will hold ServiceRequests and Edges
		Graph = new Graph();
		_mstEdges = new List<Edge>();
		_displayGraphCommand = new RelayCommand(DisplayGraph);

		ServiceRequests = new ObservableCollection<ServiceRequest>();


		// Setting up lists
		Categories = new List<string> { "All", "Pending", "In Progress", "Completed" };
		Priorities = new List<string> { "All", "High", "Medium", "Low" };

		// Defining command
		GenerateMSTCommand = new RelayCommand(GenerateAndDisplayMST);

		// Initialize Red-Black Tree for sorting by date
		_serviceRequestTreeByDate = new RedBlackTree<ServiceRequest>();

		// Define the SortByDateCommand
		SortByDateCommand = new RelayCommand(SortServiceRequestsByDate);

		// Load the service request data from the JSON file
		LoadServiceRequests();
	}




	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Load service requests from the JSON file and add them to the graph
	// Load service requests into both the BST (for ID) and RBT (for submission date)
	public void LoadServiceRequests()
	{
		var serviceRequestManager = new ServiceRequestManager();
		var serviceRequests = serviceRequestManager.LoadServiceRequests();

		// Convert the dictionary from string keys to int keys (assuming the key is RequestId as string in serviceRequests)
		var serviceRequestsIntKey = serviceRequests.ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value);

		// Convert and insert into the Red-Black Tree for sorting by submission date
		foreach (var request in serviceRequestsIntKey.Values)
		{
			// Insert into Binary Search Tree for ID (assumes InsertRequestIntoTree is handling the BST)
			InsertRequestIntoTree(request);

			// Insert into Red-Black Tree for sorting by submission date
			_serviceRequestTreeByDate.Insert(request);

			// Add the request to the graph
			AddToGraph(request, serviceRequestsIntKey);  // Pass the correctly typed dictionary
		}

		// Initialize filtered service requests
		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(_serviceRequestTreeByDate.GetSortedItems());

		// After loading all service requests, filter them by current filter settings
		FilterRequests();
	}

	// Sort the requests by submission date using the Red-Black Tree
	public void SortServiceRequestsByDate()
	{
		var sortedRequestsByDate = _serviceRequestTreeByDate.GetSortedItems();
		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(sortedRequestsByDate);
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Method to add dependencies (edges) between service requests in the graph
	public void AddToGraph(ServiceRequest request, Dictionary<int, ServiceRequest> serviceRequests)
	{
		

		// Add the service request to the Graph
		if (!_serviceRequestGraph.Requests.ContainsKey(request.RequestId))
		{
			_serviceRequestGraph.AddRequest(request);  // Add to graph
			Console.WriteLine($"Request {request.RequestId} added to graph.");
		}
		else
		{
			Console.WriteLine($"Request {request.RequestId} already in graph.");
		}

		// Loops through all other requests in the serviceRequests dictionary.
		foreach (var otherRequest in serviceRequests.Values)
		{
			if (request.RequestId != otherRequest.RequestId)
			{
				// Calculate the weight of the edge based on the requests' properties (e.g., priority)
				double weight = CalculateEdgeWeight(request, otherRequest);
				// Add the edge to the graph
				_serviceRequestGraph.AddEdge(request.RequestId, otherRequest.RequestId, weight);
				Console.WriteLine($"Edge added: {request.RequestId} -> {otherRequest.RequestId} with weight {weight}");
			}
		}

		// Add the service request to the Graph
		if (!Graph.Requests.ContainsKey(request.RequestId))
		{
			Graph.AddRequest(request);  // Make sure the service request is added to the graph
		}

		// Loops through all other requests in the serviceRequests dictionary.
		foreach (var otherRequest in serviceRequests.Values)
		{
			if (request.RequestId != otherRequest.RequestId)
			{
				// Calculate the weight of the edge based on the requests' properties (e.g., priority)
				double weight = CalculateEdgeWeight(request, otherRequest);
				// Add the edge to the graph
				Graph.AddEdge(request.RequestId, otherRequest.RequestId, weight);
			}
		}
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Helper method to calculate the weight of an edge between two requests
	private double CalculateEdgeWeight(ServiceRequest req1, ServiceRequest req2)
	{
		return Math.Abs(req1.Priority.CompareTo(req2.Priority)); // Simplified weight calculation based on priority
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Update the filtered list of service requests based on the edges from the MST
	public void UpdateFilteredServiceRequests(List<Edge> mstEdges)
	{
		Console.WriteLine($"MST Edges Count: {mstEdges.Count}");

		// Filter edges where Start exists in the dictionary and look up the corresponding ServiceRequest from the graph
		var filteredRequests = new ObservableCollection<ServiceRequest>(
			mstEdges.Select(edge =>
			{
				// Check if the ServiceRequest exists in the graph based on the Start value (RequestId)
				ServiceRequest req = null;
				if (_serviceRequestGraph.Requests.TryGetValue(edge.Start, out req))
				{
					Console.WriteLine($"Found ServiceRequest with ID {edge.Start}: {req.ResidentName}");
				}
				else
				{
					Console.WriteLine($"No ServiceRequest found for ID {edge.Start}");
				}
				return req;
			})
			.Where(req => req != null)
		);

		// Update the filtered requests list
		FilteredServiceRequests = filteredRequests;
		Console.WriteLine($"Filtered Service Requests Count: {FilteredServiceRequests.Count}");
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Display the graph and compute the MST
	public void DisplayGraph()
	{
		// First, ensure the graph is populated by loading service requests
		LoadServiceRequests();

		// Now the graph contains all service requests and their edges.
		Console.WriteLine($"Total Requests in Graph: {Graph.Requests.Count}");

		// Check if the adjacency list has the expected data
		Console.WriteLine("Adjacency List Contents:");
		foreach (var node in Graph.AdjacencyList)
		{
			Console.WriteLine($"Node {node.Key}:");
			foreach (var edge in node.Value)
			{
				Console.WriteLine($"   -> {edge.End} with weight {edge.Weight}");
			}
		}

		// If the graph has no requests or edges, exit early
		if (Graph.Requests.Count == 0 || Graph.AdjacencyList.Count == 0)
		{
			Console.WriteLine("Graph is empty or not populated correctly.");
			return;
		}

		// Compute the Minimum Spanning Tree (MST) from the graph
		var mstEdges = Graph.ComputeMST();

		// Optionally, generate a string to display the MST graph info
		GraphDisplayText = GenerateGraphDisplayText(mstEdges);

		// Optionally, log the MST edges to the console for debugging purposes
		Console.WriteLine("Displaying MST Edges:");
		foreach (var edge in mstEdges)
		{
			Console.WriteLine($"Edge: {edge.Start} -> {edge.End} with weight {edge.Weight}");
		}
	}


	// Helper method to create a displayable text of the graph
	private string GenerateGraphDisplayText(List<Edge> mstEdges)
	{
		var displayText = new StringBuilder();

		foreach (var edge in mstEdges)
		{
			displayText.AppendLine($"Edge: {edge.Start} -> {edge.End} with weight {edge.Weight}");
		}

		return displayText.ToString();
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Generate and display the Minimum Spanning Tree (MST)
	public void GenerateAndDisplayMST()
	{
		var mstGenerator = new MinimumSpanningTree();
		var mstEdges = mstGenerator.GenerateMST(_serviceRequestGraph, 1);

		foreach (var edge in mstEdges)
		{
			Console.WriteLine($"MST Edge: {edge.Start} -> {edge.End} with weight: {edge.Weight}");
		}

		UpdateFilteredServiceRequests(mstEdges);
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Insert a request into the Binary Search Tree
	public void InsertRequestIntoTree(ServiceRequest request)
	{
		serviceRequestTree.Insert(request);
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Insert a request into the MaxHeap for prioritization
	public void InsertRequestIntoHeap(ServiceRequest request)
	{
		priorityQueue.Insert(request);
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Filter requests based on selected category and priority
	private void FilterRequests()
	{
		var filtered = new List<ServiceRequest>();

		foreach (var request in _serviceRequestGraph.Requests.Values)
		{
			bool matchesCategory = string.IsNullOrEmpty(SelectedCategory) || SelectedCategory == "All" || request.Status == SelectedCategory;
			bool matchesPriority = string.IsNullOrEmpty(SelectedPriority) || SelectedPriority == "All" || request.Priority == SelectedPriority;

			if (matchesCategory && matchesPriority)
			{
				filtered.Add(request);
			}
		}

		// Update the filtered list of service requests
		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(filtered);
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Search for service requests based on the search query
	private void SearchRequests()
	{
		var filtered = new List<ServiceRequest>();

		if (int.TryParse(SearchQuery, out int requestId))
		{
			var result = serviceRequestTree.Find(requestId);
			if (result != null)
			{
				filtered.Add(result);
			}
		}
		else
		{
			serviceRequestTree.InOrderTraversal(req => filtered.Add(req));
		}

		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(filtered);
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Helper method to update properties and notify the UI when a property has changed
	private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
	{
		if (EqualityComparer<T>.Default.Equals(field, value))
			return false;

		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
