using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationGPV.Dao;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class MohinhController : Controller
    {
        // GET: Mohinh
        private khoDao khoDao = new khoDao();
        private vitrisuaDao vitrisuaDao = new vitrisuaDao();
        private nhacugcapDao nhacugcapDao = new nhacugcapDao();
        private hanghoaDao hanghoaDao = new hanghoaDao();
        private lohangDao lohangDao = new lohangDao();
        private phieuxuatDao phieuxuatDao = new phieuxuatDao();
        public ActionResult Index()
        {
            ViewBag.lstNhaKho = khoDao.GetNhaKhos();
            ViewBag.lstViTriSua = vitrisuaDao.GetVitriSuas();
            ViewBag.selectNCC = new SelectList(nhacugcapDao.GetNhacugcaps().Where(x => x.Status == true), "MaNCC", "TenNCC");
            ViewBag.selectHH = new SelectList(hanghoaDao.GetHangHoas().Where(x => x.Status == true), "MaHH", "TenHH");
            return View(lohangDao.GetLoHangs().Where(x => x.Status != 0).ToList());
        }

        public JsonResult AddViTriSua(VitriSua ob)
        {
            return Json(vitrisuaDao.Insert(ob));
        }

        public JsonResult Fixed(int id)
        {
            var ob = vitrisuaDao.GetVitriSuas().FirstOrDefault(q => q.ID_ViTri == id);
            if (ob == null) return Json(true);
            return Json(vitrisuaDao.Delete(id));
        }


        public ActionResult Detail(int id)
        {
            var ob = lohangDao.GetLoHangs().FirstOrDefault(q => q.MaLoHang == id);
            return View(ob);
        }
    }
}