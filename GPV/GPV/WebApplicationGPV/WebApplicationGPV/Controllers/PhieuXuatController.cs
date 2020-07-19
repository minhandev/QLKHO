using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplicationGPV.Dao;

namespace WebApplicationGPV.Controllers
{
    public class PhieuXuatController : RoutingController
    {
        phieuxuatDao phieuxuatDao = new phieuxuatDao();
        public ActionResult Index(DateTime? batdau, DateTime? ketthuc)
        {
            var data = phieuxuatDao.GetPhieuXuats();
            if (batdau.HasValue)
            {
                data = data.Where(x => x.ThoiGian.Value >= batdau.Value).ToList();
            }
            if (ketthuc.HasValue)
            {
                data = data.Where(x => x.ThoiGian.Value <= ketthuc.Value).ToList();
            }
            return View(data);
        }
    }
}