using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using mob_monitoring_api.Models;

namespace mob_monitoring_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class RedZonesController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();

        [HttpGet]
        public HttpResponseMessage GetAllZones()
        {
            var z = db.RedZone.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, z);
        }
        [HttpGet]
        public HttpResponseMessage GetActiveZones()
        {
            var z = db.RedZone.Where(x => x.IsActive == "1").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, z);
        }
        [HttpGet]
        public HttpResponseMessage GetInActiveMobs()
        {
            var z = db.RedZone.Where(x => x.IsActive == "0").ToList();
            return Request.CreateResponse(HttpStatusCode.OK,z);
        }
        [HttpPost]
        public HttpResponseMessage AddRedZone(RedZone r)
        {
            db.RedZone.Add(r);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPost]
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RedZoneExists(int id)
        {
            return db.RedZone.Count(e => e.RedZoneID == id) > 0;
        }
    }
}