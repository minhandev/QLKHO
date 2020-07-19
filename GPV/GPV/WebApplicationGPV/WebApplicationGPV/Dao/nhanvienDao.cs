using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class nhanvienDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();

        public List<NhanVien> GetNhanViens()
        {
            return db.NhanViens.ToList();
        }

        public NhanVien GetNhanVien(int id)
        {
            return db.NhanViens.Single(x => x.MaNV == id);
        }

        public void Insert(NhanVien nv)
        {
            db.NhanViens.Add(nv);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var nv = db.NhanViens.Single(x => x.MaNV == id);
            db.NhanViens.Remove(nv);
            db.SaveChanges();
        }

        public void Edit()
        {
            db.SaveChanges();
        }

        public bool ChangeStatus(int id)
        {
            var nv = db.NhanViens.Single(x => x.MaNV == id);
            nv.Status = !nv.Status;
            db.SaveChanges();
            return nv.Status;
        }
    }
}