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
    public class UsersController : ApiController
    {
        private FYP_DBEntities db = new FYP_DBEntities();

        [HttpPost]
        public HttpResponseMessage Register(User user)
        {
            db.User.Add(user);
            db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpPost]
        public HttpResponseMessage Login(User user)
        {
            String role = "Not Found";
            var u = db.User.Where(x => x.Email == user.Email && x.Password == user.Password).Select(x => x.Role).FirstOrDefault();
            if (u != null)
            {
                role = u.ToString();
            }
            string message = $"{{\"message\": \"{role}\"}}";
            HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.OK);
            res.Content = new StringContent(message, Encoding.UTF8, "application/json");
            return res;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.UserID == id) > 0;
        }
    }
}