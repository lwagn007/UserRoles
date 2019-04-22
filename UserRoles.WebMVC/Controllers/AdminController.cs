using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
using UserRoles.Services;

namespace UserRoles.WebMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var service = CreateAdminService();

            var userRoles = service.GetUsersRoles();

            ViewBag.UserRole = new SelectList(userRoles, "Email", "Name");

            if(service.IsUserAdmin())
                return View(userRoles);

            return RedirectToAction("Error");
        }

        public ActionResult UserIndex()
        {
            var service = CreateAdminService();

            if (!service.IsUserAdmin())
                return RedirectToAction("Index", "Home");

            var users = service.GetUsers();

            return View(users);
        }

        public ActionResult RoleIndex()
        {
            var service = CreateAdminService();

            if (!service.IsUserAdmin())
                return RedirectToAction("Index", "Home");

            var roles = service.GetRoles();

            return View(roles);
        }

        public ActionResult AssignUserRole()
        {
            var service = CreateAdminService();

            var users = service.GetUsers();

            ViewBag.UserId = new SelectList(users, "Id", "Email");

            var roles = service.GetRoles();

            ViewBag.Name = new SelectList(roles, "Name", "Name");

            if (!service.IsUserAdmin())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        
        //public ActionResult Edit(string userId)
        //{

        //    //return View(model);
        //} 

        [HttpPost]
        public ActionResult AssignUserRole(UserRole assignUserRole)
        {
            var service = CreateAdminService();

            if(service.AssignUserRole(assignUserRole))
            {
                TempData["SaveResult"] = "User assigned to role.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "User could not be assigned to role");
            return View();
        }

        private AdminService CreateAdminService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AdminService(userId);
            return service;
        }
    }
}