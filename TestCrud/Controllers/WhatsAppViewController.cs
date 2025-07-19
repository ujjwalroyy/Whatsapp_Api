using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestCrud.Controllers
{
    public class WhatsAppViewController : Controller
    {
        public ActionResult SendWhatsAppMessage()
        {
            return View();
        }
    }
}