using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EntityModel;

namespace DeviceManagement.Controllers
{
    public class historiesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/histories
        public IQueryable<history> Gethistories()
        {
            return db.histories;
        }

        // GET: api/histories/5
        [ResponseType(typeof(history))]
        public async Task<IHttpActionResult> Gethistory(int id)
        {
            history history = await db.histories.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        // PUT: api/histories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puthistory(int id, history history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != history.id)
            {
                return BadRequest();
            }

            db.Entry(history).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!historyExists(id))
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

        // POST: api/histories
        [ResponseType(typeof(history))]
        public async Task<IHttpActionResult> Posthistory(history history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.histories.Add(history);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = history.id }, history);
        }

        // DELETE: api/histories/5
        [ResponseType(typeof(history))]
        public async Task<IHttpActionResult> Deletehistory(int id)
        {
            history history = await db.histories.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            db.histories.Remove(history);
            await db.SaveChangesAsync();

            return Ok(history);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool historyExists(int id)
        {
            return db.histories.Count(e => e.id == id) > 0;
        }
    }
}