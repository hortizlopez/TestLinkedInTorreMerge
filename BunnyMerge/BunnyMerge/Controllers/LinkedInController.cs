using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BunnyMerge.Controllers
{
	public class LinkedInController : Controller
	{
		// GET: Linkedin
		public ActionResult Index()
		{
			return View();
		}

		// GET: Linkedin/Auth
		public ActionResult Auth(string code, string state)
		{
			try
			{
				System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
				var client = new RestClient("https://www.linkedin.com/oauth/v2/accessToken");
				var request = new RestRequest(Method.POST);
				request.AddParameter("grant_type", "authorization_code");
				request.AddParameter("code", code);
				request.AddParameter("redirect_uri", "http://localhost:61234/Linkedin/Auth");
				request.AddParameter("client_id", "783kpt24kkvmuf");
				request.AddParameter("client_secret", "nZDGbWMgsT5IYz8W");
				IRestResponse response = client.Execute(request);

				JsonDeserializer deserializer = new JsonDeserializer();
				var content = deserializer.Deserialize<BunnyMerge.Helpers.AccessTokenHelper>(response);

				string querystring = string.Format(@"~:({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21})?oauth2_access_token={22}&format=json",
					"id",
					"first-name",
					"last-name",
					"maiden-name",
					"formatted-name",
					"phonetic-first-name",
					"phonetic-last-name",
					"formatted-phonetic-name",
					"headline",
					"location",
					"industry",
					"current-share",
					"num-connections",
					"num-connections-capped",
					"summary",
					"specialties",
					"positions",
					"picture-url",
					"picture-urls::(original)",
					"site-standard-profile-request",
					"api-standard-profile-request",
					"public-profile-url",
					content.access_token);

				string url = string.Format(@"https://api.linkedin.com/v1/people/{0}", querystring);

				client = new RestClient(url);
				request = new RestRequest(Method.GET);
				response = client.Execute(request);

				var profile = deserializer.Deserialize<Models.BasicProfile>(response);

				//client = new RestClient("https://api.linkedin.com/v1/people/")

				//content = response.Content;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return View();
		}
	}
}