using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class CTXuatKhoController : RoutingController
    {
        public ActionResult Index(int ID_PhieuXuat, string export_excel = null)
        {
            var phieuXuat = new dbQLKhoEntities().PhieuXuats.ToList().FirstOrDefault(q => q.ID_PhieuXuat == ID_PhieuXuat);
            ViewBag.phieuXuat = phieuXuat;
            var data = new dbQLKhoEntities().CT_XuatKho.ToList().Where(q => q.ID_PhieuXuat == ID_PhieuXuat).ToList();
            ViewBag.CTX = data;
            if (string.IsNullOrWhiteSpace(export_excel) == false && data != null && data.Count > 0)
            {
                XLWorkbook wb = new XLWorkbook(Server.MapPath(@"\Content\Export\ctxuatkho.xlsx"));
                var ws = wb.Worksheet(1);
                var index_row = 4;

                ws.Row(index_row++).Cell(3).Value = phieuXuat.ID_PhieuXuat;
                ws.Row(index_row++).Cell(3).Value = phieuXuat.ThoiGian;
                ws.Row(index_row++).Cell(3).Value = string.Format("{0}-{1}", phieuXuat.NhanVien.MaNV, phieuXuat.NhanVien.TenNV);

                index_row = 9;
                for (int i = 0; i < data.Count; i++)
                {
                    var item = data[i];
                    var index_col = 1;
                    ws.Row(index_row).Cell(index_col++).Value = i + 1;
                    ws.Row(index_row).Cell(index_col++).Value = item.MaHH;
                    ws.Row(index_row).Cell(index_col++).Value = item.SanPham.TenHH;
                    ws.Row(index_row).Cell(index_col++).Value = (item.SoLuong + "/" + item.SanPham.Dvt);
                    ws.Row(index_row++).Cell(index_col++).Value = item.Note;
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            return View(data);
        }

        public ActionResult Print(int ID_PhieuXuat)
        {
            var phieuXuat = new dbQLKhoEntities().PhieuXuats.ToList().FirstOrDefault(q => q.ID_PhieuXuat == ID_PhieuXuat);
            ViewBag.phieuXuat = phieuXuat;
            var data = new dbQLKhoEntities().CT_XuatKho.ToList().Where(q => q.ID_PhieuXuat == ID_PhieuXuat).ToList();
            ViewBag.CTX = data;
            return View(data);
        }
    }
}