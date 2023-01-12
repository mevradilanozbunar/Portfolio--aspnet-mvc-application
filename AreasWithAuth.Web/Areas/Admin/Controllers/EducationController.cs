using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class EducationController : Controller
	{
		private readonly ApplicationDbContext _db;
		public EducationController(ApplicationDbContext db)
		{
			_db = db;
		}

		[Authorize(Roles = "Admin")]
		[Route("Admin/PersonalInfo/Education")]
		public IActionResult Index()
		{

			return View();
		}


		public IActionResult GetAll()
		{
			return Json(new { data = _db.Educations.Where(p => p.IsActive == true).Where(p => p.IsDeleted == false).ToList<Education>() }); ;
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Add(Education _education)
		{
            _db.Educations.Add(_education);
			_db.SaveChanges();
			return Json(_education);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Delete(Guid id)
		{
			Education FoundEducation = _db.Educations.Find(id);
			_db.Educations.Remove(FoundEducation);
			_db.SaveChanges();
			return Json(FoundEducation);
		}


		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult GetById(Guid id)
		{
			Education edu = _db.Educations.Find(id);
			return Json(edu);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Edit(Education _education)
		{
			Education FoundEducation = _db.Educations.Find(_education.Id);

			if (FoundEducation.UniversityName != null)
				FoundEducation.UniversityName = _education.UniversityName;
			if (FoundEducation.Location != null)
				FoundEducation.Location = _education.Location;
			if (FoundEducation.Department != null)
				FoundEducation.Department = _education.Department;
			if (FoundEducation.Description != null)
				FoundEducation.Description = _education.Description;
			if (FoundEducation.StartDate != null)
				FoundEducation.StartDate = _education.StartDate;
			if (FoundEducation.EndDate != null)
				FoundEducation.EndDate = _education.EndDate;

			FoundEducation.DateModified = DateTime.Now;

			_db.Educations.Update(FoundEducation);
			_db.SaveChanges();
			return Json(FoundEducation);
		}


	}
}
