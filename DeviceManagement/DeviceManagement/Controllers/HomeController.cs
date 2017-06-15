using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Crud;
using EntityModel;

namespace DeviceManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            dbTest();

            ViewBag.Title = "Home Page";

            return View();
        }

        private void dbTest() {
            //insertDevice();
            //queryTest();
            deleteTest();
        }

        private Boolean insertDevice(){
            DeviceCrubOperator decOperator = new DeviceCrubOperator();

            EntityModel.device dev = new EntityModel.device();

            dev.name = "ipad2";
            dev.desc = "嘉勇爸爸的ipad2";
            dev.is_safety = Constant.safety;

            return decOperator.create(dev);

        }

        private void queryTest() {
            DeviceCrubOperator decOperator = new DeviceCrubOperator();

            device list = decOperator.queryById(2);

            list.name = "ipad air";

            decOperator.update(list);

        }

        private void deleteTest() {
            UserCrubOperator op = new UserCrubOperator();
            DeviceCrubOperator dop = new DeviceCrubOperator();
            dop.delete(3);
            Console.Write(1);
            
        }

    }

    

}
