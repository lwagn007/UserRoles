using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoles.Data;
using UserRoles.Models;

namespace UserRoles.Services.Contracts
{
    public interface IAdminService
    {
        bool IsUserAdmin();
        IEnumerable<ApplicationUser> GetUsers();
        IEnumerable<IdentityRole> GetRoles();
        bool AssignUserRole(UserRole assignUserRole);
    }
}
