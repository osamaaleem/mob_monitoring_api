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
    public class RedZonesController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();

        // GET: api/RedZones
        public IQueryable<RedZone> GetRedZone()
        {
            return db.RedZone;
        }

        // GET: api/RedZones/5
        [ResponseType(typeof(RedZone))]
        public IHttpActionResult GetRedZone(int id)
        {
            RedZone redZone = db.RedZone.Find(id);
            if (redZone == null)
            {
                return NotFound();
            }

            return Ok(redZone);
        }

        // PUT: api/RedZones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRedZone(int id, RedZone redZone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != redZone.RedZoneID)
            {
                return BadRequest();
            }

            db.Entry(redZone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RedZoneExists(id))
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

        // POST: api/RedZones
        [ResponseType(typeof(RedZone))]
        public IHttpActionResult PostRedZone(RedZone redZone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RedZone.Add(redZone);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = redZone.RedZoneID }, redZone);
        }

        // DELETE: api/RedZones/5
        [ResponseType(typeof(RedZone))]
        public IHttpActionResult DeleteRedZone(int id)
        {
            RedZone redZone = db.RedZone.Find(id);
            if (redZone == null)
            {
                return NotFound();
            }

            db.RedZone.Remove(redZone);
            db.SaveChanges();

            return Ok(redZone);
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