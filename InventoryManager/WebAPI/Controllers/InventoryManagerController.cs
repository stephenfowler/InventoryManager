using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class InventoryManagerController : ApiController
    {
        
        [HttpGet]
        public IHttpActionResult Retrieve(string request)
        {
            IWorkerAPI worker = (IWorkerAPI) Configuration.Services.GetService(typeof (IWorkerAPI));
            string response = worker.Retrieve(request);
            return Json(response);
        }

        [HttpPost]
        public IHttpActionResult Add(string request)
        {
            string fakeAuth =
                "I Didn't implement the auth. In theory the request would have the auth token on it and I could pick it off of the request content";
            IWorkerAPI worker = (IWorkerAPI)Configuration.Services.GetService(typeof(IWorkerAPI));
            worker.Add(request, fakeAuth);
            return Ok();
        }
    }
}
