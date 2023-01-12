using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SkillsController : Controller
	{
		private readonly ApplicationDbContext _db;
		public SkillsController(ApplicationDbContext db)
		{
			_db = db;
		}

		[Authorize(Roles = "Admin")]
		[Route("Admin/PersonalInfo/skills")]
		public IActionResult Index()
		{

			return View();
		}


		public IActionResult GetAll()
		{
			return Json(new { data = _db.Skills.Where(p => p.IsActive == true).Where(p => p.IsDeleted == false).ToList<Skills>() }); ;
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Add(Skills _skills)
		{
            _db.Skills.Add(_skills);
			_db.SaveChanges();
			return Json(_skills);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Delete(Guid id)
		{
			Skills FoundSkill = _db.Skills.Find(id);
			_db.Skills.Remove(FoundSkill);
			_db.SaveChanges();
			return Json(FoundSkill);
		}


		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult GetById(Guid id)
		{
			Skills FoundSkill = _db.Skills.Find(id);
			return Json(FoundSkill);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Edit(Skills _skills)
		{
			Skills FoundSkill = _db.Skills.Find(_skills.Id);

			if (FoundSkill.name != null)
				FoundSkill.name = _skills.name;
			if (FoundSkill.percentDegree != null)
				FoundSkill.percentDegree = _skills.percentDegree;


			FoundSkill.DateModified = DateTime.Now;

			_db.Skills.Update(FoundSkill);
			_db.SaveChanges();
			return Json(FoundSkill);
		}
	}
}
