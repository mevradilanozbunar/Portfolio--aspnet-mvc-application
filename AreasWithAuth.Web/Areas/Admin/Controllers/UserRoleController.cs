using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserRoleController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserRoleController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {

            return View(_db.AppUserRoles.ToList<AppUserRole>());
        }
      

        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.AppUserRoles.ToList() });
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(AppUserRole appUserRole)
        {
            AppUserRole foundUserRole = _db.AppUserRoles.FirstOrDefault(au => au.Id == appUserRole.Id);
            foundUserRole.IsDeleted = true;
            foundUserRole.IsActive = false;
            _db.Update(foundUserRole);
            _db.SaveChanges();
            return Json(foundUserRole);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult FindById(AppUserRole appUserRole)
        {
            return Json(_db.AppUserRoles.FirstOrDefault(au => au.Id == appUserRole.Id));
        }



        [Authorize(Roles = "Admin")]
        public IActionResult Edit(AppUserRole appUserRole)
        {

            AppUserRole foundUserRole = _db.AppUserRoles.FirstOrDefault(au => au.Id == appUserRole.Id);

            foundUserRole.Name=appUserRole.Name;
           
            _db.Update(foundUserRole);
            _db.SaveChanges();


            return Json(foundUserRole);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(AppUserRole appUserRole)
        {

            _db.AppUserRoles.Add(appUserRole);
            _db.SaveChanges();
            return Json(appUserRole);

        }



    }
}
