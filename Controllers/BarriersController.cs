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
        [HttpGet]
        public HttpResponseMessage GetBarriers(int id)
        {
            var b = db.Barrier.Where(x => x.MobID_FK == id);
            return Request.CreateResponse(HttpStatusCode.OK,b);
        }
        public HttpResponseMessage AddBarrier(Barrier b) {
            db.Barrier.Add(b);
            return Request.CreateResponse(HttpStatusCode.OK);
        
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