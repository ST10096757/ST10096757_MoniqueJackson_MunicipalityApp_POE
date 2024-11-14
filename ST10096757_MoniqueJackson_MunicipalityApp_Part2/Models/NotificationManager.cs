using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	public class NotificationManager
	{
		private Stack<string> notifications;

		public NotificationManager()
		{
			notifications = new Stack<string>();
		}

		public void AddNotification(string message)
		{
			notifications.Push(message); // Add a new notification
		}

		public string GetLatestNotification()
		{
			return notifications.Count > 0 ? notifications.Peek() : null; // Get the latest notification
		}

		public string RemoveLatestNotification()
		{
			return notifications.Count > 0 ? notifications.Pop() : null; // Remove the latest notification
		}
	}
}
