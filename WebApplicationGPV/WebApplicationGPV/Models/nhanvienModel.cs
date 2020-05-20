using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationGPV.Models
{
    public class nhanvienModel
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public string PB { get; set; }
        public string MatKhau { get; set; }
        public string MT { get; set; }
        public HttpPostedFileBase image { get; set; }
        public string taikhoan { get; set; }
        public bool Status { get; set; }
    }
}