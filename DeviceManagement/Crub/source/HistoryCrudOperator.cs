using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CLI;

namespace Crud
{
    public class HistoryCrudOperator
    {

        private Entities entity = new Entities();

        private DeviceCrubOperator deviceCrudOp = new DeviceCrubOperator();

        private ExceptionLog exception = new ExceptionLog();

        public Boolean create(history insert_item) {
            try
            {
                entity.histories.Add(insert_item);
                entity.SaveChanges();
                return true;
            }
            catch (Exception e) {
                exception.log(e.Message);
                return true;
            }
        }

        public List<history> queryAll() {
            try
            {
                return (from h in entity.histories select h).ToList();
            }
            catch (Exception e) {
                exception.log(e.Message);
                return null;
            }
        }

        public List<history> queryByDevice(device d) {
            try
            {
                return (from h in entity.histories where h.device_id == d.id select h).ToList();
            }
            catch (Exception e) {
                exception.log(e.Message);
                return null;
            }
        }

        public List<history> queryByUser(user u) {
            try
            {
                return (from h in entity.histories where h.user_id.CompareTo(u.id) == 0 select h).ToList();
            }
            catch (Exception e) {
                exception.log(e.Message);
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
                exception.log(e.Message);
                return false;
            }
        }

        public List<history> getAllHisOfUser(user u) {
            try
            {
                var ret = (from his in u.histories select his).ToList();

                foreach (var item in ret)
                {
                    //item.device = null;
                    //item.user = null;
                    entity.Entry(item).State = EntityState.Detached;
                }
                return ret;
            }
            catch (Exception e)
            {
                exception.log(e.Message);
                return null;
            }
        }

        public List<history> getAllHisOfDev(device d)
        {
            try
            {
                var ret = (from his in d.histories select his).ToList();

                foreach (var item in ret)
                {
                    //item.device = null;
                    //item.user = null;
                    entity.Entry(item).State = EntityState.Detached;
                    
                }
                entity.SaveChanges();
                return ret;
            }
            catch (Exception e)
            {
                exception.log(e.Message);
                return null;
            }
        }


        public List<string> bestOfType_1(string type_1) {
            try
            {
                var count_table = from h_1 in entity.histories
                                  where h_1.device.type_1.CompareTo(type_1)==0
                                  group h_1.user_id by h_1.device_id into g
                                  select new { id = g.Key, count = g.Count()};

                List<string> ret_list = new List<string>();

                foreach (var item in count_table) {
                    device d = deviceCrudOp.queryById(item.id);
                    ret_list.Add(d.name);
                }

                return ret_list;
            }
            catch (Exception e) {
                exception.log(e.Message);
                return null;
            }
        }
    }
}
