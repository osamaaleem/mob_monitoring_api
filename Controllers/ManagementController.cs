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
                management.Mobs = db.Mob.Where(x => x.IsActive == true).ToList();
                management.Users = db.User.Where(x => x.Role == "Standard").Select(x => x.Name).ToList();
                management.Drones = db.Drone.Where(x => x.IsAvailable == true).Select(x => x.Name).ToList();
                management.Operators = db.User.Where(x => x.Role == "Operator").Select(x => x.Name).ToList();
                management.RedZones = db.RedZone.Select(x => x.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, management);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            
        }
        [HttpGet]
        public HttpResponseMessage GetOperators()
        {
            try
            {
                var operators = db.User.Where(x => x.Role == "Operator").Select(x => x.Name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, operators);
            }
            catch(Exception) { return Request.CreateResponse(HttpStatusCode.InternalServerError); }
            
        }
        [HttpPost]
        public HttpResponseMessage AllotDrones(AllotedDrones ad)
        {
            try
            {
                db.AllotedDrones.Add(ad);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            
        }
        public HttpResponseMessage AllocateMobToOperator(int mobId, int userId)
        {
            MobDetail md = new MobDetail();
            md.MobID_FK = mobId;
            md.UsersID_FK = userId;
        }
    }
}
