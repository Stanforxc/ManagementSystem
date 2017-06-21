using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;

namespace Crud
{
    public class DeviceCrubOperator
    {
        private Entities entity = new Entities();

        public Boolean create(device insert_item)
        {
            try
            {
                entity.devices.Add(insert_item);
                entity.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }

        public List<device> queryByType_1(string type_1) {
            try
            {
                return (from d in entity.devices where d.type_1.CompareTo(type_1) == 0 select d).ToList();
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return null;
            }
        }

        public List<device> queryAll()
        {
            try
            {
                var devices = from d in entity.devices select d;

                return devices.ToList();

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }

        public List<device> queryByName(string name) {
            try
            {
                var devices = from du in entity.device_user where 0 == du.user.username.CompareTo(name) select du.device;

                return devices.ToList();
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return null;
            }

        }


        public device queryById(int id)
        {
            try
            {
                Entities entity = new Entities();

                var devices = from d in entity.devices where d.id == id select d;

                return devices.AsEnumerable().First();
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


        public Boolean delete(int id) {
            try
            {
                device dev_to_del = (from d in entity.devices where d.id == id select d).First();

                //删除用户设备表
                List<device_user> du_list = (from du in entity.device_user where du.device_id == id select du).ToList();

                entity.device_user.RemoveRange(du_list);

                //删除历史纪录
                List<history> his_list = (from his in entity.histories where his.device_id == id select his).ToList();

                entity.histories.RemoveRange(his_list);

                entity.devices.Remove(dev_to_del);

                entity.SaveChanges();

                return true;
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return false;
            }
        }

        public Boolean delete(device dev) {
            try
            {
                return delete(dev.id);
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return false;
            }
        }

        public List<user> getAllUserOfDevice(device d) {
            try
            {
                return (from du in d.device_user select du.user).ToList();
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return null;
            }
        }

        public Boolean addUserToDevice(device d, user u) {
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

        public Boolean deleteUserFromDevice(device d, user u) {
            try
            {
                device_user record = (from du in entity.device_user where du.device_id == d.id where du.user_id.CompareTo(u.id)==0 select du).First();

                entity.device_user.Remove(record);

                entity.SaveChanges();

                return true;
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return false;
            }
        }

        public int sumOfType_1(string type_1) {
            try {
                return (from d in entity.devices where d.type_1.CompareTo(type_1) == 0 select d).Count();
            }catch (Exception e) {
                Console.Write(e.Message);
                return 0;
            }
        }

        ~DeviceCrubOperator() {
            entity.Dispose();
        }
    }
}
