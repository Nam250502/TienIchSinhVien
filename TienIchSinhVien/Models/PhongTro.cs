namespace TienIchSinhVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhongTro")]
    public partial class PhongTro
    {
        [Key]
        public int IdPhongTro { get; set; }

        [StringLength(256)]
        public string IdUser { get; set; }

        [StringLength(128)]
        public string TieuDe { get; set; }

        [StringLength(255)]
        public string AnhMinhHoa { get; set; }

        [StringLength(255)]
        public string Gia { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDang { get; set; }

        [StringLength(50)]
        public string DienTich { get; set; }

        public string MoTa { get; set; }

        public int? TrangThai { get; set; }

        public string PhoneNumber { get; set; }
        public bool isShowYeuThich = false;
    }
}
