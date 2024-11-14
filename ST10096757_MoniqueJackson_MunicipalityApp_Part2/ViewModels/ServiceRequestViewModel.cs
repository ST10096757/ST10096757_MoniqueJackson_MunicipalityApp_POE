using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels
{
	public class ServiceRequestViewModel : INotifyPropertyChanged
	{
		private BinarySearchTree _serviceRequestBST;
		private MaxHeap _serviceRequestHeap;
		private List<ServiceRequest> _serviceRequests;

		public List<ServiceRequest> ServiceRequests
		{
			get { return _serviceRequests; }
			set
			{
				_serviceRequests = value;
				OnPropertyChanged(nameof(ServiceRequests));
			}
		}

		public ServiceRequestViewModel()
		{
			_serviceRequests = new List<ServiceRequest>();
			_serviceRequestBST = new BinarySearchTree();
			_serviceRequestHeap = new MaxHeap();
			// Example data initialization
			InitializeServiceRequests();
		}

		private void InitializeServiceRequests()
		{
			// Add example requests to BST and Heap for initial data
			var requests = new List<ServiceRequest>
			{
				new ServiceRequest(1, "Streetlight repair", "Pending", "High", DateTime.Now),
				new ServiceRequest(2, "Pothole filling", "In Progress", "Medium", DateTime.Now.AddDays(-1)),
				new ServiceRequest(3, "Water leak", "Completed", "High", DateTime.Now.AddDays(-3))
			};

			foreach (var request in requests)
			{
				_serviceRequestBST.Insert(request);
				_serviceRequestHeap.Insert(request);
			}

			UpdateServiceRequestsList();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Update the ServiceRequests list from the BST
		private void UpdateServiceRequestsList()
		{
			var sortedRequests = new List<ServiceRequest>();
			_serviceRequestBST.InOrderTraversal(request => sortedRequests.Add(request));
			ServiceRequests = sortedRequests;
		}

		// Update the status of a specific service request
		public void UpdateRequestStatus(int requestId, string newStatus)
		{
			var request = _serviceRequests.FirstOrDefault(r => r.RequestId == requestId);
			if (request != null)
			{
				request.Status = newStatus;
				OnPropertyChanged(nameof(ServiceRequests));
			}
		}

		// Get the highest priority service request from the heap
		public ServiceRequest GetHighestPriorityRequest()
		{
			return _serviceRequestHeap.ExtractMax();
		}
	}
}
