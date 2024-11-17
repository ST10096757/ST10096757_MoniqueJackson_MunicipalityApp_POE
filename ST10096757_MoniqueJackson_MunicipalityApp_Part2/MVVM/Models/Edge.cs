using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.MVVM.Models
{
	public class Edge
	{
		public int Start { get; set; }
		public int End { get; set; }
		public double Weight { get; set; }

		public Edge(int start, int end, double weight)
		{
			Start = start;
			End = end;
			Weight = weight;
		}
	}
}
