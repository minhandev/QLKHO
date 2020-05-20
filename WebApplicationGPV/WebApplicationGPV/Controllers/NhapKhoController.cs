using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationGPV.Common;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class NhapKhoController : Controller
    {
        // GET: NhapKho
        public ActionResult NhapKho(List<LoHang> lst_LoHang)
        {
            var id_nvLogin = (nguoidungLogin)Session[Common.sessionSite.USER_SESSION];
            if (lst_LoHang.Count > 0)
            {
                var db = new dbQLKhoEntities();
                var hanghoa = db.HangHoas.ToList();
                var phieuNhap = new PhieuNhap
                {
                    ID_NhanVien = id_nvLogin.MaNV,
                    Note = "",
                    ThoiGian = DateTime.Now
                };
                db.PhieuNhaps.Add(phieuNhap);

                var r = db.SaveChanges() > 0;
                if (r)
                {
                    foreach (var item in lst_LoHang)
                    {
                        var hh = hanghoa.FirstOrDefault(q => q.MaHH == item.MaHH);
                        hh.SoLuongTon += item.SoLuong;
                        db.LoHangs.Add(new LoHang
                        {
                            Cot = item.Cot,
                            Hang = item.Hang,
                            HSD = item.HSD,
                            ID_NhaKho = item.ID_NhaKho,
                            ID_PhieuNhap = phieuNhap.ID_PhieuNhap,
                            MaHH = item.MaHH,
                            MaNCC = item.MaNCC,
                            NSX = item.NSX,
                            SoLuong = item.SoLuong,
                            Status = 1,
                            Tang = item.Tang
                        });
                    }
                    if (db.SaveChanges() > 0)
                    {
                        return RedirectToAction("Index", "CTNhapKho", new { id = phieuNhap.ID_PhieuNhap });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Xuất kho thất bại, tải lại trang web và thử lại.");
                    }
                }
            }
            return RedirectToAction("Index", "Mohinh");
        }
    }
}