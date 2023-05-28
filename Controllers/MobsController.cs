using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using mob_monitoring_api.Models;

namespace mob_monitoring_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class MobsController : ApiController
    {
       
        private FYP_DBEntities db = new FYP_DBEntities();
        [HttpPost]
        public HttpResponseMessage AddMob([FromBody] Mob mob)
        {
            db.Mob.Add(mob);
            db.SaveChanges();
            var id = db.Mob.Where(x => x.Name == mob.Name).Select(x => x.MobID).FirstOrDefault();
            //MobDetail mobDetail = new MobDetail();
            //mobDetail.MobID_FK = id;
            //db.MobDetail.Add(mobDetail);
            db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpPost]
        public HttpResponseMessage UpdateMob(Mob mob)
        {
            var m = db.Mob.Where(x => x.MobID == mob.MobID).FirstOrDefault();
            m = mob;
            db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpPost]
        public HttpResponseMessage DeleteMob(int id)
        {
            var m = db.Mob.Where(x => x.MobID == id).FirstOrDefault();
            db.Mob.Remove(m); 
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
                List<Mob> mNullList = removeNullValues(mList);
                return Request.CreateResponse(HttpStatusCode.OK, mNullList);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No mobs found.");
            }
        }
        //[HttpGet]
        //public HttpResponseMessage GetMobsByUserId(int id)
        //{
        //    try
        //    {
        //        var mobIDs = db.MobDetail.Where(x => x.UsersID_FK == id).Select(x => x.MobID_FK).ToList();
        //        if (mobIDs.Count == 0)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NoContent, "No Mobs Alloted Yet");
        //        }
        //        var mobs = db.Mob.Where(x => mobIDs.Contains(x.MobID)).ToList();
        //        if (mobs.Count == 0)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NoContent, "No Mobs Alloted Yet");
        //        }

        //        return Request.CreateResponse(HttpStatusCode.OK, mobs);
        //    }
        //    catch (Exception)
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //    }
        //}
        [HttpGet]
        public HttpResponseMessage GetInactiveMobs()
        {
            var mobs = db.Mob.Where(x => x.IsActive == false).ToList();
            List<Mob> mNullList = removeNullValues(mobs);
            return Request.CreateResponse(HttpStatusCode.OK, mNullList);
        }
        [HttpGet]
        public HttpResponseMessage GetActiveMobs()
        {
            var mobs = db.Mob.Where(x => x.IsActive == true).ToList();
            List<Mob> mNullList = removeNullValues(mobs);
            return Request.CreateResponse(HttpStatusCode.OK, mNullList);
        }
        public HttpResponseMessage UpdateMobs(Mob m)
        {
            Mob mob = db.Mob.Where(x => x.MobID == m.MobID).FirstOrDefault();
            mob = m;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpGet]
        public HttpResponseMessage GetMobsWithoutOperators()
        {
            try
            {
                var mobFKeys = db.MobOperator.Select(x => x.MobId_FK).ToList();
                var mobs = db.Mob.Where(x => !mobFKeys.Contains(x.MobID)).ToList();
                List<Mob> mNullList = removeNullValues(mobs);
                return Request.CreateResponse(HttpStatusCode.OK, mNullList);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetMobsWithOperators()
        {
            try
            {
                var mobFKeys = db.MobOperator.Select(x => x.MobId_FK).ToList();
                var mobs = db.Mob.Where(x => mobFKeys.Contains(x.MobID)).ToList();
                List<Mob> mNullList = removeNullValues(mobs);
                return Request.CreateResponse(HttpStatusCode.OK, mNullList);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage GetMobsWithoutOfficers()
        {
            try
            {
                var mobFKeys = db.MobOfficer.Select(x => x.MobId_FK).ToList();
                var mobs = db.Mob.Where(x => !mobFKeys.Contains(x.MobID)).ToList();
                List<Mob> mNullList = removeNullValues(mobs);
                return Request.CreateResponse(HttpStatusCode.OK, mNullList);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetMobsWithOfficer()
        {
            try
            {
                var mobFKeys = db.MobOfficer.Select(x => x.MobId_FK).ToList();
                var mobs = db.Mob.Where(x => mobFKeys.Contains(x.MobID)).ToList();
                List<Mob> mNullList = removeNullValues(mobs);
                return Request.CreateResponse(HttpStatusCode.OK, mNullList);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        //[HttpGet]
        //public HttpResponseMessage AssignedUser()
        //{
        //    List<Mob> mList = new List<Mob>();
        //    var id = db.MobDetail.Where(x => x.UsersID_FK != null).Select(x => x.MobID_FK);
        //    foreach(int i in id)
        //    {
        //        Mob m = db.Mob.Where(x => x.MobID == i).FirstOrDefault();
        //        mList.Add(m);
        //    }
        //    if(mList.Count == 0)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No mobs found.");
        //    }
        //    List<Mob> mNullList = removeNullValues(mList);
        //    return Request.CreateResponse(HttpStatusCode.OK, mNullList);
        //}
        //[HttpGet]
        //public HttpResponseMessage NotAssignedUser()
        //{
        //    List<Mob> mList = new List<Mob>();
        //    var id = db.MobDetail.Where(x => x.UsersID_FK == null).Select(x => x.MobID_FK).ToList();
        //    foreach (int i in id)
        //    {
        //        Mob m = db.Mob.Where(x => x.MobID == i).FirstOrDefault();
        //        mList.Add(m);
        //    }
        //    if (mList.Count == 0)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No mobs found.");
        //    }
        //    List<Mob> mNullList = removeNullValues(mList);
        //    return Request.CreateResponse(HttpStatusCode.OK, mNullList);
        //}
        //[HttpGet]
        //public HttpResponseMessage WithDrone()
        //{
        //    List<Mob> mList = new List<Mob>();
        //    var id = db.MobDetail.Where(x => x.DroneID_FK != null).Select(x => x.MobID_FK);
        //    foreach (int i in id)
        //    {
        //        Mob m = db.Mob.Where(x => x.MobID == i).FirstOrDefault();
        //        mList.Add(m);
        //    }
        //    if (mList.Count == 0)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No mobs found.");
        //    }
        //    List<Mob> mNullList = removeNullValues(mList);
        //    return Request.CreateResponse(HttpStatusCode.OK, mNullList);
        //}
        //[HttpGet]
        //public HttpResponseMessage WithoutDrone()
        //{
        //    List<Mob> mList = new List<Mob>();
        //    var id = db.MobDetail.Where(x => x.UsersID_FK == null).Select(x => x.MobID_FK);
        //    foreach (int i in id)
        //    {
        //        Mob m = db.Mob.Where(x => x.MobID == i).FirstOrDefault();
        //        mList.Add(m);
        //    }
        //    if (mList.Count == 0)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No mobs found.");
        //    }
        //    List<Mob> mNullList = removeNullValues(mList);
        //    return Request.CreateResponse(HttpStatusCode.OK, mNullList);
        //}
        private List<Mob> removeNullValues(List<Mob> mobs)
        {
            var list = new List<Mob>();
            foreach (Mob m in mobs)
            {
                if (m.ProputedStrength == null || m.StartDate == null || m.IsActive == null || m.MobStartLat == null || m.MobStartLon == null)
                {
                    continue;
                }
                list.Add(m);
            }
            return list;
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