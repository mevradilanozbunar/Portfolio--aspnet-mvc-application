using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class LanguageController : Controller
	{
		private readonly ApplicationDbContext _db;
		public LanguageController(ApplicationDbContext db)
		{
			_db = db;
		}
		[Authorize(Roles = "Admin")]
		[Route("Admin/PersonalInfo/language")]
		public IActionResult Index()
		{

			return View();
		}


		public IActionResult GetAll()
		{
			return Json(new { data = _db.Languages.Where(p => p.IsActive == true).Where(p => p.IsDeleted == false).ToList<Language>() }); ;
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Add(Language _language)
		{
            _db.Languages.Add(_language);
			_db.SaveChanges();
			return Json(_language);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Delete(Guid id)
		{
			Language FoundLanguage = _db.Languages.Find(id);
			_db.Languages.Remove(FoundLanguage);
			_db.SaveChanges();
			return Json(FoundLanguage);
		}


		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult GetById(Guid id)
		{
			Language FoundLanguage = _db.Languages.Find(id);
			return Json(FoundLanguage);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Edit(Language _language)
		{
			Language FoundLanguage = _db.Languages.Find(_language.Id);

			if (FoundLanguage.name != null)
				FoundLanguage.name = _language.name;
			if (FoundLanguage.level != null)
				FoundLanguage.level = _language.level;


			FoundLanguage.DateModified = DateTime.Now;

			_db.Languages.Update(FoundLanguage);
			_db.SaveChanges();
			return Json(FoundLanguage);
		}
	}
}
