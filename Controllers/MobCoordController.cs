using Microsoft.Ajax.Utilities;
using mob_monitoring_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace mob_monitoring_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class MobCoordController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();

        [HttpPost]
        public HttpResponseMessage GetMobCoords(int mobID)
        {
            try
            {
                var mCoords = db.PreDefCoords.Where(x => x.MobID_FK == mobID).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, mCoords);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddMobCoords(List<PreDefCoords> mobCoords)
        {
            try
            {
                db.PreDefCoords.AddRange(mobCoords);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetPreDefCoords(int mobId)
        {
            try
            {
                var preDefCoords = db.PreDefCoords.Where(x => x.MobID_FK == mobId).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, preDefCoords);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetMobsByOfficerId(int id)
        {
            try
            {
                var mobIds = db.MobOfficer.Where(x => x.UserId_FK == id).Select(x => x.MobId_FK).ToList();
                var mobs = db.Mob.Where(x => mobIds.Contains(x.MobID)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, mobs);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetMobsByOperatorId(int id)
        {
            try
            {
                var mobIds = db.MobOperator.Where(x => x.UserId_FK == id).Select(x => x.MobId_FK).ToList();
                var mobs = db.Mob.Where(x => mobIds.Contains(x.MobID)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, mobs);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetMobsWithoutPreDefCoords(int mobId)
        {
            try
            {
                var defMobIds = db.PreDefCoords.Select(x => x.MobID_FK).ToList();
                var mobs = db.Mob.Where(x => !defMobIds.Contains(x.MobID)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, mobs);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
