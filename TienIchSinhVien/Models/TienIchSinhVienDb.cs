using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TienIchSinhVien.Models
{
    public partial class TienIchSinhVienDb : DbContext
    {
        public TienIchSinhVienDb()
            : base("name=TienIchSinhVienDb")
        {
        }

        public virtual DbSet<PhongTro> PhongTro { get; set; }
        public virtual DbSet<ViecLam> ViecLam { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ViecLam>()
                .Property(e => e.ViTriUngTuyen)
                .IsFixedLength();

           
        }
    }
}
