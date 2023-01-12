using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using PagedList;
using AreasWithAuth.Models.ViewModels;

namespace AreasWithAuth.Web.Controllers
{
    public class HomeController : Controller
	{

		private readonly ApplicationDbContext _db;

		public HomeController(ApplicationDbContext db)
		{

			_db = db;
		}

		public IActionResult Index()
		{

			Allinfo portfolio = new()
			{
				Educations = _db.Educations.OrderByDescending(m => m.StartDate).ToList<Education>(),
				Experiences = _db.Experiences.OrderByDescending(m => m.StartDate).ToList<Experience>(),
				Skills = _db.Skills.ToList<Skills>(),
				Languages = _db.Languages.ToList<Language>(),
				Profiles = _db.Profiles.ToList<Profile>(),
				ProjectCategories = _db.ProjectCategories.ToList<ProjectCategory>(),
				Projects = _db.Projects.ToList<Projects>(),
				LastExperience= _db.Experiences.OrderByDescending(m => m.DateCreated).Take(1).ToList<Experience>()
			};

			return View(portfolio);
		}

		public IActionResult Privacy()
		{
			return View();
		}
	

	}
}