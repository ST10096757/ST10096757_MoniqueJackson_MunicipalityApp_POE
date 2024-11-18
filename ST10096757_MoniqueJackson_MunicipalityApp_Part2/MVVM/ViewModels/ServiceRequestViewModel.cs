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

public class ServiceRequestViewModel : INotifyPropertyChanged
{
	private Graph _serviceRequestGraph;
	private List<Edge> _mstEdges;
	public ICommand GenerateMSTCommand { get; set; }
	public ObservableCollection<ServiceRequest> ServiceRequests { get; set; } = new ObservableCollection<ServiceRequest>();

	private BinarySearchTree serviceRequestTree;
	private MaxHeap priorityQueue;
	private ObservableCollection<ServiceRequest> _filteredServiceRequests;
	private string _searchQuery;
	private string _selectedCategory;
	private string _selectedPriority;
	private ServiceRequest _selectedRequest;

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

		// Setting up lists
		Categories = new List<string> { "All", "Pending", "In Progress", "Completed" };
		Priorities = new List<string> { "All", "High", "Medium", "Low" };

		// Defining command
		GenerateMSTCommand = new RelayCommand(GenerateAndDisplayMST);

		// Load the service request data from the JSON file
		LoadServiceRequests();
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Load service requests from the JSON file and add them to the graph
	public void LoadServiceRequests()
	{
		var serviceRequestManager = new ServiceRequestManager();
		var serviceRequests = serviceRequestManager.LoadServiceRequests();

		// Converts the dictionary results (string keys) to a dictionary with int keys.
		var serviceRequestsIntKey = serviceRequests.ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value);

		// Add requests to the graph
		foreach (var request in serviceRequestsIntKey.Values)
		{
			// Insert into Binary Search Tree - for searching
			InsertRequestIntoTree(request);
			// Insert into MaxHeap - for prioritization
			InsertRequestIntoHeap(request);
			// Add request dependencies (edges) to the graph
			AddToGraph(request, serviceRequestsIntKey);
		}

		// Compute and display MST from the Graph
		var mstEdges = _serviceRequestGraph.ComputeMST();
		// Update the filtered service requests based on the MST.
		UpdateFilteredServiceRequests(mstEdges);

		// Filter the requests after all are loaded
		FilterRequests();
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	// Method to add dependencies (edges) between service requests in the graph
	public void AddToGraph(ServiceRequest request, Dictionary<int, ServiceRequest> serviceRequests)
	{
		// Add the service request to the Graph
		if (!_serviceRequestGraph.Requests.ContainsKey(request.RequestId))
		{
			_serviceRequestGraph.AddRequest(request);  // Make sure the service request is added to the graph
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
		// Example edges - this should be handled in LoadServiceRequests() or elsewhere in your program
		Graph.AddEdge(1, 2, 1);
		Graph.AddEdge(1, 3, 1);
		Graph.AddEdge(2, 4, 1);
		Graph.AddEdge(4, 5, 1);

		// Check the graph before displaying
		Console.WriteLine($"Total Requests in Graph: {Graph.Requests.Count}");

		// Compute the MST
		var mstEdges = Graph.ComputeMST();

		// Optionally, generate a string to display graph info
		GraphDisplayText = GenerateGraphDisplayText(mstEdges);
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
