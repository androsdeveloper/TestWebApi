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
    public class InsurancesController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Insurances
        public IQueryable<Insurance> GetInsurances()
        {
            return db.Insurances;
        }

        // GET: api/Insurances/5
        [ResponseType(typeof(Insurance))]
        public IHttpActionResult GetInsurance(int id)
        {
            Insurance insurance = db.Insurances.Find(id);
            if (insurance == null)
            {
                return NotFound();
            }

            return Ok(insurance);
        }

        // PUT: api/Insurances/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInsurance(int id, Insurance insurance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != insurance.InsuranceId)
            {
                return BadRequest();
            }

            db.Entry(insurance).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuranceExists(id))
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

        // POST: api/Insurances
        [ResponseType(typeof(Insurance))]
        public IHttpActionResult PostInsurance(Insurance insurance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Insurances.Add(insurance);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = insurance.InsuranceId }, insurance);
        }

        // DELETE: api/Insurances/5
        [ResponseType(typeof(Insurance))]
        public IHttpActionResult DeleteInsurance(int id)
        {
            Insurance insurance = db.Insurances.Find(id);
            if (insurance == null)
            {
                return NotFound();
            }

            db.Insurances.Remove(insurance);
            db.SaveChanges();

            return Ok(insurance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InsuranceExists(int id)
        {
            return db.Insurances.Count(e => e.InsuranceId == id) > 0;
        }
    }
}