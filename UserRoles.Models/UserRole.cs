using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoles.Data;

namespace UserRoles.Models
{
    public class UserRole
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
