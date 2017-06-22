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

    [EnableCors("*","*","*")]
    public class device_userController : ApiController
    {
        private Entities db = new Entities();

        private DeviceCrubOperator deviceCrudOperator = new DeviceCrubOperator();

        private UserCrubOperator userCrudOperator = new UserCrubOperator();

        // GET: api/device_user
        public List<device_user> Getdevice_user()
        {
            List<device_user> ret_list = (from du in db.device_user select du).ToList();

            foreach (var item in ret_list) {
                db.Entry(item).State = EntityState.Deleted;
            }

            return ret_list;
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
                if (!deviceCrudOperator.update()) {
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

            string user_id = device_user.user_id;
            int dev_id = device_user.device_id;
            user u = this.userCrudOperator.queryById(user_id);
            device d = this.deviceCrudOperator.queryById(dev_id);

            if (null == u || null == d)
            {
                return NotFound();
            }

            if (deviceCrudOperator.addUserToDevice(d, u))
            {
                if (!deviceCrudOperator.update())
                {
                    return NotFound();
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return NotFound();
            }

            return CreatedAtRoute("DefaultApi", new { id = device_user.rela_id }, device_user);
        }

        // DELETE: api/device_user/delete?dev_id=...&user_id=...  test
        [ResponseType(typeof(device_user))]
        [HttpGet]
        [Route("api/device_user/delete")]
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

        [HttpGet]
        [Route("api/device_user/get_user")]
        public List<user> getUserOfDevice(int device_id) {

            device d = this.deviceCrudOperator.queryById(device_id);

            return this.deviceCrudOperator.getAllUserOfDevice(d);
        }

        [HttpGet]
        [Route("api/device_user/get_device")]
        public List<device> getDeviceOfUser(string user_id)
        {
            user u = this.userCrudOperator.queryById(user_id);

            return this.userCrudOperator.getAllDeviceOfUser(u);
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