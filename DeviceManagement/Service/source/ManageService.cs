using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud;
using EntityModel;


namespace Service.source
{
    class ManageService
    {
        private DeviceCrubOperator dev_op = new DeviceCrubOperator();
        private HistoryCrudOperator his_op = new HistoryCrudOperator();

        public Boolean enterRepo(string name, string type_1, string type_2, string type_3, string desc, sbyte status, sbyte is_safety) {
            try
            {
                DateTime in_time = DateTime.Now;
                string in_time_str = String.Format("yyyy-mm-dd", in_time);

                device dev = new device();
                dev.name = name;
                dev.type_1 = type_1;
                dev.type_2 = type_2;
                dev.type_3 = type_3;
                dev.in_time = in_time_str;
                dev.desc = desc;
                dev.status = status;
                dev.is_safety = is_safety;

                return dev_op.create(dev);

            }
            catch (Exception e) {
                Console.Write(e.Message);
                return false;
            }
        }

        /*
         * 按页查询， 如果pageSize = -1 就是打表
         * **/
        public List<device> queryByPage(int pageSize=-1, int pageIndex=-1) {
            try
            {
                List<device> dev_list = dev_op.queryAll();
                if (-1 == pageSize) {
                    dev_list = dev_list.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
                }
                return dev_list;
            }catch(Exception e){
                Console.Write(e.Message);
                return null;
            }
        }

        public Boolean deleteDev(int id) {
            try
            {
                return dev_op.delete(id);
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return false;
            }
        }

        /*待测*/
        public Boolean changeAttribute(int id, string attribute, Object new_val) {

            try
            {
                if (attribute.CompareTo("id") == 0) {
                    return false;
                }
                device dev = dev_op.queryById(id);
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

                System.Reflection.PropertyInfo property = assembly.GetType("EntityModel.device", true).GetProperty(attribute);
                property.SetValue(dev, new_val, null);
                dev_op.update();
                return true;
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return false;
            }
        }
    }
}
