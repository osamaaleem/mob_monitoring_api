﻿using System;
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
using mob_monitoring_api.Models;

namespace mob_monitoring_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
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
            try
            {
                String role;
                User u = db.User.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
                if (u != null)
                {
                    UserModel us = new UserModel();
                    us.UserID = u.UserID;
                    us.Name = u.Name;
                    us.Email = u.Email;
                    us.Password = u.Password;

                    us.Role = u.Role;
                    us.Organization = u.Organization;

                    return Request.CreateResponse(HttpStatusCode.OK, us);
                    /*role = u.ToString();
                    string message = $"{{\"message\": \"{role}\"}}";
                    HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.OK);
                    res.Content = new StringContent(message, Encoding.UTF8, "application/json");
                    return res;*/
                }
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }   
        }
        [HttpGet]
        public HttpResponseMessage GetUserByEmail(string email)
        {
            try
            {
                int id = db.User.Where(x => x.Email == email).Select(x => x.UserID).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK,id);
            }
            catch 
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetAllUsers()
        {
            var users = db.User.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }
        [HttpGet]
        public HttpResponseMessage DeleteUser(int id)
        {
            var user = db.User.Where(x => x.UserID == id).FirstOrDefault();
            if (user != null)
            {
                db.User.Remove(user);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPost]
        public HttpResponseMessage UpdateUser(User user)
        {
            var u = db.User.Where(x => x.UserID == user.UserID).FirstOrDefault();
            if (u != null) {
                u = user;
                db.SaveChanges();
            }
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

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.UserID == id) > 0;
        }
    }
}