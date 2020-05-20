using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationGPV.Common;
using WebApplicationGPV.Dao;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class NhanVienController : Controller
    {
        private nhanvienDao nhanvienDao = new nhanvienDao();
        private phongbanDao phongbanDao = new phongbanDao();
        [HttpGet]
        //[HasCredential(mavaitro = "View")]
        public ActionResult Index()
        {
            return View(nhanvienDao.GetNhanViens());
        }

        [HttpPost]
        public ActionResult Index(nhanvienModel nhansu)
        {
            string message = string.Empty;
            string imaUnique = string.Empty;
            string nameImg = string.Empty;
            if (nhansu.MaNV == 0)
            {
                imaUnique = Guid.NewGuid().ToString();
                nameImg = imaUnique + Path.GetExtension(nhansu.image.FileName);
                nhansu.image.SaveAs(Server.MapPath("~/upload/" + nameImg));

                NhanVien nv = new NhanVien()
                {
                    TenNV = nhansu.TenNV,
                    MT = nhansu.MT,
                    PB = nhansu.PB,
                    MatKhau = nhansu.MatKhau,
                    Status = true,
                    image = nameImg,
                    taikhoan = nhansu.taikhoan
                };
                nhanvienDao.Insert(nv);
                message = "Thêm xong";
            }
            else
            {
                var nv = nhanvienDao.GetNhanVien(nhansu.MaNV);

                if (nhansu.image != null)
                {
                    imaUnique = Guid.NewGuid().ToString();
                    nameImg = imaUnique + Path.GetExtension(nhansu.image.FileName);  //system.IO
                    nhansu.image.SaveAs(Server.MapPath("~/upload/" + nameImg));
                    nv.image = nameImg;
                }
                nv.TenNV = nhansu.TenNV;
                nv.MT = nhansu.MT;
                nv.PB = nhansu.PB;
                nv.taikhoan = nhansu.taikhoan;
                nv.MatKhau = nhansu.MatKhau;
                nv.Status = true;
                nhanvienDao.Edit();
                message = "Cập nhật xong";
            }
            return Json(data: new { message = "Thành công " + message, success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            nhanvienDao.Delete(id);
            return Json(data: new { message = "Xóa thành công ", success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Send(int id)
        {
            var nv = new NhanVien();
            List<NhanVien> nhanViens = nhanvienDao.GetNhanViens();
            foreach (var item in nhanViens)
            {
                if (item.MaNV == id)
                {
                    nv.MaNV = item.MaNV;
                    nv.TenNV = item.TenNV;
                    nv.PB = item.PB;
                    nv.MatKhau = item.MatKhau;
                    nv.image = item.image;
                    nv.MT = item.MT;
                    nv.taikhoan = item.taikhoan;
                }
            }
            return Json(nv, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new nhanvienDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            }); ;
        }

        [ChildActionOnly]
        public ActionResult Add()
        {
            ViewBag.ListPB = new SelectList(phongbanDao.GetPhongBans(), "PB", "TenBP");
            return PartialView();
        }
    }
}
