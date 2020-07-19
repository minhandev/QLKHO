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
    public class NhomSanPhamController : RoutingController
    {
        NhomspDao NhomspDao = new NhomspDao();
        public ActionResult Index()
        {
            return View(NhomspDao.GetNhomSanPhams());
        }

        [HttpPost]
        public ActionResult Index(NhomSanPham nsp)
        {
            string message = string.Empty;
            if (nsp.ID_Nhom == 0)
            {
                NhomSanPham nv = new NhomSanPham()
                {
                    TenNhom = nsp.TenNhom,
                    Status = true
                };
                NhomspDao.Insert(nv);
                message = "Thêm xong";
            }
            else
            {
                var nv = NhomspDao.NhomSanPham(nsp.ID_Nhom);
                nv.TenNhom = nsp.TenNhom;
                NhomspDao.Update();
                message = "Cập nhật xong";
            }
            return Json(data: new { message = "Thành công " + message, success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            NhomspDao.Delete(id);
            return Json(data: new { message = "Xóa thành công ", success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Send(int id)
        {
            var nv = new NhomSanPham();
            List<NhomSanPham> nhomSanPhams = NhomspDao.GetNhomSanPhams();
            foreach (var item in nhomSanPhams)
            {
                if (item.ID_Nhom == id)
                {
                    nv.ID_Nhom = item.ID_Nhom;
                    nv.TenNhom = item.TenNhom;
                }
            }
            return Json(nv, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new NhomspDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            }); ;
        }

        [ChildActionOnly]
        public ActionResult Add()
        {
            return PartialView();
        }


    }
}