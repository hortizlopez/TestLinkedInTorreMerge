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
			var client = new RestClient(string.Format("https://torre.bio/api/bios/{0}",username));
			var request = new RestRequest(Method.GET);
			IRestResponse response = client.Execute(request);

			JsonDeserializer deserializer = new JsonDeserializer();
			var content = deserializer.Deserialize<Models.TorreProfile>(response);

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				Session["userName"] = content.person.publicId;
				Session["userId"] = content.person.id;
			}


			return View();
		}
    }
}