using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplicationGPV.Common;
using WebApplicationGPV.Dao;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class HomeController : RoutingController
    {
        private SanPhamDao SanPhamDao = new SanPhamDao();
        private nhanvienDao nhanvienDao = new nhanvienDao();
        public ActionResult Index()
        {
            ViewBag.SP = SanPhamDao.GetSanPhams();
            ViewBag.XK = new dbQLKhoEntities().CT_XuatKho.ToList();
            ViewBag.NV = nhanvienDao.GetNhanViens();
            return View();
        }

        public ActionResult Nofication()
        {
            return View();
        }
    }
}