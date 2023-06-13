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
        [HttpPost]
        public HttpResponseMessage UpdateDrone(Drone drone)
        {
            try
            {
                var d = db.Drone.Where(x => x.DroneID == drone.DroneID).FirstOrDefault();
                if (d != null)
                {
                    d = drone;
                    db.SaveChanges();
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception) { 
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        public HttpResponseMessage DeleteDrone(int id)
        {
            try
            {
                var drone = db.Drone.Where(x => x.DroneID == id).FirstOrDefault();
                db.Drone.Remove(drone);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpGet]
        public HttpResponseMessage GetAllotedDrones(int id)
        {
            try
            {
                var droneIDs = db.AllotedDrones.Where(x => x.UserID_FK == id).Select(x => x.DroneID_FK).ToList();
                if(droneIDs .Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No Drones Allocated Yet");
                }
                var drones = db.Drone.Where(x => droneIDs.Contains(x.DroneID)).ToList();
                if(drones.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No Drones Allocated Yet");
                }

                return Request.CreateResponse(HttpStatusCode.OK, drones);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetUnAllocatedDrones()
        {
            try
            {
                var droneIds = db.AllotedDrones.Select(x => x.DroneID_FK).ToList();
                var drones = db.Drone.Where(x => !droneIds.Contains(x.DroneID)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, drones);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetAllDrones()
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