using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunnyMerge.Helper
{
	public class LinkedInList<T>
	{
		public int _total { get; set; }

		public List<T> values { get; set; }
	}
}