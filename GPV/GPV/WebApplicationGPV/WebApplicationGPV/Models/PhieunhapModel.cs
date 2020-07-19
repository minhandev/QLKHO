using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationGPV.Models
{
    public class PhieunhapModel
    {
        public string TenHH { get; set; }
        public int ID_PhieuNhap { get; set; }
        public DateTime ThoiGian { get; set; }
        public int ID_NhanVien { get; set; }
        public string Note { get; set; }
    }
}