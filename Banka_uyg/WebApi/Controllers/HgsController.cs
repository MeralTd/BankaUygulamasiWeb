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
using WebApi.Models;

namespace WebApi.Controllers
{
    public class HgsController : ApiController
    {
        private KurumDBEntities db = new KurumDBEntities();

        // GET: api/Hgs
        public IQueryable<Hgs> GetHgs()
        {
            return db.Hgs;
        }

        // GET: api/Hgs/5
        [ResponseType(typeof(Hgs))]
        public IHttpActionResult GetHgs(int id)
        {
            Hgs hgs = db.Hgs.Find(id);
            if (hgs == null)
            {
                return NotFound();
            }

            return Ok(hgs);
        }

        // PUT: api/Hgs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHgs(int id, Hgs hgs)
        {
          

            if (id != hgs.HgsId)
            {
                return BadRequest();
            }

            db.Entry(hgs).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HgsExists(id))
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

        // POST: api/Hgs
        [ResponseType(typeof(Hgs))]
        public IHttpActionResult PostHgs(Hgs hgs)
        {
           

            db.Hgs.Add(hgs);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hgs.HgsId }, hgs);
        }

        // DELETE: api/Hgs/5
        [ResponseType(typeof(Hgs))]
        public IHttpActionResult DeleteHgs(int id)
        {
            Hgs hgs = db.Hgs.Find(id);
            if (hgs == null)
            {
                return NotFound();
            }

            db.Hgs.Remove(hgs);
            db.SaveChanges();

            return Ok(hgs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HgsExists(int id)
        {
            return db.Hgs.Count(e => e.HgsId == id) > 0;
        }
    }
}