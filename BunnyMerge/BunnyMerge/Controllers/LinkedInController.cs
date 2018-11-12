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
				request.AddParameter("redirect_uri", System.Configuration.ConfigurationManager.AppSettings["LIAuthRedirectUri"].ToString());
				request.AddParameter("client_id", System.Configuration.ConfigurationManager.AppSettings["LIAuthApiClientId"]);
				request.AddParameter("client_secret", System.Configuration.ConfigurationManager.AppSettings["LIAuthClientSecret"]);
				IRestResponse response = client.Execute(request);

				JsonDeserializer deserializer = new JsonDeserializer();
				var content = deserializer.Deserialize<BunnyMerge.Helpers.AccessTokenHelper>(response);

				var tuple = connectLinkedin(content.access_token);
				var profile = tuple.Item1;
				response = tuple.Item2;

				var linkedInChecked = false;
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					Session["LIUserName"] = profile.firstName;
					Session["LIUserId"] = profile.id;
					Session["access_token"] = content.access_token;
					linkedInChecked = true;
				}

				var torreBioChecked = false;
				var torreModel = new Models.TorreProfile();

				if (Session["UserId"] != null && Session["UserId"] != string.Empty)
				{
					var torreController = new TorreBioController();
					var userId = Session["UserId"].ToString();
					var userName = Session["UserName"].ToString();

					torreModel = torreController.connectTorre(userName).Item1;
					torreBioChecked = true;
				}
				if (torreBioChecked && linkedInChecked)
					Session["MergeLITorre"] = new Models.MergeLITorre() { LIProfile = profile, TorreProfile = torreModel };

				return RedirectToAction("Index", "Home");

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Tuple<Models.BasicProfile, IRestResponse> connectLinkedin(string access_token)
		{
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
					access_token);

			string url = string.Format(@"https://api.linkedin.com/v1/people/{0}", querystring);

			var client = new RestClient(url);
			var request = new RestRequest(Method.GET);
			var response = client.Execute(request);

			JsonDeserializer deserializer = new JsonDeserializer();
			var profile = deserializer.Deserialize<Models.BasicProfile>(response);

			return new Tuple<Models.BasicProfile, IRestResponse>(profile, response);
		}
	}
}