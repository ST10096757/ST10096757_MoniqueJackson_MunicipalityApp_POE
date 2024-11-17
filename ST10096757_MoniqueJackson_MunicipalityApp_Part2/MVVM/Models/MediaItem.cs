using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models
{
	public class MediaItem
	{
		public string FilePath { get; set; }

		public bool IsImage => FilePath.EndsWith(".jpg") || FilePath.EndsWith(".jpeg") || FilePath.EndsWith(".png");
		public bool IsVideo => FilePath.EndsWith(".mp4") || FilePath.EndsWith(".avi");
		public bool IsDocument => FilePath.EndsWith(".pdf") || FilePath.EndsWith(".docx");

		public string ImageVisible => IsImage ? "Visible" : "Collapsed";
		public string VideoVisible => IsVideo ? "Visible" : "Collapsed";
		public string DocumentVisible => IsDocument ? "Visible" : "Collapsed";

		public ICommand OpenDocumentCommand => new RelayCommand(OpenDocument);

		private void OpenDocument()
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = FilePath,
				UseShellExecute = true
			});
		}
	}
}
