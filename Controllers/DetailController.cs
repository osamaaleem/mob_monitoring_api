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
    public class DetailController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();
        [HttpGet]
        public HttpResponseMessage getMobDetailById(int mobId)
        {
            try
            {
                Mob m = db.Mob.Where(x => x.MobID == mobId).FirstOrDefault();
                var mobCoords = db.MobCoords.Where(x => x.MobID_FK == mobId).ToList();
                var operatorId = db.MobOperator.Where(x => x.MobId_FK == mobId).Select(x => x.UserId_FK).FirstOrDefault();
                var officerId = db.MobOfficer.Where(x => x.MobId_FK == mobId).Select(x => x.UserId_FK).FirstOrDefault();
                var redzoneIds = db.AllotedRedZones.Where(x => x.MobID_FK == mobId).Select(x => x.RedZoneID_FK).ToList();
                var redzones = db.RedZone.Where(x => redzoneIds.Contains(x.RedZoneID)).ToList();
                var mobOperator = db.User.Where(x => x.UserID == operatorId).FirstOrDefault();
                var mobOfficer = db.User.Where(x => x.UserID == officerId).FirstOrDefault();
                var mobDetail = new
                {
                    mob = m,
                    mobCoords = mobCoords,
                    mobOperator = mobOperator,
                    mobOfficer = mobOfficer,
                    redzones = redzones
                };
                return Request.CreateResponse(HttpStatusCode.OK, mobDetail);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage getRedzonedDetialById(int zoneId)
        {
            try
            {
                var zone = db.RedZone.Where(x => x.RedZoneID == zoneId).FirstOrDefault();
                var zoneCoords = db.RedZoneCoordinates.Where(x => x.RedZoneID_FK == zoneId).ToList();
                var zoneDetail = new
                {
                    zone = zone,
                    zoneCoords = zoneCoords
                };
                return Request.CreateResponse(HttpStatusCode.OK, zoneDetail);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }   
    }
}
