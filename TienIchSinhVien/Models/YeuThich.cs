using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TienIchSinhVien.Models
{
    //public class yeuthichphongtro
    //{
    //    YeuThich YeuThich { get; set; }
    //    PhongTro PhongTro { get; set; }
    //}
    //public class yeuthichvieclam
    //{
    //    YeuThich YeuThich { get; set; }
    //    ViecLam ViecLam { get; set; }
    //}
    //public class yeuthichraovat
    //{
    //    YeuThich YeuThich { get; set; }
    //   RaoVat RaoVat { get; set; }
    //}
    public class tindayeuthich
    {
        YeuThich YeuThich { get; set; } 
        RaoVat RaoVat { get; set; }
        PhongTro PhongTro { get; set;}
        ViecLam ViecLam { get; set; }
    }
    [Table("YeuThich")]
    public partial class YeuThich
    {
        public int Id { get;set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public string NamePost { get; set; }
    }
}