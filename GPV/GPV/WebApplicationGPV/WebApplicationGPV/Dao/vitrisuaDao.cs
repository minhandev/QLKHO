using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class vitrisuaDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();

        public List<VitriSua> GetVitriSuas()
        {
            return db.VitriSuas.ToList();
        }

        public int Insert(VitriSua vt)
        {
            db.VitriSuas.Add(vt);
            db.SaveChanges();
            return 1;
        }

        public int Delete(int id)
        {
            var vt = db.VitriSuas.Find(id);
            db.VitriSuas.Remove(vt);
            db.SaveChanges();
            return 1;
        }
    }
}