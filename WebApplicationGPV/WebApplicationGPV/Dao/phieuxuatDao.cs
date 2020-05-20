using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class phieuxuatDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();

        public List<PhieuXuat> GetPhieuXuats()
        {
            return db.PhieuXuats.ToList();
        }
    }
}