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
        public HttpResponseMessage GetRedZonesByMobId(int id)
        {
            try
            {
                var zoneIds = db.AllotedRedZones.Where(x => x.MobID_FK == id).Select(x => x.RedZoneID_FK).ToList();
                if(zoneIds.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var zones = db.RedZone.Where(x => zoneIds.Contains(x.RedZoneID)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, zones);
            }
            catch(Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetUnallocatedRedzones()
        {
            try
            {
                var zoneIds = db.AllotedRedZones.Select(x => x.RedZoneID_FK).ToList();
                var zones = db.RedZone.Where(x => !zoneIds.Contains(x.RedZoneID)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, zones);
            }
            catch(Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateRedzone(RedZone r)
        {
            try
            {
                var redZone = db.RedZone.Where(x => x.RedZoneID == r.RedZoneID).FirstOrDefault();
                redZone.Name = r.Name;
                redZone.IsActive = r.IsActive;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetRedZoneIdByName(String name) 
        {
            try
            {
                var redZoneName = db.RedZone.Where(x => x.Name == name).Select(x => x.RedZoneID).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, redZoneName);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateRedzoneCoords(List<RedZoneCoordinates> rcList,int id)
        {
            try
            {
                var redZone = db.RedZone.Where(x => x.RedZoneID == id).FirstOrDefault();
                db.RedZoneCoordinates.RemoveRange(db.RedZoneCoordinates.Where(x => x.RedZoneID_FK == id));
                db.RedZoneCoordinates.AddRange(rcList);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeleteRedzoneCoords(int id)
        {
            try
            {
                db.RedZoneCoordinates.RemoveRange(db.RedZoneCoordinates.Where(x => x.RedZoneID_FK == id));
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddRedZoneCoords(List<RedZoneCoordinates> rcList)
        {
            try
            {
                db.RedZoneCoordinates.AddRange(rcList);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetRedZoneCoordsById(int zoneId)
        {
            try
            {
                var coords = db.RedZoneCoordinates.Where(x => x.RedZoneID_FK == zoneId).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, coords);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
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
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPost]
        public HttpResponseMessage DeleteZone(int id)
        {
            var z = db.RedZone.Where(x => x.RedZoneID == id).FirstOrDefault();
            db.RedZone.Remove(z);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPost]
        public HttpResponseMessage UpdateZone(RedZone r)
        {
            var z = db.RedZone.Where(x => x.RedZoneID == r.RedZoneID).FirstOrDefault();
            z = r;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

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