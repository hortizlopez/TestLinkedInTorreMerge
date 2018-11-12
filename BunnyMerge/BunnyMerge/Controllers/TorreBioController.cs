using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BunnyMerge.Controllers
{
    public class TorreBioController : Controller
    {
        // GET: TorreBio
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult SignIn()
		{
			return View();
		}

		public ActionResult SignInT (string username)
		{
			var tuple = connectTorre(username);
			var content = tuple.Item1;
			var response = tuple.Item2;

			var torreBioChecked = false;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				Session["userName"] = content.person.publicId;
				Session["userId"] = content.person.id;
				torreBioChecked = true;
			}

			var linkedInModel = new Models.BasicProfile();
			var linkedInChecked = false;
			if (Session["LIUserId"] != null && Session["LIUserId"] != string.Empty)
			{
				var linkedInController = new LinkedInController();
				var userId = Session["UserId"].ToString();
				var userName = Session["UserName"].ToString();
				var access_token = Session["access_token"].ToString();

				linkedInModel = linkedInController.connectLinkedin(access_token).Item1;
				linkedInChecked = true;
			}

			if (torreBioChecked && linkedInChecked)
				return RedirectToAction("Index", "Home", new Tuple<Models.TorreProfile, Models.BasicProfile>(content, linkedInModel));
			else
				return RedirectToAction("Index", "Home");
		}

		public Tuple<Models.TorreProfile, IRestResponse> connectTorre (string username)
		{
			var client = new RestClient(string.Format("https://torre.bio/api/bios/{0}", username));
			var request = new RestRequest(Method.GET);
			IRestResponse response = client.Execute(request);

			JsonDeserializer deserializer = new JsonDeserializer();
			var content = deserializer.Deserialize<Models.TorreProfile>(response);
			
			return new Tuple<Models.TorreProfile, IRestResponse>(content, response);
		}
    }
}