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

		private ServiceRequest _selectedRequest;
		public ServiceRequest SelectedRequest
		{
			get { return _selectedRequest; }
			set
			{
				_selectedRequest = value;
				OnPropertyChanged(nameof(SelectedRequest));
			}
		}

		public ServiceRequestViewModel()
		{
			// Example data initialization
			ServiceRequests = new List<ServiceRequest>
			{
				new ServiceRequest(1, "Streetlight repair", "Pending", "High", DateTime.Now),
				new ServiceRequest(2, "Pothole filling", "In Progress", "Medium", DateTime.Now.AddDays(-1)),
				new ServiceRequest(3, "Water leak", "Completed", "High", DateTime.Now.AddDays(-3))
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void UpdateRequestStatus(int requestId, string newStatus)
		{
			var request = ServiceRequests.FirstOrDefault(r => r.RequestId == requestId);
			if (request != null)
			{
				request.Status = newStatus;
				OnPropertyChanged(nameof(ServiceRequests));
			}
		}
	}
}
