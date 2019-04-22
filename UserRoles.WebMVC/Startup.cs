using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using UserRoles.Data;

[assembly: OwinStartupAttribute(typeof(UserRoles.WebMVC.Startup))]
namespace UserRoles.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndAdminUser();
        }

        private void CreateRolesAndAdminUser()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);

                    var user = new ApplicationUser();
                    user.UserName = "admin";
                    user.Email = "admin@admin.com";

                    string password = "Test1!";
                    var checkUser = userManager.Create(user, password);

                    if (checkUser.Succeeded)
                    {
                        var result = userManager.AddToRole(user.Id, "Admin");
                    }
                }

                if (!roleManager.RoleExists("Super-User"))
                {
                    var role = new IdentityRole();
                    role.Name = "Super-User";
                    roleManager.Create(role);
                }

                if (!roleManager.RoleExists("User"))
                {
                    var role = new IdentityRole();
                    role.Name = "User";
                    roleManager.Create(role);
                }
            }
        }
    }
}
