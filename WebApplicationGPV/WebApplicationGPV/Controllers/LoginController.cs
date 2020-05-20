using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationGPV.Common;
using WebApplicationGPV.Dao;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            var LoginDao = new LoginDao();
            var nvSession = new nguoidungLogin();
            if (ModelState.IsValid)
            {
                var result = LoginDao.Login(model.taikhoan, model.MatKhau, true);
                if (result == 1)
                {
                    var nhanvien = LoginDao.NhanVien(model.taikhoan);
                    var capquyen = LoginDao.Getcapquyen(nhanvien.MaNV);
                    nvSession.MaNV = nhanvien.MaNV;
                    nvSession.TenNV = nhanvien.TenNV;
                    nvSession.image = nhanvien.image;
                    Session.Add(sessionSite.SESSION_CAPQUYEN, capquyen);
                    Session.Add(sessionSite.USER_SESSION, nvSession);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session.Remove(sessionSite.USER_SESSION);
            Session.Remove(sessionSite.SESSION_CAPQUYEN);
            Session.Abandon();
            return RedirectToAction("index");
        }
    }
}