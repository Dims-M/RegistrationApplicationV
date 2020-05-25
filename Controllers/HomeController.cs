using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
            // return RedirectToAction("Areas/Admin/Dashboard");
        }

        public ActionResult AddNewClent()
        {

            //Переадресуем а админку
            return Redirect("/Admin/PagesClients/GetAllClients");
        }

        public ActionResult Cabinet()
        {
           // ViewBag.Message = "Your contact page.";

            return Redirect("/Admin/Dashboard/Index");
        }
    }
}