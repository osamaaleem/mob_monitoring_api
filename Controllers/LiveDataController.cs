using Microsoft.AspNetCore.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using mob_monitoring_api.Models;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http.Cors;

namespace mob_monitoring_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class LiveDataController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();
        [HttpPost]
        public HttpResponseMessage postCoord(MobCoords coords)
        {
            try
            {
                db.MobCoords.Add(coords);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
