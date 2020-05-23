using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// Работа с заявками  полученых с формы. 
    /// </summary>
    public class PageApplicationsDocumentsController : Controller
    {
        // GET: Admin/PageApplicationsDocuments
        public ActionResult Index()
        {
            return View();
        }
    }
}