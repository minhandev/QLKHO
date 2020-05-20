using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationGPV.Dao;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class NhaKhoController : Controller
    {
        // GET: NhaKho
        khoDao khoDao = new khoDao();
        // GET: NhomHangHoa
        public ActionResult Index()
        {
            return View(khoDao.GetNhaKhos());
        }

        [HttpPost]
        public ActionResult Index(NhaKho hh)
        {
            string message = string.Empty;
            if (hh.ID_NhaKho == 0)
            {
                var nhhs = new NhaKho()
                {
                    SoTang = hh.SoTang,
                    SoHang = hh.SoHang,
                    SoCot = hh.SoCot,
                    Status = hh.Status,
                };
                khoDao.Insert(nhhs);
                message = "Thêm xong";
            }
            else
            {
                var nhh = khoDao.NhaKho(hh.ID_NhaKho);
                nhh.SoTang = hh.SoTang;
                nhh.SoHang = hh.SoHang;
                nhh.SoCot = hh.SoCot;
                nhh.Status = hh.Status;
                khoDao.Update();
                message = "Cập nhật xong";
            }
            return Json(data: new { message = "Thành công " + message, success = true }, JsonRequestBehavior.AllowGet);
        }


        [ChildActionOnly]
        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Send(int id)
        {
            List<NhaKho> items = khoDao.GetNhaKhos();
            var nhh = new NhaKho();
            foreach (var item in items)
            {
                if (item.ID_NhaKho == id)
                {
                    nhh.ID_NhaKho = item.ID_NhaKho;
                    nhh.SoTang = item.SoTang;
                    nhh.SoHang = item.SoHang;
                    nhh.SoCot = item.SoCot;
                    nhh.Status = item.Status;
                }
            }
            return Json(nhh, JsonRequestBehavior.AllowGet);
        }

       
        [HttpGet]
        public JsonResult Delete(int id)
        {
            khoDao.Delete(id);
            return Json(data: new { message = "Xóa thành công ", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}