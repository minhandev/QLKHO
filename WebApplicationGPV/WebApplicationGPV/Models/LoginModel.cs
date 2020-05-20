using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationGPV.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Mời nhập mã nhân viên.")]
        public string taikhoan { get; set; }

        [Required(ErrorMessage = "Mời nhập mật khẩu.")]
        public string MatKhau { get; set; }

    }
}