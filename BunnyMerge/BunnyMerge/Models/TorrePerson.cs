using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunnyMerge.Models
{
	public class TorrePerson
	{
		public string id { get; set; }

		public string publicId { get; set; }

		public string name { get; set; }

		public string email { get; set; }

		public bool hasEmail { get; set; }

		public bool showEmail { get; set; }

		public string professionalHeadline { get; set; }

		public string location { get; set; }

		public string picture { get; set; }

		public string theme { get; set; }

		public string phone { get; set; }

		public bool showPhone { get; set; }

		public double weight { get; set; }

		public double completion { get; set; }

		public bool verified { get; set; }

		public bool claimant { get; set; }
	}
}