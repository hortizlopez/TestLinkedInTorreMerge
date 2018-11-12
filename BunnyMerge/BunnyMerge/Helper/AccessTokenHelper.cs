using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunnyMerge.Helpers
{
	public class AccessTokenHelper
	{
		public string access_token { get; set; }

		public string expires_in { get; set; }

		public string error { get; set; }

		public string error_description { get; set; }
	}
}