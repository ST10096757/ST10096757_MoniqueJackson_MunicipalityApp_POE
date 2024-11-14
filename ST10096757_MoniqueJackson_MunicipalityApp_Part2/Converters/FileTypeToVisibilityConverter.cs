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
	// This class implements IValueConverter to convert file paths into corresponding UI visibility states
	// depending on the file type (Image, Video, or Document).
	public class FileTypeToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// Convert method is called to transform a value (file path) into a target value (Visibility)
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Check if the value is a string (file path)
			if (value is string filePath)
			{
				// Get the file extension (e.g., .jpg, .mp4) from the file path and convert it to lowercase
				var extension = Path.GetExtension(filePath).ToLower();

				// Check if the "parameter" is "Image" and if the file is an image (.jpg, .png, .gif)
				if (parameter.ToString() == "Image" && (extension == ".jpg" || extension == ".png" || extension == ".gif"))
				{
					// Return Visibility.Visible to make the associated UI element visible
					return Visibility.Visible;
				}
				// Check if the "parameter" is "Video" and if the file is a video (.mp4, .avi)
				else if (parameter.ToString() == "Video" && (extension == ".mp4" || extension == ".avi"))
				{
					// Return Visibility.Visible to make the associated UI element visible
					return Visibility.Visible;
				}
				// Check if the "parameter" is "Document" and if the file is a document (.pdf, .docx)
				else if (parameter.ToString() == "Document" && (extension == ".pdf" || extension == ".docx"))
				{
					// Return Visibility.Visible to make the associated UI element visible
					return Visibility.Visible;
				}
			}
			// If none of the conditions are met, return Visibility.Collapsed to hide the UI element
			return Visibility.Collapsed;
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// ConvertBack method is not implemented because it's not needed in this case
		/// The ConvertBack method is used for two-way data binding, but we are only using one-way binding here
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Since we don't need to convert the visibility back to a file path, this method throws a NotImplementedException
			throw new NotImplementedException();
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
