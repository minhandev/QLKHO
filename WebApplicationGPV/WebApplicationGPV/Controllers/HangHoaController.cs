using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationGPV.Dao;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class HangHoaController : Controller
    {
        hanghoaDao hanghoaDao = new hanghoaDao();
        // GET: NhomHangHoa
        public ActionResult Index()
        {
            return View(hanghoaDao.GetHangHoas());
        }

        [HttpPost]
        public ActionResult Index(HangHoa hh)
        {
            string message = string.Empty;
            var nhhs = new HangHoa()
            {
                MaHH = hh.MaHH,
                TenHH = hh.TenHH,
                MoTa = hh.MoTa,
                ID_NHH = hh.ID_NHH,
                SoLuongTon = hh.SoLuongTon,
                Status = true
            };
            hanghoaDao.Insert(nhhs);
            message = "Thêm xong";
            return Json(data: new { message = "Thành công " + message, success = true }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult Add()
        {
            nhomhhDao nhomhhDao = new nhomhhDao();
            ViewBag.ListNHH = new SelectList(nhomhhDao.GetNhomHangHoas().Where(x => x.Status == true), "ID_NHH", "TenNHH");
            return PartialView();
        }

        [HttpGet]
        public ActionResult Send(string id)
        {
            List<HangHoa> items = hanghoaDao.GetHangHoas();
            var nhh = new HangHoa();
            foreach (var item in items)
            {
                if (item.MaHH == id)
                {
                    nhh.MaHH = item.MaHH;
                    nhh.TenHH = item.TenHH;
                    nhh.MoTa = item.MoTa;
                    nhh.ID_NHH = item.ID_NHH;
                    nhh.SoLuongTon = item.SoLuongTon;
                }
            }
            return Json(nhh, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(HangHoa item)
        {
            string message = string.Empty;
            var nhh = hanghoaDao.HangHoa(item.MaHH);
            nhh.MaHH = item.MaHH;
            nhh.TenHH = item.TenHH;
            nhh.MoTa = item.MoTa;
            nhh.ID_NHH = item.ID_NHH;
            nhh.SoLuongTon = item.SoLuongTon;
            nhh.Status = true;
            hanghoaDao.Update();
            message = "cập nhật!";
            return Json(data: new { message = "Thành công " + message, success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeStatusHH(string id)
        {
            var result = new hanghoaDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            }); ;
        }

        [HttpGet]
        public JsonResult Delete(string id)
        {
            hanghoaDao.Delete(id);
            return Json(data: new { message = "Xóa thành công ", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}