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
    public class BarriersController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();

        // GET: api/Barriers
        public IQueryable<Barrier> GetBarrier()
        {
            return db.Barrier;
        }

        // GET: api/Barriers/5
        [ResponseType(typeof(Barrier))]
        public IHttpActionResult GetBarrier(int id)
        {
            Barrier barrier = db.Barrier.Find(id);
            if (barrier == null)
            {
                return NotFound();
            }

            return Ok(barrier);
        }

        // PUT: api/Barriers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBarrier(int id, Barrier barrier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != barrier.BarrierID)
            {
                return BadRequest();
            }

            db.Entry(barrier).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarrierExists(id))
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

        // POST: api/Barriers
        [ResponseType(typeof(Barrier))]
        public IHttpActionResult PostBarrier(Barrier barrier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Barrier.Add(barrier);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = barrier.BarrierID }, barrier);
        }

        // DELETE: api/Barriers/5
        [ResponseType(typeof(Barrier))]
        public IHttpActionResult DeleteBarrier(int id)
        {
            Barrier barrier = db.Barrier.Find(id);
            if (barrier == null)
            {
                return NotFound();
            }

            db.Barrier.Remove(barrier);
            db.SaveChanges();

            return Ok(barrier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BarrierExists(int id)
        {
            return db.Barrier.Count(e => e.BarrierID == id) > 0;
        }
    }
}