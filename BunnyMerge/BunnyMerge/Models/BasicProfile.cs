using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunnyMerge.Models
{
	public class BasicProfile
	{
		public string firstName { get; set; }

		public string lastName { get; set; }

		public string headline { get; set; }

		public string id { get; set; }

		public string industry { get; set; }

		public int numConnections { get; set; }

		public string pictureUrl { get; set; }

		public string publicProfileUrl { get; set; }

		public string summary { get; set; }

		public Location location { get; set; }

		public Helper.LinkedInList<Position> positions { get; set; }
	}
}