using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TienIchSinhVien.Models
{
    public partial class YeuThich
    {
        public int Id { get;set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public string NamePost { get; set; }
    }
}