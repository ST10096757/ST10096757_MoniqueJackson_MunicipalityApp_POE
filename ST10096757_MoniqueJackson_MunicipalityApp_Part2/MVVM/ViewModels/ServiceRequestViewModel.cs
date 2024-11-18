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

	private BinarySearchTree serviceRequestTree;
	private MaxHeap priorityQueue;
	private ObservableCollection<ServiceRequest> _filteredServiceRequests;
	private string _searchQuery;
	private string _selectedCategory;
	private string _selectedPriority;
	private ServiceRequest _selectedRequest;

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

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	/// <summary>
	/// Default Constructor
	/// </summary>
	public ServiceRequestViewModel()
	{
		//Initializing Data Structures
		serviceRequestTree = new BinarySearchTree();
		priorityQueue = new MaxHeap();
		_serviceRequestGraph = new Graph();
		_filteredServiceRequests = new ObservableCollection<ServiceRequest>();

		//Setting up lists
		Categories = new List<string> { "All", "Pending", "In Progress", "Completed" };
		Priorities = new List<string> { "All", "High", "Medium", "Low" };

		//Defining command
		GenerateMSTCommand = new RelayCommand(GenerateAndDisplayMST);

		//Calls method to load the request data from the JSON file.
		LoadServiceRequests();  
	}

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//

	/// <summary>
	/// Method load service request data and processes them
	/// </summary>
	public void LoadServiceRequests()
	{
		//Used to load service requests
		var serviceRequestManager = new ServiceRequestManager();
		var serviceRequests = serviceRequestManager.LoadServiceRequests();

		//Converts the dictionary results with string keys, to a dictionary with int keys.
		var serviceRequestsIntKey = serviceRequests.ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value);

		foreach (var request in serviceRequestsIntKey.Values)
		{
			// Insert into Binary Search Tree - for searching
			InsertRequestIntoTree(request);
			// Insert into MaxHeap - for prioritization
			InsertRequestIntoHeap(request);
			// Add request dependencies to the graph - to track relationship between requests
			AddToGraph(request, serviceRequestsIntKey);  
		}

		// Compute and display MST from the Graph
		var mstEdges = _serviceRequestGraph.ComputeMST();
		//Updates the filtered service requests based on the MST.
		UpdateFilteredServiceRequests(mstEdges);

		// Filter the requests after all are loaded
		FilterRequests();  
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//

	/// <summary>
	/// This method updates the filtered list of service requests based on the edges from the MST.
	/// </summary>
	/// <param name="mstEdges"></param>
	public void UpdateFilteredServiceRequests(List<Edge> mstEdges)
	{
		// Filter edges where Start exists in the dictionary
		// For each edge, it looks up the corresponding ServiceRequest from the graph using TryGetValue.
		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(
			mstEdges.Select(edge =>
			{
				// Use ContainsKey to check if the key exists in the dictionary
				_serviceRequestGraph.Requests.TryGetValue(edge.Start, out ServiceRequest req);
				return req;
			})
			.Where(req => req != null)  // Ensure that valid requests are included
		);
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//

	/// <summary>
	/// This method adds dependencies (edges) between service requests in the graph.
	/// </summary>
	/// <param name="request"></param>
	/// <param name="serviceRequests"></param>
	public void AddToGraph(ServiceRequest request, Dictionary<int, ServiceRequest> serviceRequests)
	{
		// Loops through all other requests in the serviceRequests dictionary.
		foreach (var otherRequest in serviceRequests.Values)
		{
			// If the requests are not the same(request.RequestId != otherRequest.RequestId),
			// it calculates the weight of the connection(edge) between them.
			if (request.RequestId != otherRequest.RequestId)
			{
				double weight = CalculateEdgeWeight(request, otherRequest);
				//Adds the edge to the graph
				_serviceRequestGraph.AddEdge(request.RequestId, otherRequest.RequestId, weight);
			}
		}
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//

	/// <summary>
	/// This method generates and displays the Minimum Spanning Tree (MST).
	/// </summary>
	public void GenerateAndDisplayMST()
	{
		// Creates a new instance of MinimumSpanningTree to generate the MST from the graph.
		var mstGenerator = new MinimumSpanningTree();
		// The MST is generated using the graph and starting from a request with ID = 1.
		var mstEdges = mstGenerator.GenerateMST(_serviceRequestGraph, 1);

		// Optionally display the MST
		foreach (var edge in mstEdges)
		{
			// It prints the MST edges to the console for debugging/visualization
			Console.WriteLine($"MST Edge: {edge.Start} -> {edge.End} with weight: {edge.Weight}");
		}

		// It then calls UpdateFilteredServiceRequests() to update the UI with the new MST edges.
		UpdateFilteredServiceRequests(mstEdges);  
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//

	/// <summary>
	/// 
	/// </summary>
	/// <param name="req1"></param>
	/// <param name="req2"></param>
	/// <returns></returns>
	private double CalculateEdgeWeight(ServiceRequest req1, ServiceRequest req2)
	{
		return Math.Abs(req1.Priority.CompareTo(req2.Priority));  // Simplified weight calculation based on priority
	}

	public void InsertRequestIntoTree(ServiceRequest request)
	{
		serviceRequestTree.Insert(request);
	}

	public void InsertRequestIntoHeap(ServiceRequest request)
	{
		priorityQueue.Insert(request);
	}

	/// <summary>
	/// This method filters the service requests based on selected category and priority.
	/// Uses the Priority Queue and the Heap
	/// </summary>
	private void FilterRequests()
	{
		var filtered = new List<ServiceRequest>();

		// Creates a temporary heap to preserve the state of the priorityQueue.
		var tempHeap = new MaxHeap();

		// Loops through all requests in the priority queue,
		// and checks if they match the selected category and priority.
		while (!priorityQueue.IsEmpty())
		{
			var topRequest = priorityQueue.ExtractMax();

			bool matchesCategory = string.IsNullOrEmpty(SelectedCategory) || SelectedCategory == "All" || topRequest.Status == SelectedCategory;
			bool matchesPriority = string.IsNullOrEmpty(SelectedPriority) || SelectedPriority == "All" || topRequest.Priority == SelectedPriority;

			// If a request matches both filters, it is added to the filtered list.
			if (matchesCategory && matchesPriority)
			{
				filtered.Add(topRequest);
			}

			tempHeap.Insert(topRequest);
		}

		// Restore the original heap state
		while (!tempHeap.IsEmpty())
		{
			priorityQueue.Insert(tempHeap.ExtractMax());
		}

		// Updates the FilteredServiceRequests collection,
		// which will update the UI with the filtered results.
		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(filtered);
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//

	/// <summary>
	/// This method searches for service requests based on a search query
	/// Use BST 
	/// </summary>
	private void SearchRequests()
	{
		var filtered = new List<ServiceRequest>();

		// Checks if the SearchQuery can be parsed as an integer (requestId).
		if (int.TryParse(SearchQuery, out int requestId))
		{
			// If so, it searches for that requestId in the Binary Search Tree 
			// and adds the result to filtered
			var result = serviceRequestTree.Find(requestId);
			if (result != null)
			{
				filtered.Add(result);  // Add to filtered if found
			}
		}
		else
		{
			// If search query isn't an integer, show all requests (in sorted order)
			serviceRequestTree.InOrderTraversal(req => filtered.Add(req));
		}

		// Updates FilteredServiceRequests with the search results.
		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(filtered);
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//

	/// <summary>
	/// A helper method to update properties and notify the UI when a property has changed.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="field"></param>
	/// <param name="value"></param>
	/// <param name="propertyName"></param>
	/// <returns></returns>
	private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
	{
		// Checks if the value has changed. If not, it does nothing.
		if (EqualityComparer<T>.Default.Equals(field, value))
			return false;

		//If the value has changed, it updates the field and triggers the PropertyChanged event
		//to notify the UI
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//

	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}


