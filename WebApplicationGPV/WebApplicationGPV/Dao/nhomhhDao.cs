using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class nhomhhDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();

        public List<NhomHangHoa> GetNhomHangHoas()
        {
            return db.NhomHangHoas.ToList();
        }

        public NhomHangHoa NhomHangHoa(string id)
        {
            return db.NhomHangHoas.Single(x => x.ID_NHH == id);
        }

        public void Insert(NhomHangHoa nhh)
        {
            db.NhomHangHoas.Add(nhh);
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            var nhh = db.NhomHangHoas.Single(x => x.ID_NHH == id);
            db.NhomHangHoas.Remove(nhh);
            db.SaveChanges();
        }

        public void Update()
        {
            db.SaveChanges();
        }

        public bool ChangeStatus(string id)
        {
            var nhh = db.NhomHangHoas.Find(id);
            nhh.Status = !nhh.Status;
            db.SaveChanges();
            return nhh.Status;
        }
    }
}