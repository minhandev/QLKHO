using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplicationGPV.Common;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class XKhoController : RoutingController
    {
        // GET: XKho
        [HttpPost]
        public ActionResult Index(List<CT_XuatKho> lst_XuatKho)
        {
            var id_nvLogin = (nguoidungLogin)Session[Common.sessionSite.USER_SESSION];
            if (lst_XuatKho != null && lst_XuatKho.Count > 0)
            {
                var phieuXuat = new PhieuXuat
                {
                    ID_NhanVien = id_nvLogin.MaNV,
                    Note = "",
                    ThoiGian = DateTime.Now
                };
                var db = new dbQLKhoEntities();
                db.PhieuXuats.Add(phieuXuat);

                var lstHang = db.SanPhams.ToList().Where(q => q.Status != false).ToList();

                foreach (var item in lst_XuatKho)
                {
                    var ob = lstHang.FirstOrDefault(q => q.MaHH == item.MaHH);
                    if (ob != null)
                    {
                        db.CT_XuatKho.Add(new CT_XuatKho
                        {
                            ID_PhieuXuat = phieuXuat.ID_PhieuXuat,
                            MaHH = item.MaHH,
                            Note = item.Note,
                            SoLuong = item.SoLuong,
                            Tongcong = ob.DonGia * item.SoLuong,
                        });

                        ob.SoLuongTon -= item.SoLuong;
                        if (ob.SoLuongTon == 0)
                        {
                            ob.Status = false;
                        }
                    }
                }
                if (db.SaveChanges() > 0)
                {
                    return RedirectToAction("Index", "CTXuatKho", new { ID_PhieuXuat = phieuXuat.ID_PhieuXuat });
                }
                else
                {
                    ModelState.AddModelError("", "Xuất kho thất bại, tải lại trang web và thử lại.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Xuất kho thất bại, vui lòng chọn các lô hàng để xuất.");
            }
            return RedirectToAction("Index");
        }

    }
}