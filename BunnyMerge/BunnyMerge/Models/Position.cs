using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunnyMerge.Models
{
	public class Position
	{
		public string id { get; set; }

		public string title { get; set; }

		public string summary { get; set; }

		public Helper.LinkedInDate startDate { get; set; }

		public Helper.LinkedInDate endDate { get; set; }

		public string isCurrent { get; set; }

		public Company company { get; set; }

		public Location location { get; set; }
	}
}