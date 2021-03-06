﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplicationGPV.Dao;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class PhongBanController : RoutingController
    {
        phongbanDao phongbanDao = new phongbanDao();
        // GET: NhomHangHoa
        public ActionResult Index()
        {
            return View(phongbanDao.GetPhongBans());
        }

        [HttpPost]
        public ActionResult Index(PhongBan nhh)
        {
            string message = string.Empty;
            var nhhs = new PhongBan()
            {
                PB = nhh.PB,
                TenBP = nhh.TenBP,
            };
            phongbanDao.Insert(nhhs);
            message = "Thêm xong";
            return Json(data: new { message = "Thành công " + message, success = true }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult Add()
        {
            return PartialView();
        }

        //[HttpGet]
        //public ActionResult Send(string id)
        //{
        //    List<NhomHangHoa> items = nhomhhDao.GetNhomHangHoas();
        //    var nhh = new NhomHangHoa();
        //    foreach (var item in items)
        //    {
        //        if (item.ID_NHH == id)
        //        {
        //            nhh.ID_NHH = item.ID_NHH;
        //            nhh.TenNHH = item.TenNHH;
        //        }
        //    }
        //    return Json(nhh, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult Edit(NhomHangHoa nhh)
        //{
        //    string message = string.Empty;
        //    var hh = nhomhhDao.NhomHangHoa(nhh.ID_NHH);
        //    hh.ID_NHH = nhh.ID_NHH;
        //    hh.TenNHH = nhh.TenNHH;
        //    hh.Status = true;
        //    nhomhhDao.Update();
        //    message = "cập nhật!";
        //    return Json(data: new { message = "Thành công " + message, success = true }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult ChangeStatusNH(string id)
        //{
        //    var result = new nhomhhDao().ChangeStatus(id);
        //    return Json(new
        //    {
        //        status = result
        //    }); ;
        //}

        //[HttpGet]
        //public JsonResult Delete(string id)
        //{
        //    nhomhhDao.Delete(id);
        //    return Json(data: new { message = "Xóa thành công ", success = true }, JsonRequestBehavior.AllowGet);
        //}
    }
}