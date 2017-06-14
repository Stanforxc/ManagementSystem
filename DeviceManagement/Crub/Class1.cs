using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;

namespace Crub
{
    public class Class1
    {
        public Boolean create(device insert_item)
        {
            try
            {
                hjyEntities entity = new hjyEntities();
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

        public void queryAll()
        {
            try
            {
                hjyEntities entity = new hjyEntities();

                var users = from d in entity.users select d;

                Console.Write("te");

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                //return null;
            }
        }
    }
}
