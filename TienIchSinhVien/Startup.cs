using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TienIchSinhVien.Startup))]
namespace TienIchSinhVien
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
