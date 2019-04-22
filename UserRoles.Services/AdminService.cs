using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UserRoles.Data;
using UserRoles.Models;
using UserRoles.Services.Contracts;

namespace UserRoles.Services
{
    public class AdminService : IAdminService
    {
        private readonly Guid _userId;

        public AdminService(Guid userId)
        {
            _userId = userId;
        }

        public bool IsUserAdmin()
        {
            using (var context = new ApplicationDbContext())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roles = userManager.GetRoles(_userId.ToString());
                if (roles.Count != 0 && roles[0].ToString() == "Admin")
                    return true;
                else
                    return false;
            }
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            using (var context = new ApplicationDbContext())
                return context.Users.ToArray();
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            using (var context = new ApplicationDbContext())
                return context.Roles.ToArray();
        }

        public IEnumerable<UserRoleListItem> GetUsersRoles()
        {
            using (var context = new ApplicationDbContext())
            {
                var users = (from u in context.Users
                             let query = (from ur in context.Set<IdentityUserRole>()
                                          where ur.UserId.Equals(u.Id)
                                          join r in context.Roles on ur.RoleId equals r.Id
                                          select r.Name)
                             select new UserRoleListItem { UserId = u.Id, UserName = u.UserName, Roles = query.ToList() }).ToArray();
                return users;
            }
        }

        public bool AssignUserRole(UserRole assignUserRole)
        {
            using (var context = new ApplicationDbContext())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var result = userManager.AddToRole(assignUserRole.UserId.ToString(), assignUserRole.Name);
                return true;
            }
        }
    }
}
