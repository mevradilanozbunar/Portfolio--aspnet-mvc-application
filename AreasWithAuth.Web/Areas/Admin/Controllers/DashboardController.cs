using AreasWithAuth.Data;
using AreasWithAuth.Models;
using AreasWithAuth.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DashboardController : Controller
	{
        private readonly ApplicationDbContext _db;
        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
            Allinfo DashBoard = new()
            {
             
                Profiles = _db.Profiles.ToList<Profile>(),
                ProjectCategories = _db.ProjectCategories.ToList<ProjectCategory>(),
                Projects = _db.Projects.ToList<Projects>(),
         
            };

			return View(DashBoard);
		}
    



       

    }
}
