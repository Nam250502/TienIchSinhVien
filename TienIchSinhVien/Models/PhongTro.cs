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
        [Display(Name = "Tiêu Đề")]
        public string TieuDe { get; set; }

        [StringLength(255)]
        [Display(Name = "Ảnh")]
        public string AnhMinhHoa { get; set; }

        [StringLength(255)]
        [Display(Name = "Giá")]
        public string Gia { get; set; }

        [StringLength(255)]
        [Display(Name = "Địa Chỉ ")]
        public string DiaChi { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày Đăng")]
        public DateTime? NgayDang { get; set; }

        [StringLength(50)]
        [Display(Name = "Diện Tích")]
        public string DienTich { get; set; }
        [Display(Name = "Mô Tả")]
        public string MoTa { get; set; }
        
        public int? TrangThai { get; set; }
        public string PhoneNumber { get; set; }

        //public int? TinhTrang { get; set; }

    }
}
