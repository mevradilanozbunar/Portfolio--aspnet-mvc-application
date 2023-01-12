using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ExperienceController : Controller
	{
		private readonly ApplicationDbContext _db;
		public ExperienceController(ApplicationDbContext db)
		{
			_db = db;
		}

		[Authorize(Roles = "Admin")]
		[Route("Admin/PersonalInfo/Experience")]
		public IActionResult Index()
		{

			return View();
		}


		public IActionResult GetAll()
		{
			return Json(new { data = _db.Experiences.Where(p => p.IsActive == true).Where(p => p.IsDeleted == false).ToList<Experience>() }); ;
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Add(Experience _experience)
		{
            _db.Experiences.Add(_experience);
			_db.SaveChanges();
			return Json(_experience);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Delete(Guid id)
		{
			Experience FoundExperience = _db.Experiences.Find(id);
			_db.Experiences.Remove(FoundExperience);
			_db.SaveChanges();
			return Json(FoundExperience);
		}


		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult GetById(Guid id)
		{
			Experience FoundExperience = _db.Experiences.Find(id);
			return Json(FoundExperience);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Edit(Experience _experience)
		{
			Experience FoundExperience = _db.Experiences.Find(_experience.Id);

			if (FoundExperience.CompanyName != null)
				FoundExperience.CompanyName = _experience.CompanyName;
			if (FoundExperience.Location != null)
				FoundExperience.Location = _experience.Location;
			if (FoundExperience.Position != null)
				FoundExperience.Position = _experience.Position;
			if (FoundExperience.Description != null)
				FoundExperience.Description = _experience.Description;
			if (FoundExperience.StartDate != null)
				FoundExperience.StartDate = _experience.StartDate;
			if (FoundExperience.EndDate != null)
				FoundExperience.EndDate = _experience.EndDate;

			FoundExperience.DateModified = DateTime.Now;

			_db.Experiences.Update(FoundExperience);
			_db.SaveChanges();
			return Json(FoundExperience);
		}
	}
}
