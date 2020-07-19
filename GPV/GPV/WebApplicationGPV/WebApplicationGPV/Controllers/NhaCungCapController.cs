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
    public class NhaCungCapController : RoutingController
    {
        nhacugcapDao nhacugcapDao = new nhacugcapDao();
        // GET: NhomHangHoa
        public ActionResult Index()
        {
            return View(nhacugcapDao.GetNhacugcaps());
        }

        [HttpPost]
        public ActionResult Index(NhaCungCap hh)
        {
            string message = string.Empty;
            var nhhs = new NhaCungCap()
            {
                MaNCC = hh.MaNCC,
                TenNCC = hh.TenNCC,
                DiaChi = hh.DiaChi,
                SDT = hh.SDT,
                Web = hh.Web,
                Status = true
            };
            nhacugcapDao.Insert(nhhs);
            message = "Thêm xong";
            return Json(data: new { message = "Thành công " + message, success = true }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Send(string id)
        {
            List<NhaCungCap> items = nhacugcapDao.GetNhacugcaps();
            var nhh = new NhaCungCap();
            foreach (var item in items)
            {
                if (item.MaNCC == id)
                {
                    nhh.MaNCC = item.MaNCC;
                    nhh.TenNCC = item.TenNCC;
                    nhh.DiaChi = item.DiaChi;
                    nhh.SDT = item.SDT;
                    nhh.Web = item.Web;
                }
            }
            return Json(nhh, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(NhaCungCap item)
        {
            string message = string.Empty;
            var nhh = nhacugcapDao.nhaCungCap(item.MaNCC);
            nhh.TenNCC = item.TenNCC;
            nhh.DiaChi = item.DiaChi;
            nhh.SDT = item.SDT;
            nhh.Web = item.Web;
            nhh.Status = true;
            nhacugcapDao.Update();
            message = "cập nhật!";
            return Json(data: new { message = "Thành công " + message, success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            var result = new nhacugcapDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            }); ;
        }

        [HttpGet]
        public JsonResult Delete(string id)
        {
            nhacugcapDao.Delete(id);
            return Json(data: new { message = "Xóa thành công ", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}