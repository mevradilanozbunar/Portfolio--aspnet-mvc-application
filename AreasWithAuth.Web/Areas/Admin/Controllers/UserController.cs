using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly ApplicationDbContext _db;
		public UserController(ApplicationDbContext db)
		{
			_db = db;
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{

            return View(_db.AppUsers.Include(au => au.AppUserRole).ToList<AppUser>());
		}

		public IActionResult Login()
		{
			return View();
		}

		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}

		[HttpPost]
		public async Task<IActionResult> Login(AppUser appUser)
		{

			AppUser usr = _db.AppUsers.Include(u => u.AppUserRole).FirstOrDefault(u => u.UserName == appUser.UserName && u.Password == appUser.Password);
			if (usr != null)
			{
				List<Claim> userClaims = new List<Claim>();
				userClaims.Add(new Claim(ClaimTypes.Name, usr.UserName));
				userClaims.Add(new Claim(ClaimTypes.GivenName, usr.Name));
				userClaims.Add(new Claim(ClaimTypes.NameIdentifier, usr.Id.ToString()));

				userClaims.Add(new Claim(ClaimTypes.Role, usr.AppUserRole.Name));
				var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


				return RedirectToAction("Index", "Dashboard");

			}

			TempData["msg"] = "Username or Password is wrong...!";
			return View(appUser);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult GetAll()
		{
			return Json(new { data = _db.AppUsers.Include(au => au.AppUserRole).ToList() });
		}
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(AppUser appUser)
		{
			AppUser foundUser = _db.AppUsers.FirstOrDefault(au => au.Id == appUser.Id);
			foundUser.IsDeleted = true;
			foundUser.IsActive = false;
			_db.Update(foundUser);
			_db.SaveChanges();
			return Json(foundUser);
		}


		[Authorize(Roles = "Admin")]
		public IActionResult FindById(AppUser appUser)
		{
			return Json(_db.AppUsers.Include(au => au.AppUserRole).FirstOrDefault(au => au.Id == appUser.Id));
		}

		[Authorize(Roles = "Admin")]
		public IActionResult GetUserRoleNames()
		{
			return Json(_db.AppUserRoles.ToList());
		}


		[Authorize(Roles = "Admin")]
		public IActionResult Edit(AppUser appUser)
		{

			AppUser foundUser = _db.AppUsers.FirstOrDefault(a => a.Id == appUser.Id);


			foundUser.AppUserRoleId = appUser.AppUserRoleId;
			foundUser.UserName = appUser.UserName;
            foundUser.Password = appUser.Password;
            foundUser.Name = appUser.Name;
            foundUser.Surname = appUser.Surname;
            foundUser.Email = appUser.Email;
			foundUser.DateModified = DateTime.Now;
			foundUser.IsActive = appUser.IsActive;
			_db.Update(foundUser);
			_db.SaveChanges();


			return Json(foundUser);
		}

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(AppUser appUser)
        {
           
            _db.AppUsers.Add(appUser);
            _db.SaveChanges();
            return Json(appUser);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult userProfile()
        {

            return View(_db.AppUsers.Include(au => au.AppUserRole).ToList<AppUser>());
        }

    }
}
