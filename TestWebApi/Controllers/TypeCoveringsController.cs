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
    public class TypeCoveringsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/TypeCoverings
        public IQueryable<TypeCovering> GetTypeCoverings()
        {
            return db.TypeCoverings;
        }

        // GET: api/TypeCoverings/5
        [ResponseType(typeof(TypeCovering))]
        public IHttpActionResult GetTypeCovering(int id)
        {
            TypeCovering typeCovering = db.TypeCoverings.Find(id);
            if (typeCovering == null)
            {
                return NotFound();
            }

            return Ok(typeCovering);
        }

        // PUT: api/TypeCoverings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTypeCovering(int id, TypeCovering typeCovering)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeCovering.TypeCoveringId)
            {
                return BadRequest();
            }

            db.Entry(typeCovering).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeCoveringExists(id))
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

        // POST: api/TypeCoverings
        [ResponseType(typeof(TypeCovering))]
        public IHttpActionResult PostTypeCovering(TypeCovering typeCovering)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypeCoverings.Add(typeCovering);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = typeCovering.TypeCoveringId }, typeCovering);
        }

        // DELETE: api/TypeCoverings/5
        [ResponseType(typeof(TypeCovering))]
        public IHttpActionResult DeleteTypeCovering(int id)
        {
            TypeCovering typeCovering = db.TypeCoverings.Find(id);
            if (typeCovering == null)
            {
                return NotFound();
            }

            db.TypeCoverings.Remove(typeCovering);
            db.SaveChanges();

            return Ok(typeCovering);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeCoveringExists(int id)
        {
            return db.TypeCoverings.Count(e => e.TypeCoveringId == id) > 0;
        }
    }
}