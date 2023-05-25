using mob_monitoring_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace mob_monitoring_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class MobCoordController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();

        [HttpPost]
        public HttpResponseMessage GetMobCoords(int mobID)
        {
            try
            {
                var mCoords = db.PreDefCoords.Where(x => x.MobID_FK == mobID).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, mCoords);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddMobCoords(List<MobCoords> mobCoords)
        {
            try
            {
                db.MobCoords.AddRange(mobCoords);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
