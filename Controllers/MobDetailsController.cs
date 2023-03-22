using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using mob_monitoring_api.Models;

namespace mob_monitoring_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class MobDetailsController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();

        [HttpPost]
        public HttpResponseMessage AddDetail(MobDetail entity)
        {
            db.MobDetail.Add(entity);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpGet]
        public HttpResponseMessage GetDetails(int id)
        {
            var md = db.MobDetail.Where(x => x.MobID_FK == id).FirstOrDefault();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MobDetailExists(int id)
        {
            return db.MobDetail.Count(e => e.DetailID == id) > 0;
        }
    }
}