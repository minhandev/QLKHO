using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationGPV.Common;
using WebApplicationGPV.Models;

namespace WebApplicationGPV.Dao
{
    public class LoginDao
    {
        dbQLKhoEntities db = new dbQLKhoEntities();
        #region LOGIN NHÂN VIÊN
        public int Login(string taikhoan, string MatKhau, bool IsLoginAdmin = false)
        {
            var result = db.NhanViens.SingleOrDefault(x => x.taikhoan == taikhoan);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (IsLoginAdmin == true)
                {
                    if (result.PB == sessionLogin.ADMIN_GROUP || result.PB == sessionLogin.MEMBER_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.MatKhau == MatKhau)
                            {
                                return 1;
                            }
                            else return 2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.MatKhau == MatKhau)
                        {
                            return 1;
                        }
                        else return 2;
                    }
                }
            }
        }

        public List<string> Getcapquyen(int MaNV)
        {
            var user = db.NhanViens.Single(x => x.MaNV == MaNV);
            var data = (from a in db.capquyens
                        join b in db.PhongBans on a.PB equals b.PB
                        join c in db.VaiTroes on a.mavaitro equals c.mavaitro
                        where b.PB == user.PB
                        select new
                        {
                            mavaitro = a.mavaitro,
                            PB = a.PB
                        }).AsEnumerable().Select(x => new capquyen()
                        {
                            mavaitro = x.mavaitro,
                            PB = x.PB
                        });
            return data.Select(x => x.mavaitro).ToList();
        }

        public NhanVien NhanVien(string taikhoan)
        {
            return db.NhanViens.SingleOrDefault(x => x.taikhoan == taikhoan);
        }
        #endregion
    }
}