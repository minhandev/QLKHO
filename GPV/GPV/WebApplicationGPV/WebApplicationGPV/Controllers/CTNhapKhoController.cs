using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplicationGPV.Dao;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class CTNhapKhoController : RoutingController
    {
        SanPhamDao SanPhamDao = new SanPhamDao();
        phieunhapDao phieunhapDao = new phieunhapDao();
        public ActionResult Index(int ID_PhieuNhap, string export_excel = null)
        {
            var pn = phieunhapDao.GetPhieuNhaps().FirstOrDefault(q => q.ID_PhieuNhap == ID_PhieuNhap);
            ViewBag.phieuNhap = pn;
            var data = SanPhamDao.GetSanPhams().Where(q => q.ID_PhieuNhap == ID_PhieuNhap).ToList();
            if (string.IsNullOrWhiteSpace(export_excel) == false && data != null && data.Count > 0)
            {
                XLWorkbook wb = new XLWorkbook(Server.MapPath(@"\Content\Export\phieunhap.xlsx"));
                var ws = wb.Worksheet(1);
                var index_row = 5;

                ws.Row(index_row++).Cell(5).Value = pn.ID_PhieuNhap;
                ws.Row(index_row++).Cell(5).Value = pn.ThoiGian;
                ws.Row(index_row++).Cell(5).Value = string.Format("{0} - {1}", pn.NhanVien.MaNV, pn.NhanVien.TenNV);

                index_row = 10;
                for (int i = 0; i < data.Count; i++)
                {
                    var item = data[i];
                    var index_col = 1;
                    ws.Row(index_row).Cell(index_col++).Value = i + 1;
                    ws.Row(index_row).Cell(index_col++).Value = item.MaHH;
                    ws.Row(index_row).Cell(index_col++).Value = item.NSX;
                    ws.Row(index_row).Cell(index_col++).Value = item.HSD;
                    ws.Row(index_row).Cell(index_col++).Value = item.NhaCungCap.TenNCC;
                    ws.Row(index_row).Cell(index_col++).Value = string.Format("Tầng {0} - Hàng {1} - Cột {2}", item.Tang, item.Hang, item.Cot);
                    ws.Row(index_row).Cell(index_col++).Value = string.Format("{0:0,0}", item.DonGia);
                    ws.Row(index_row).Cell(index_col++).Value = item.SoLuongTon;
                    ws.Row(index_row).Cell(index_col++).Value = string.Format("{0:0,0}", item.DonGia * item.SoLuongTon);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            return View(data);
        }

        public ActionResult Print(int ID_PhieuNhap)
        {
            var pn = phieunhapDao.GetPhieuNhaps().FirstOrDefault(q => q.ID_PhieuNhap == ID_PhieuNhap);
            ViewBag.phieuNhap = pn;
            var data = SanPhamDao.GetSanPhams().Where(q => q.ID_PhieuNhap == ID_PhieuNhap).ToList();
            return View(data);
        }
    }
}