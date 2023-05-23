using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TienIchSinhVien.Models
{
    public class Detailphongtroviewmodel
    {
   
            public PhongTro PhongTro { get; set; }
            public string Name { get; set; }
            public string Anh { get; set; }
             public string UserId { get; set; }
    }
    public class Detailraovatviewmodel
    {
        public RaoVat RaoVat { get; set; }
        public string Name { get; set; }
        public string Anh { get; set; }
        public string UserId { get; set; }
    }
    public class Detailvieclamviewmodel
    {
        public ViecLam ViecLam { get; set; }
        public string Name { get; set; }
        public string Anh { get; set; }
        public string UserId { get; set; }
    }
    public  class loadDanhGia
    {
        public DanhGiaUser danhGiaUser { get; set; }
        public ProfileViewModel ProfileViewModel { get; set; }
    }
   
   
}  