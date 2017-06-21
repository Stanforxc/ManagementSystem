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

namespace DeviceManagement.Controllers
{
    public class device_userController : ApiController
    {
        private Entities db = new Entities();

        private DeviceCrubOperator deviceCrudOperator = new DeviceCrubOperator();

        private UserCrubOperator userCrudOperator = new UserCrubOperator();

        // GET: api/device_user
        public IQueryable<device_user> Getdevice_user()
        {
            return db.device_user;
        }

        // GET: api/device_user/5
        [ResponseType(typeof(device_user))]
        public async Task<IHttpActionResult> Getdevice_user(int id)
        {
            device_user device_user = await db.device_user.FindAsync(id);
            if (device_user == null)
            {
                return NotFound();
            }

            return Ok(device_user);
        }

        // PUT: api/device_user/5 test
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdevice_user(int id, device_user device_user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string user_id = device_user.user_id;
            int dev_id = device_user.device_id;
            user u = this.userCrudOperator.queryById(user_id);
            device d = this.deviceCrudOperator.queryById(dev_id);

            if (null == u || null == d) {
                return NotFound();
            }

            if (deviceCrudOperator.addUserToDevice(d, u))
            {
                if (deviceCrudOperator.update()) {
                    return NotFound();
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            else {
                return  NotFound();
            }
            //db.Entry(device_user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!device_userExists(id))
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




        // POST: api/device_user
        [ResponseType(typeof(device_user))]
        public async Task<IHttpActionResult> Postdevice_user(device_user device_user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.device_user.Add(device_user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (device_userExists(device_user.rela_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = device_user.rela_id }, device_user);
        }

        // DELETE: api/device_user?dev_id=...&user_id=...  test
        [ResponseType(typeof(device_user))]
        [HttpGet]
        [Route("api/device_user")]
        public async Task<IHttpActionResult> Deletedevice_user(int dev_id, string user_id)
        {

            device d = deviceCrudOperator.queryById(dev_id);
            user u = userCrudOperator.queryById(user_id);

            if (deviceCrudOperator.deleteUserFromDevice(d, u))
            {
                return Ok();
            }
            else {
                return NotFound();
            }

            //device dev = this.deviceCrudOperator.queryById(dev_id);
            //user u = this.userCrudOperator.queryById(user_id);

            //if (deviceCrudOperator.deleteUserFromDevice(dev, u))
            //{
            //    return Ok();
            //}
            //else {
            //    return NotFound();
            //}
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool device_userExists(int id)
        {
            return db.device_user.Count(e => e.rela_id == id) > 0;
        }
    }
}