using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using mob_monitoring_api.Models;

namespace mob_monitoring_api.Controllers
{
    public class MobsController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();
        [HttpPost]
        public HttpResponseMessage AddMob([FromBody] Mob mob)
        {
            db.Mob.Add(mob);
            db.SaveChanges();
            var id = db.Mob.Where(x => x.Name == mob.Name).Select(x => x.MobID).FirstOrDefault();
            MobDetail mobDetail = new MobDetail();
            mobDetail.MobID_FK = id;
            db.MobDetail.Add(mobDetail);
            db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpGet]
        public HttpResponseMessage GetMob(int id)
        {
            var m = db.Mob.Where(x => x.MobID == id).FirstOrDefault();
            if (m == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, m);
        }
        [HttpGet]
        public HttpResponseMessage GetAllMobs()
        {
            //TODO:only the active mobs
            List<Mob> mList = db.Mob.ToList();
            if (mList != null && mList.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, mList);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No mobs found.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetInactiveMobs()
        {
            var mobs = db.Mob.Where(x => x.IsActive == false).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, mobs);
        }
        [HttpGet]
        public HttpResponseMessage GetActiveMobs()
        {
            var mobs = db.Mob.Where(x => x.IsActive == true).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, mobs);
        }
        public HttpResponseMessage UpdateMobs(Mob m)
        {
            Mob mob = db.Mob.Where(x => x.MobID == m.MobID).FirstOrDefault();
            mob = m;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpGet]
        public HttpResponseMessage AssignedUser()
        {
            List<Mob> mList = new List<Mob>();
            var id = db.MobDetail.Where(x => x.UsersID_FK != null).Select(x => x.MobID_FK);
            foreach(int i in id)
            {
                Mob m = db.Mob.Where(x => x.MobID == i).FirstOrDefault();
                mList.Add(m);
            }
            if(mList.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No mobs found.");
            }
            return Request.CreateResponse(HttpStatusCode.OK,mList);
        }
        [HttpGet]
        public HttpResponseMessage NotAssignedUser()
        {
            List<Mob> mList = new List<Mob>();
            var id = db.MobDetail.Where(x => x.UsersID_FK == null).Select(x => x.MobID_FK).ToList();
            foreach (int i in id)
            {
                Mob m = db.Mob.Where(x => x.MobID == i).FirstOrDefault();
                mList.Add(m);
            }
            if (mList.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No mobs found.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, mList);
        }
        [HttpGet]
        public HttpResponseMessage WithDrone()
        {
            List<Mob> mList = new List<Mob>();
            var id = db.MobDetail.Where(x => x.DroneID_FK != null).Select(x => x.MobID_FK);
            foreach (int i in id)
            {
                Mob m = db.Mob.Where(x => x.MobID == i).FirstOrDefault();
                mList.Add(m);
            }
            if (mList.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No mobs found.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, mList);
        }
        [HttpGet]
        public HttpResponseMessage WithoutDrone()
        {
            List<Mob> mList = new List<Mob>();
            var id = db.MobDetail.Where(x => x.UsersID_FK == null).Select(x => x.MobID_FK);
            foreach (int i in id)
            {
                Mob m = db.Mob.Where(x => x.MobID == i).FirstOrDefault();
                mList.Add(m);
            }
            if (mList.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No mobs found.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, mList);
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