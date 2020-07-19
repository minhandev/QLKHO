using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplicationGPV.Dao;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Controllers
{
    public class SanPhamController : RoutingController
    {
        dbQLKhoEntities db = new dbQLKhoEntities();
        SanPhamDao SanPhamDao = new SanPhamDao();
        public ActionResult Index()
        {
            return View(SanPhamDao.GetSanPhams());
        }

        public ActionResult Reports(string type)
        {
            var query = from a in db.SanPhams
                        join b in db.NhaCungCaps on a.MaNCC equals b.MaNCC
                        join c in db.NhomSanPhams on a.ID_Nhom equals c.ID_Nhom
                        from d in db.NhaKhoes.Where(x => a.ID_NhaKho == x.ID_NhaKho).DefaultIfEmpty()
                        select new
                        {
                            TenHH = a.TenHH,
                            MaHH = a.MaHH,
                            TenNhom = c.TenNhom,
                            Dvt = a.Dvt,
                            SoLuongTon = a.SoLuongTon,
                            DonGia = a.DonGia,
                            NSX = a.NSX,
                            HSD = a.HSD,
                            TenNCC = b.TenNCC,
                            Tang = a.Tang,
                            Status3 = a.Status ? "SP Tồn kho" : "SP Hết hàng",
                        };

            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/ReportSanPham.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSetSanPham";
            reportDataSource.Value = query.ToList();
            localReport.DataSources.Add(reportDataSource);
            string rptype = type;
            string gettype;
            string encoding;
            string fileNameExtention;
            if (rptype == "Excel")
            {
                fileNameExtention = "xlsx";
            }
            else if (rptype == "word")
            {
                fileNameExtention = "docx";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render(rptype, "", out gettype, out encoding, out fileNameExtention, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename = DSSanPham." + fileNameExtention);
            return File(renderedByte, fileNameExtention);
        }
    }
}