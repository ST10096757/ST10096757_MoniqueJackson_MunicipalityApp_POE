using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.IO;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	public class FileTypeToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string filePath)
			{
				var extension = Path.GetExtension(filePath).ToLower();

				if (parameter.ToString() == "Image" && (extension == ".jpg" || extension == ".png" || extension == ".gif"))
				{
					return Visibility.Visible;
				}
				else if (parameter.ToString() == "Video" && (extension == ".mp4" || extension == ".avi"))
				{
					return Visibility.Visible;
				}
				else if (parameter.ToString() == "Document" && (extension == ".pdf" || extension == ".docx"))
				{
					return Visibility.Visible;
				}
			}
			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
