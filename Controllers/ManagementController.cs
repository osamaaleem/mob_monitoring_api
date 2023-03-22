using mob_monitoring_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Http.Cors;

namespace mob_monitoring_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class ManagementController : ApiController
    {
        
        private FYP_DBEntities db = new FYP_DBEntities();
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                Management management = new Management();
                management.Mobs = db.Mob.Where(x => x.IsActive == true).Select(x => x.Name).ToList();
                management.Users = db.User.Where(x => x.Role == "Standard").Select(x => x.Name).ToList();
                management.Drones = db.Drone.Where(x => x.IsAvailable == true).Select(x => x.Name).ToList();
                management.Operators = db.User.Where(x => x.Role == "Operator").Select(x => x.Name).ToList();
                management.RedZones = db.RedZone.Select(x => x.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, management);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            
        }
    }
}
