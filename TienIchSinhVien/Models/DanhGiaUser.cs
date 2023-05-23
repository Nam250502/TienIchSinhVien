using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TienIchSinhVien.Models
{
    public class DanhGiaUser
    {
        [Key]
        public int Id { get; set; } 
        public string UserId1 { get; set; }
        public string UserId2 { get; set; }
        public string DanhGia { get; set; }
        public string Name { get; set; }
        public string Anh { get; set; }

    }
}