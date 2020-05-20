using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class phongbanDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();

        public List<PhongBan> GetPhongBans()
        {
            return db.PhongBans.ToList();
        }
    }
}