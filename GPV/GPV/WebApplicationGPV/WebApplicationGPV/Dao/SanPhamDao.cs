using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class SanPhamDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();

        public List<SanPham> GetSanPhams()
        {
            return db.SanPhams.ToList();
        }

        public SanPham SanPham(string id)
        {
            return db.SanPhams.Single(x => x.MaHH == id);
        }

        public void Insert(SanPham hh)
        {
            db.SanPhams.Add(hh);
            db.SaveChanges();
        }

        public void Update()
        {
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            var hh = db.SanPhams.Find(id);
            db.SanPhams.Remove(hh);
            db.SaveChanges();
        }

        public bool ChangeStatus(string id)
        {
            var hh = db.SanPhams.Find(id);
            hh.Status = !hh.Status;
            db.SaveChanges();
            return hh.Status;
        }
    }
}