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

        public PhongBan PhongBan(string id)
        {
            return db.PhongBans.Single(x => x.PB == id);
        }

        public void Insert(PhongBan pb)
        {
            db.PhongBans.Add(pb);
            db.SaveChanges();
        }
    }
}