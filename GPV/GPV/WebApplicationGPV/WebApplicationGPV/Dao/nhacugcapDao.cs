using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class nhacugcapDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();

        public List<NhaCungCap> GetNhacugcaps()
        {
            return db.NhaCungCaps.ToList();
        }

        public NhaCungCap nhaCungCap(string id)
        {
            return db.NhaCungCaps.Single(x => x.MaNCC == id);
        }

        public void Insert(NhaCungCap ncc)
        {
            db.NhaCungCaps.Add(ncc);
            db.SaveChanges();
        }

        public void Update()
        {
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            var ncc = db.NhaCungCaps.Find(id);
            db.NhaCungCaps.Remove(ncc);
            db.SaveChanges();
        }

        public bool ChangeStatus(string id)
        {
            var ncc = db.NhaCungCaps.Find(id);
            ncc.Status = !ncc.Status;
            db.SaveChanges();
            return ncc.Status;
        }
    }
}