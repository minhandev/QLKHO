using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class DeveloperController : RoutingController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string MatKhau)
        {
            var db = new dbQLKhoEntities();
            string redirectUrl = string.Empty;
            string message = string.Empty;
            var result = db.Developers.SingleOrDefault(x => x.MatKhau == MatKhau);
            if (result.MatKhau == MatKhau)
            {
                message = "ĐĂNG NHẬP THÀNH CÔNG!";
                redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Developer");
            }
            else
            {
                message = "ĐĂNG NHẬP KHÔNG THÀNH CÔNG!";
            }
            return Json(data: new { message = message, success = true, url = redirectUrl }, JsonRequestBehavior.AllowGet);
        }
    }
}