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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Controls
{
	/// <summary>
	/// Interaction logic for myTextBox.xaml
	/// </summary>
	public partial class myTextBox : UserControl
	{
		public myTextBox()
		{
			InitializeComponent();
		}

		public string Hint
		{
			get { return (string)GetValue(HintProperty); }
			set { SetValue(HintProperty, value); }
		}

		public static readonly DependencyProperty HintProperty = DependencyProperty.Register("Hint", typeof(string), typeof(myTextBox));
	}
}
