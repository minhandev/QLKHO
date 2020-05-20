using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class hanghoaDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();

        public List<HangHoa> GetHangHoas()
        {
            return db.HangHoas.ToList();
        }

        public HangHoa HangHoa(string id)
        {
            return db.HangHoas.Single(x => x.MaHH == id);
        }

        public void Insert(HangHoa hh)
        {
            db.HangHoas.Add(hh);
            db.SaveChanges();
        }

        public void Update()
        {
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            var hh = db.HangHoas.Find(id);
            db.HangHoas.Remove(hh);
            db.SaveChanges();
        }

        public bool ChangeStatus(string id)
        {
            var hh = db.HangHoas.Find(id);
            hh.Status = !hh.Status;
            db.SaveChanges();
            return hh.Status;
        }
    }
}