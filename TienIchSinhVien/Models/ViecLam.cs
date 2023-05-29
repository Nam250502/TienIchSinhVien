namespace TienIchSinhVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViecLam")]
    public partial class ViecLam
    {
        [Key]
        public int IdViecLam { get; set; }

        [StringLength(256)]
        public string IdUser { get; set; }

        [StringLength(128)]
        public string TieuDe { get; set; }

        [StringLength(255)]
        public string AnhMinhHoa { get; set; }

        [StringLength(255)]
        public string Luong { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDang { get; set; }

        [StringLength(50)]
        public string ViTriUngTuyen { get; set; }

        public string MoTa { get; set; }

        public int? TrangThai { get; set; }

        public string PhoneNumber { get; set; }
        public bool isShowYeuThich = false;
    }
}
