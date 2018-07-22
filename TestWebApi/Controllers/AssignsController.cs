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
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    public class AssignsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Assigns
        public IQueryable<Assign> GetAssigns()
        {
            return db.Assigns;
        }

        // GET: api/Assigns/5
        [ResponseType(typeof(Assign))]
        public IHttpActionResult GetAssign(int id)
        {
            Assign assign = db.Assigns.Find(id);
            if (assign == null)
            {
                return NotFound();
            }

            return Ok(assign);
        }

        // PUT: api/Assigns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssign(int id, Assign assign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assign.AssignId)
            {
                return BadRequest();
            }

            db.Entry(assign).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignExists(id))
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

        // POST: api/Assigns
        [ResponseType(typeof(Assign))]
        public IHttpActionResult PostAssign(Assign assign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assigns.Add(assign);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assign.AssignId }, assign);
        }

        // DELETE: api/Assigns/5
        [ResponseType(typeof(Assign))]
        public IHttpActionResult DeleteAssign(int id)
        {
            Assign assign = db.Assigns.Find(id);
            if (assign == null)
            {
                return NotFound();
            }

            db.Assigns.Remove(assign);
            db.SaveChanges();

            return Ok(assign);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssignExists(int id)
        {
            return db.Assigns.Count(e => e.AssignId == id) > 0;
        }
    }
}