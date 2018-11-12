using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunnyMerge.Models
{
	public class Location
	{
		public string name { get; set; }

		public Country country { get; set; }
	}
}