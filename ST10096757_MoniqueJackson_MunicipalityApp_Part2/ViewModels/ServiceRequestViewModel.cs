using ST10096757_MoniqueJackson_MunicipalityApp_Part2;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Managers;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

public class ServiceRequestViewModel : INotifyPropertyChanged
{
	private Dictionary<string, ServiceRequest> _serviceRequests;
	private ObservableCollection<ServiceRequest> _filteredServiceRequests;
	private string _searchQuery;
	private string _selectedCategory;
	private string _selectedPriority;
	private ServiceRequest _selectedRequest;

	// ICommand for the search button
	public ICommand SearchCommand { get; private set; }

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
		}
	}

	public string SelectedCategory
	{
		get { return _selectedCategory; }
		set
		{
			_selectedCategory = value;
			OnPropertyChanged(nameof(SelectedCategory));
			FilterRequests(); // Re-filter when status changes
		}
	}

	public string SelectedPriority
	{
		get { return _selectedPriority; }
		set
		{
			_selectedPriority = value;
			OnPropertyChanged(nameof(SelectedPriority));
			FilterRequests(); // Re-filter when priority changes
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
	public List<string> Priorities { get; set; }

	// Constructor
	public ServiceRequestViewModel()
	{
		_serviceRequests = new Dictionary<string, ServiceRequest>();
		_filteredServiceRequests = new ObservableCollection<ServiceRequest>();
		Categories = new List<string> { "All", "Pending", "In Progress", "Completed" };
		Priorities = new List<string> { "All", "High", "Medium", "Low" };

		// Initialize service requests
		InitializeServiceRequests();

		// Initialize the command
		SearchCommand = new RelayCommand(ExecuteSearch);
	}

	// Initialize the service requests (example)
	private void InitializeServiceRequests()
	{
		var serviceRequestManager = new ServiceRequestManager();
		var loadedRequests = serviceRequestManager.LoadServiceRequests(); // Returns Dictionary<string, ServiceRequest>

		foreach (var request in loadedRequests)
		{
			_serviceRequests[request.Key] = request.Value;
		}

		// Initially show all requests
		FilterRequests();
	}

	// Filter requests based on search query, selected status, and selected priority
	private void FilterRequests()
	{
		var filtered = _serviceRequests.Values.AsEnumerable();

		if (!string.IsNullOrEmpty(SearchQuery))
		{
			filtered = filtered.Where(r => r.Description.IndexOf(SearchQuery, System.StringComparison.OrdinalIgnoreCase) >= 0);
		}

		if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "All")
		{
			filtered = filtered.Where(r => r.Status == SelectedCategory);
		}

		if (!string.IsNullOrEmpty(SelectedPriority) && SelectedPriority != "All")
		{
			filtered = filtered.Where(r => r.Priority == SelectedPriority);
		}

		FilteredServiceRequests = new ObservableCollection<ServiceRequest>(filtered);
	}

	// Command execution for search
	private void ExecuteSearch()
	{
		FilterRequests();
	}

	// Event for INotifyPropertyChanged
	public event PropertyChangedEventHandler PropertyChanged;
	protected virtual void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
