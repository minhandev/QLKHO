using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class NhomspDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();

        public List<NhomSanPham> GetNhomSanPhams()
        {
            return db.NhomSanPhams.ToList();
        }

        public NhomSanPham NhomSanPham(int id)
        {
            return db.NhomSanPhams.Single(x => x.ID_Nhom == id);
        }

        public void Insert(NhomSanPham nsp)
        {
            db.NhomSanPhams.Add(nsp);
            db.SaveChanges();
        }

        public void Update()
        {
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var nsp = db.NhomSanPhams.Find(id);
            db.NhomSanPhams.Remove(nsp);
            db.SaveChanges();
        }

        public bool ChangeStatus(int id)
        {
            var nsp = db.NhomSanPhams.Find(id);
            nsp.Status = !nsp.Status;
            db.SaveChanges();
            return nsp.Status;
        }
    }
}