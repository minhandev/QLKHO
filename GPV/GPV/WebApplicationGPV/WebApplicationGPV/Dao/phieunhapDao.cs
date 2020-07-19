using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class phieunhapDao
    {
        private dbQLKhoEntities db = new dbQLKhoEntities();
        public List<PhieuNhap> GetPhieuNhaps()
        {
            return db.PhieuNhaps.ToList();
        }
    }
}