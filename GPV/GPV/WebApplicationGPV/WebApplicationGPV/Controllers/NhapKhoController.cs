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
    public class NhapKhoController : RoutingController
    {
        public ActionResult NhapKho(List<SanPham> lst_Sanpham)
        {
            var id_nvLogin = (nguoidungLogin)Session[Common.sessionSite.USER_SESSION];
            if (lst_Sanpham.Count > 0)
            {
                var db = new dbQLKhoEntities();
                var sanPhams = db.SanPhams.ToList();
                var phieuNhap = new PhieuNhap
                {
                    ID_NhanVien = id_nvLogin.MaNV,
                    Note = "",
                    ThoiGian = DateTime.Now,

                };
                db.PhieuNhaps.Add(phieuNhap);

                var r = db.SaveChanges() > 0;
                if (r)
                {
                    foreach (var item in lst_Sanpham)
                    {
                        var data = db.SanPhams.Find(item.MaHH);
                       if(data == null)
                        {
                            db.SanPhams.Add(new SanPham
                            {
                                MaHH = item.MaHH,
                                TenHH = item.TenHH,
                                NSX = item.NSX,
                                HSD = item.HSD,
                                Dvt = item.Dvt,
                                SoLuongTon = item.SoLuongTon,
                                DonGia = item.DonGia,
                                MaNCC = item.MaNCC,
                                ID_NhaKho = item.ID_NhaKho,
                                ID_PhieuNhap = phieuNhap.ID_PhieuNhap,
                                Cot = item.Cot,
                                Tang = item.Tang,
                                Hang = item.Hang,
                                Status = true,
                                ID_Nhom = item.ID_Nhom,
                            });
                        }
                       else if (data.MaHH == item.MaHH)
                        {
                                data.Status = true;
                                data.TenHH = item.TenHH;
                                data.NSX = item.NSX;
                                data.HSD = item.HSD;
                                data.Dvt = item.Dvt;
                                data.SoLuongTon = item.SoLuongTon;
                                data.DonGia = item.DonGia;
                                data.MaNCC = item.MaNCC;
                                data.ID_NhaKho = item.ID_NhaKho;
                                data.ID_PhieuNhap = phieuNhap.ID_PhieuNhap;
                                data.Cot = item.Cot;
                                data.Tang = item.Tang;
                                data.Hang = item.Hang;
                                data.ID_Nhom = item.ID_Nhom;
                        }
                    }
                    if (db.SaveChanges() > 0)
                    {
                        return RedirectToAction("Index", "CTNhapKho", new { ID_PhieuNhap = phieuNhap.ID_PhieuNhap });
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