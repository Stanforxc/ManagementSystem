using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Crub;
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
            insertDevice();

        }

        private Boolean insertDevice(){
            DeviceCrubOperator decOperator = new DeviceCrubOperator();

            EntityModel.device dev = new EntityModel.device();

            dev.name = "ipad2";
            dev.desc = "嘉勇爸爸的ipad2";
            dev.is_safety = Constant.safety;

            return decOperator.create(dev);

        }
    }

    

}
