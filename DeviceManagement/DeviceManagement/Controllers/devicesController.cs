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
    public class devicesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/devices
        public List<device> Getdevices()
        {
            var ret_list = db.devices.ToList();
            foreach(var item in ret_list) {
                db.Entry(item).State = EntityState.Detached;
            }
            return ret_list;
        }

        // GET: api/devices/5
        [ResponseType(typeof(device))]
        public async Task<IHttpActionResult> Getdevice(int id)
        {
            device device = await db.devices.FindAsync(id);

            db.Entry(device).State = EntityState.Detached;


            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        // PUT: api/devices/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdevice(int id, device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.id)
            {
                return BadRequest();
            }

            db.Entry(device).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!deviceExists(id))
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

        // POST: api/devices
        [ResponseType(typeof(device))]
        public async Task<IHttpActionResult> Postdevice(device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.devices.Add(device);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = device.id }, device);
        }

        // DELETE: api/devices/5
        [ResponseType(typeof(device))]
        public async Task<IHttpActionResult> Deletedevice(int id)
        {
            device device = await db.devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            db.devices.Remove(device);
            await db.SaveChangesAsync();

            return Ok(device);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool deviceExists(int id)
        {
            return db.devices.Count(e => e.id == id) > 0;
        }
    }
}