using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationGPV.Common;
using WebApplicationGPV.Dao;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class XuatKhoController : Controller
    {
        // GET: XuatKho
        private khoDao khoDao = new khoDao();
        private vitrisuaDao vitrisuaDao = new vitrisuaDao();
        private nhacugcapDao nhacugcapDao = new nhacugcapDao();
        private hanghoaDao hanghoaDao = new hanghoaDao();
        private lohangDao lohangDao = new lohangDao();
        private phieuxuatDao phieuxuatDao = new phieuxuatDao();
        [HttpPost]
        public ActionResult XuatKho(List<CT_XuatKho> lst_XuatKho)
        {
            var session = (nguoidungLogin)Session[Common.sessionSite.USER_SESSION];
            if (lst_XuatKho != null && lst_XuatKho.Count > 0)
            {
                var phieuXuat = new PhieuXuat
                {
                    ID_NhanVien = session.MaNV,
                    Note = "",
                    ThoiGian = DateTime.Now
                };
                var db = new dbQLKhoEntities();
                db.PhieuXuats.Add(phieuXuat);

                var lstHang = db.LoHangs.ToList().Where(q => q.Status != 0).ToList();
                var lstHangHoa = db.HangHoas.ToList();
                foreach (var item in lst_XuatKho)
                {
                    var ob = lstHang.FirstOrDefault(q => q.MaLoHang == item.MaLoHang);
                    if (ob != null)
                    {
                        db.CT_XuatKho.Add(new CT_XuatKho
                        {
                            ID_PhieuXuat = phieuXuat.ID_PhieuXuat,
                            MaLoHang = item.MaLoHang,
                            Note = item.Note,
                            SoLuong = item.SoLuong
                        });

                        ob.SoLuong -= item.SoLuong;
                        if (ob.SoLuong <= 0)
                        {
                            ob.Status = 0;
                        }
                        var hh = lstHangHoa.FirstOrDefault(q => q.MaHH == ob.MaHH);
                        if (hh != null)
                        {
                            hh.SoLuongTon -= item.SoLuong;
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