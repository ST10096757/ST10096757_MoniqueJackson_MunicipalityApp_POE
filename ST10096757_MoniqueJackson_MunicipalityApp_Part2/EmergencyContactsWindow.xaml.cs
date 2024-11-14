using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	/// <summary>
	/// Interaction logic for EmergencyContactsWindow.xaml
	/// </summary>
	public partial class EmergencyContactsWindow : Window
	{
		public List<Contact> Contacts { get; set; }

		public EmergencyContactsWindow()
		{
			InitializeComponent();
			LoadContacts();
			DataContext = this; // Set DataContext for data binding
		}

		private void LoadContacts()
		{
			Contacts = new List<Contact>
			{
				new Contact { Country = "Emergencies from Mobile", Telephone = "112" },
				new Contact { Country = "Emergencies from Landline", Telephone = "107" },
				new Contact { Country = "South African Police Service", Telephone = "10111" },
				new Contact { Country = "Medical & Fire", Telephone = "021 535 1100" },
				new Contact { Country = "Table Mountain NP Emergencies", Telephone = "021 480 7700" },
				new Contact { Country = "Sea and Mountain Rescue", Telephone = "021 948 9900" },
				new Contact { Country = "National Sea Rescue Institute", Telephone = "087 094 9774" },
				new Contact { Country = "Baboon Monitors", Telephone = "071 588 6540" },
				new Contact { Country = "Shark Spotters", Telephone = "078 174 4244" },
				new Contact { Country = "Ambulance", Telephone = "10177" },
			};
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close(); // Closes the current window
		}
	}

	public class Contact
	{
		public string Country { get; set; }
		public string Telephone { get; set; }
	}
}

