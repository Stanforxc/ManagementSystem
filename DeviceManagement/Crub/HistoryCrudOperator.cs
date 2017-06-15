using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;
namespace Crub
{
    class HistoryCrudOperator
    {

        private Entities entity = new Entities();

        public Boolean create(history insert_item) {
            try
            {
                entity.histories.Add(insert_item);
                entity.SaveChanges();
                return true;
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return true;
            }
        }

        public List<history> queryAll() {
            try
            {
                return (from h in entity.histories select h).ToList();
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return null;
            }
        }

        public List<history> queryByDevice(device d) {
            try
            {
                return (from h in entity.histories where h.device_id == d.id select h).ToList();
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return null;
            }
        }

        public List<history> queryByUser(user u) {
            try
            {
                return (from h in entity.histories where h.user_id.CompareTo(u.id) == 0 select h).ToList();
            }
            catch (Exception e) {
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

    }
}
