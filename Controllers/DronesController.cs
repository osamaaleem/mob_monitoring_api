using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using mob_monitoring_api.Models;

namespace mob_monitoring_api.Controllers
{
    public class DronesController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();
        [HttpPost]
        public HttpResponseMessage AddDrone([FromBody]Drone drone)
        {
            try
            {
                db.Drone.Add(drone);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            
        }
        [HttpGet]
        public HttpResponseMessage GetAllDroness()
        {
            try
            {
                var Droness = db.Drone.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, Droness);
            }
            catch(Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DronesExists(int id)
        {
            return db.Drone.Count(e => e.DroneID == id) > 0;
        }
    }
}