using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InventoryManager;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class WorkerController : ApiController
    {
        IWorkerAPI _worker;

        public IHttpActionResult Retrieve(string item, string auth)
        {
            string response = _worker.Retrieve(item, auth);
            if (response.Equals("Out of Stock") || response.Equals("not found"))
            {
                return NotFound();
            }
            return Ok(response);
        }

        public IHttpActionResult Add

    }
}
