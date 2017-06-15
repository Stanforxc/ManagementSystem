using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;
using Crud;

namespace Service.source
{
    class StatisticService
    {
        private DeviceCrubOperator dev_op = new DeviceCrubOperator();

        private HistoryCrudOperator his_op = new HistoryCrudOperator();

        public int sumOfType_1(string type_1)
        {
            try
            {
                return dev_op.sumOfType_1(type_1);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return 0;
            }
        }

        public device bestOfType_1(string type_1)
        {
            try
            {
                int best_id = his_op.bestOfType_1(type_1);

                return dev_op.queryById(best_id);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }

        public int safeSumOfType_1(string type_1) {
            try
            {
                List<device> list = dev_op.queryByType_1(type_1);
                return (from item in list where item.is_safety == true select item).Count();
            }
            catch (Exception e) {
                Console.Write(e.Message);
                return 0;
            }
        }


    }
}
