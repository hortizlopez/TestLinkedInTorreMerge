using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunnyMerge.Models
{
	public class TorreJob
	{
		public string id { get; set; }
		public string category { get; set; }
		public string name { get; set; }
		public string role { get; set; }
		public string contributions { get; set; }
		public string fromMonth { get; set; }
		public string fromYear { get; set; }
		public string toMonth { get; set; }
		public string toYear { get; set; }
		public string location { get; set; }
		public string additionalInfo { get; set; }
	}
}