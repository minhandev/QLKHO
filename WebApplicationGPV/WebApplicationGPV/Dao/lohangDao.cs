using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class lohangDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();
        public List<LoHang> GetLoHangs()
        {
            return db.LoHangs.ToList();
        }
    }
}