using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LesGamblers.Web.Controllers
{
    public class GamblersController : Controller
    {
        [HttpGet]
        public ActionResult ListGamblers()
        {
            return View();
        }
    }
}