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
    public class TypeRisksController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/TypeRisks
        public IQueryable<TypeRisk> GetTypeRisks()
        {
            return db.TypeRisks;
        }

        // GET: api/TypeRisks/5
        [ResponseType(typeof(TypeRisk))]
        public IHttpActionResult GetTypeRisk(int id)
        {
            TypeRisk typeRisk = db.TypeRisks.Find(id);
            if (typeRisk == null)
            {
                return NotFound();
            }

            return Ok(typeRisk);
        }

        // PUT: api/TypeRisks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTypeRisk(int id, TypeRisk typeRisk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeRisk.TypeRiskId)
            {
                return BadRequest();
            }

            db.Entry(typeRisk).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeRiskExists(id))
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

        // POST: api/TypeRisks
        [ResponseType(typeof(TypeRisk))]
        public IHttpActionResult PostTypeRisk(TypeRisk typeRisk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypeRisks.Add(typeRisk);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = typeRisk.TypeRiskId }, typeRisk);
        }

        // DELETE: api/TypeRisks/5
        [ResponseType(typeof(TypeRisk))]
        public IHttpActionResult DeleteTypeRisk(int id)
        {
            TypeRisk typeRisk = db.TypeRisks.Find(id);
            if (typeRisk == null)
            {
                return NotFound();
            }

            db.TypeRisks.Remove(typeRisk);
            db.SaveChanges();

            return Ok(typeRisk);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeRiskExists(int id)
        {
            return db.TypeRisks.Count(e => e.TypeRiskId == id) > 0;
        }
    }
}