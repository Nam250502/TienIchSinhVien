using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using TienIchSinhVien.Models;

[assembly: OwinStartupAttribute(typeof(TienIchSinhVien.Startup))]
namespace TienIchSinhVien
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndAdminUser();


        }
        public void CreateRolesAndAdminUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            if (!roleManager.RoleExists("Admin"))
            {
                // Tạo vai trò Admin
                var adminRole = new IdentityRole("Admin");
                roleManager.Create(adminRole);

            }

            if (!roleManager.RoleExists("User"))
            {
                // Tạo vai trò User
                var userRole = new IdentityRole("User");
                roleManager.Create(userRole);

               
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            // Thêm người dùng vào vai trò Admin
            if (userManager.FindByEmail("admin@gmail.com")==null)
            {
                var adminUser = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
                userManager.Create(adminUser, "123456Ip@");
                userManager.AddToRole(adminUser.Id, "Admin");
            }
            
        }

    }
}
