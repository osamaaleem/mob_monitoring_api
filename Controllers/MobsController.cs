using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using mob_monitoring_api.Models;

namespace mob_monitoring_api.Controllers
{
    public class MobsController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();

        // GET: api/Mobs
        public IQueryable<Mob> GetMob()
        {
<<<<<<< HEAD
            db.Mob.Add(mob);
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
            //TODO:also search by user and drone assigned
            List<Mob> mList = db.Mob.ToList();
            if (mList != null && mList.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, mList);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No mobs found.");
            }
=======
            return db.Mob;
>>>>>>> parent of 1321a0c (refactored-controllers)
        }

        // GET: api/Mobs/5
        [ResponseType(typeof(Mob))]
        public IHttpActionResult GetMob(int id)
        {
<<<<<<< HEAD
            //date wise
            var mobs = db.Mob.Where(x => x.IsActive == false).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, mobs);
=======
            Mob mob = db.Mob.Find(id);
            if (mob == null)
            {
                return NotFound();
            }

            return Ok(mob);
>>>>>>> parent of 1321a0c (refactored-controllers)
        }

        // PUT: api/Mobs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMob(int id, Mob mob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mob.MobID)
            {
                return BadRequest();
            }

            db.Entry(mob).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Mobs
        [ResponseType(typeof(Mob))]
        public IHttpActionResult PostMob(Mob mob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Mob.Add(mob);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mob.MobID }, mob);
        }

        // DELETE: api/Mobs/5
        [ResponseType(typeof(Mob))]
        public IHttpActionResult DeleteMob(int id)
        {
            Mob mob = db.Mob.Find(id);
            if (mob == null)
            {
                return NotFound();
            }

            db.Mob.Remove(mob);
            db.SaveChanges();

            return Ok(mob);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MobExists(int id)
        {
            return db.Mob.Count(e => e.MobID == id) > 0;
        }
    }
}