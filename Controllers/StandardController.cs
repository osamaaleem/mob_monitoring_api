using mob_monitoring_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mob_monitoring_api.Controllers
{
    public class StandardController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();
        [HttpGet]
        public HttpResponseMessage GetDetails(int userID)
        {
            var mb = db.MobDetail.Where(x => x.UsersID_FK == userID).FirstOrDefault();
            if (mb != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,mb);
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
        [HttpGet]
        public HttpResponseMessage GetDrone(int droneID)
        {
            var mb = db.Drone.Where(x => x.DroneID == droneID).FirstOrDefault();
            if (mb != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, mb);
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
        [HttpGet]
        public HttpResponseMessage GetMob(int mobID)
        {
            var mb = db.Mob.Where(x => x.MobID == mobID).FirstOrDefault();
            if (mb != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, mb);
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
