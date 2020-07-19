using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplicationGPV.Dao;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class PhieuNhapController : RoutingController
    {
        dbQLKhoEntities db = new dbQLKhoEntities();
        private phieunhapDao phieunhapDao = new phieunhapDao();
        private SanPhamDao SanPhamDao = new SanPhamDao();
        public ActionResult Index(DateTime? batdau, DateTime? ketthuc)
        {
            var data = phieunhapDao.GetPhieuNhaps();
            foreach(var item in data)
            {
                ViewBag.SanPham = db.SanPhams.Where(x=> x.ID_PhieuNhap == item.ID_PhieuNhap).ToList();
                if (batdau.HasValue)
                {
                    data = data.Where(x => x.ThoiGian.Value >= batdau.Value).ToList();
                }
                if (ketthuc.HasValue)
                {
                    data = data.Where(x => x.ThoiGian.Value <= ketthuc.Value).ToList();
                }
            }
            return View(data);
        }
    }
}