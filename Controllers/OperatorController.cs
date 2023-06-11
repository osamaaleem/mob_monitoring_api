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
    public class OperatorController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();
        [HttpGet]
        public HttpResponseMessage GetOperatorsWithoutMobs()
        {
            try
            {
                var alOfficerIds = db.MobOperator.Select(x => x.UserId_FK).ToList();
                var officers = db.User.Where(x => x.Role == "Operator" && !alOfficerIds.Contains(x.UserID)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, officers);
            }
            catch (Exception) { return Request.CreateResponse(HttpStatusCode.InternalServerError); }
        }
    }
}

