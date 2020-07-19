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
    public class MohinhController : RoutingController
    {
        private khoDao khoDao = new khoDao();
        private vitrisuaDao vitrisuaDao = new vitrisuaDao();
        private nhacugcapDao nhacugcapDao = new nhacugcapDao();
        private SanPhamDao SanPhamDao = new SanPhamDao();
        private phieuxuatDao phieuxuatDao = new phieuxuatDao();
        private NhomspDao NhomspDao = new NhomspDao();
        public ActionResult Index()
        {
            ViewBag.lstNhaKho = khoDao.GetNhaKhos();
            ViewBag.lstViTriSua = vitrisuaDao.GetVitriSuas();
            ViewBag.selectNSS = new SelectList(NhomspDao.GetNhomSanPhams().Where(x => x.Status == true), "ID_Nhom", "TenNhom");
            ViewBag.selectNCC = new SelectList(nhacugcapDao.GetNhacugcaps().Where(x => x.Status == true), "MaNCC", "TenNCC");
            ViewBag.selectHH = new SelectList(SanPhamDao.GetSanPhams().Where(x => x.Status == true), "MaHH", "TenHH");
            return View(SanPhamDao.GetSanPhams().Where(x => x.Status == true).ToList());
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

        [HttpGet]
        public JsonResult Detail(string id)
        {
            var items = SanPhamDao.GetSanPhams();
            var sanpham = new SanPham();
            foreach (var item in items)
            {
                if (item.MaHH == id)
                {
                    sanpham.MaHH = item.MaHH;
                    sanpham.TenHH = item.TenHH;
                    sanpham.NSX = item.NSX;
                    sanpham.HSD = item.HSD;
                    sanpham.DonGia = item.DonGia;
                    sanpham.MaNCC = item.NhaCungCap.TenNCC;
                }
            }
            return Json(sanpham, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Convert(string id)
        {
            var items = SanPhamDao.GetSanPhams();
            var sanpham = new SanPham();
            foreach (var item in items)
            {
                if (item.MaHH == id)
                {
                    sanpham.MaHH = item.MaHH;
                    sanpham.TenHH = item.TenHH;
                    sanpham.NSX = item.NSX;
                    sanpham.HSD = item.HSD;
                    sanpham.DonGia = item.DonGia;
                    sanpham.MaNCC = item.NhaCungCap.TenNCC;
                }
            }
            return Json(sanpham, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Convert(SanPham sp)
        {
            var data = SanPhamDao.SanPham(sp.MaHH);
            if (data.MaHH == sp.MaHH)
            {
                data.Tang = sp.Tang;
                data.Hang = sp.Hang;
                data.Cot = sp.Cot;
                SanPhamDao.Update();
            }
            return Json(data: new { message = "Thành công ", success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}