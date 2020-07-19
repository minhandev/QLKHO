using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class khoDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();

        public List<NhaKho> GetNhaKhos()
        {
            return db.NhaKhoes.ToList();
        }

        public NhaKho NhaKho(int id)
        {
            return db.NhaKhoes.Single(x => x.ID_NhaKho == id);
        }

        public void Insert(NhaKho nk)
        {
            db.NhaKhoes.Add(nk);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var kh = db.NhaKhoes.Find(id);
            db.NhaKhoes.Remove(kh);
            db.SaveChanges();
        }

        public void Update()
        {
            db.SaveChanges();
        }
    }
}