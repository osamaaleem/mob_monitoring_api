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
        //[HttpGet]
        //public HttpResponseMessage Get()
        //{
        //    try
        //    {
        //        Management management = new Management();
        //        management.Mobs = db.Mob.Where(x => x.IsActive == true).ToList();
        //        management.Users = db.User.Where(x => x.Role == "Standard").Select(x => x.Name).ToList();
        //        management.Drones = db.Drone.Where(x => x.IsAvailable == true).Select(x => x.Name).ToList();
        //        management.Operators = db.User.Where(x => x.Role == "Operator").Select(x => x.Name).ToList();
        //        management.RedZones = db.RedZone.Select(x => x.Name).ToList();
        //        return Request.CreateResponse(HttpStatusCode.OK, management);
        //    }
        //    catch (Exception)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError);
        //    }
            
        //}
        [HttpGet]
        public HttpResponseMessage GetOperators()
        {
            try
            {
                var operators = db.User.Where(x => x.Role == "Operator").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, operators);
            }
            catch(Exception) { return Request.CreateResponse(HttpStatusCode.InternalServerError); }
            
        }

        [HttpGet]
        public HttpResponseMessage GetOfficers()
        {
            try
            {
                var officers = db.User.Where(x => x.Role == "Officer").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, officers);
            }
            catch (Exception) { return Request.CreateResponse(HttpStatusCode.InternalServerError); }

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
        [HttpGet]
        public HttpResponseMessage AllocateMobToOperator(int mobId, int userId)
        {
            try
            {
                MobOperator mo = new MobOperator();
                mo.MobId_FK = mobId;
                mo.UserId_FK = userId;
                db.MobOperator.Add(mo);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) { return Request.CreateResponse(HttpStatusCode.InternalServerError); }
        }
        [HttpGet]
        public HttpResponseMessage getMobByOfficerId(int offId)
        {
            try
            {
                var mobIds = db.MobOfficer.Where(x => x.UserId_FK == offId).Select(x => x.MobId_FK).ToList();
                var mobs = db.Mob.Where(x => mobIds.Contains(x.MobID)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, mobs);
            }
            catch(Exception) { return Request.CreateResponse(HttpStatusCode.InternalServerError); }
        }

        [HttpGet]
        public HttpResponseMessage getMobByOperatorId(int opId)
        {
            try
            {
                var mobId = db.MobOperator.Where(x => x.UserId_FK == opId).Select(x => x.MobId_FK).ToList();
                var mobs = db.Mob.Where(x => mobId.Contains(x.MobID)).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, mobs);
            }
            catch (Exception) { return Request.CreateResponse(HttpStatusCode.InternalServerError); }
        }

        [HttpGet]
        public HttpResponseMessage AllocateMobToOfficer(int mobId, int userId)
        {

            try
            {
                MobOfficer mo = new MobOfficer();
                mo.MobId_FK = mobId;
                mo.UserId_FK = userId;
                db.MobOfficer.Add(mo);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception) { return Request.CreateResponse(HttpStatusCode.InternalServerError); }
        }
        
    }
}
