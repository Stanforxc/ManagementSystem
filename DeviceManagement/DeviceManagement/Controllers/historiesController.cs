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
using Crud;
using System.Web.Http.Cors;


namespace DeviceManagement.Controllers
{

    [EnableCors("*", "*", "*")]
    public class historiesController : ApiController
    {
        private Entities db = new Entities();

        private HistoryCrudOperator historyCrudOperator = new HistoryCrudOperator();

        // GET: api/histories
        public List<history> Gethistories()
        {
            List<history> ret_list = (from his in db.histories select his).ToList();

            foreach (var item in ret_list) {
                db.Entry(item).State = EntityState.Detached;
            }

            return ret_list;
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

            history.time = DateTime.Now.ToString();
            this.historyCrudOperator.create(history);
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

        [HttpGet]
        [Route("api/histories/get_his_user")]
        public  List<history> getHisOfUser(string user_id) {

            List<history> ret = (from his in db.histories where his.user_id.CompareTo(user_id) == 0 select his).ToList();
            foreach (var item in ret)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return ret;
        }

        [HttpGet]
        [Route("api/histories/get_his_dev")]
        public List<history> getHisOfDev(int dev_id)
        {

            List<history> ret = (from his in db.histories where his.device_id == dev_id select his).ToList();
            foreach (var item in ret)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return ret;
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