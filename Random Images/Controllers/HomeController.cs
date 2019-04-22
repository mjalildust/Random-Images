using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Random_Images.Controllers;
using Random_Images.Models;

namespace Random_Images.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            History his = new History();

            return View();
        }


        /// <summary>
        /// save user's history 
        /// </summary>
        /// <param name="LData"></param>
        /// <returns></returns>
        public ActionResult SetLike(UserPreference LData)
        {
            History his = new History();
            string result = his.SetUserPreference(LData);
            return Json(new { msg = result.ToString() });

        }
        /// <summary>
        /// Get user's history
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ActionResult GetHistory(string username)
        {
            username = username.Trim();
            History his = new History();

            return Json(his.GetHistory(username), JsonRequestBehavior.AllowGet);

        }



    }
}