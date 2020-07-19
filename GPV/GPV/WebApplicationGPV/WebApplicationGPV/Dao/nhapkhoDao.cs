using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class nhapkhoDao
    {
        dbQLKhoEntities db = null;
        public nhapkhoDao()
        {
            db = new dbQLKhoEntities();
        }


    }
}