using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;

namespace Crud
{
    public class UserCrubOperator
    {

        private Entities entity = new Entities();

        public Boolean create(user insert_item) {
            try {
                entity.users.Add(insert_item);
                entity.SaveChanges();
                return true;
            }catch(Exception e){
                Console.Write(e.Message);
                return false;
            }
        }

        public List<user> queryAll() {
            try
            {
                return (from u in entity.users select u).ToList(); ;
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return null;
            }
        }

        public user queryById(string id) {
            try
            {
                return (from u in entity.users where u.id.CompareTo(id) == 0 select u).First();
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return null;
            }
        }

        public user queryByName(string name)
        {
            try
            {
                return (from u in entity.users where u.username.CompareTo(name) == 0 select u).First();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }

        public Boolean update() {
            try
            {
                entity.SaveChanges();
                return true;
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return false;
            }
        }

        public Boolean delete(string id) {
            try
            {
                user user_to_delete = (from u in entity.users where u.id.CompareTo(id) == 0 select u).First();

                entity.users.Remove(user_to_delete);

                entity.SaveChanges();

                return true;

            }
            catch (Exception e) {
                Console.Write(e.Message);
                return false;
            }
        }

        public Boolean delete(user u) {
            try
            {
                return delete(u.id);
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return false;
            }
        }

        
        public List<device> getAllDeviceOfUser(user renter) {
            try
            {
                IEnumerable<device> ret = (from du in renter.device_user
                                          select du.device).AsEnumerable();
                return ret.ToList();
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return null;
            }
        }

        public Boolean addDeviceToUser(user u, device d) {
            try
            {
                device_user du = new device_user();

                du.device_id = d.id;
                du.user_id = u.id;
                entity.device_user.Add(du);
                entity.SaveChanges();

                return true;
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return false;
            }
        }

        public Boolean deleteDeviceFromUser(user u, device d) {
            try
            {
                device_user record = (from du in u.device_user where du.device_id == d.id select du).First();

                entity.device_user.Remove(record);

                entity.SaveChanges();

                return true;
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return false;
            }
        }
        ~UserCrubOperator() {
            entity.Dispose();
        }
        
    }
}
