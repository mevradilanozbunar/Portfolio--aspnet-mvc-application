using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProjectCategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
		public ProjectCategoryController(ApplicationDbContext db)
		{
			_db = db;
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{

			return View();
		}


		public IActionResult GetAll()
		{

			return Json(new { data = _db.ProjectCategories.ToList<ProjectCategory>() });

		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Add(ProjectCategory _projectCategory)
		{
            _db.ProjectCategories.Add(_projectCategory);
			_db.SaveChanges();
			return Json(_projectCategory);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Delete(Guid id)
		{
			ProjectCategory FoundProjectCategory = _db.ProjectCategories.Find(id);
			_db.ProjectCategories.Remove(FoundProjectCategory);
			_db.SaveChanges();
			return Json(FoundProjectCategory);
		}


		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult GetById(Guid id)
		{
			ProjectCategory FoundProjectCategory = _db.ProjectCategories.Find(id);
			return Json(FoundProjectCategory);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Edit(ProjectCategory _projectCategory)
		{
			ProjectCategory FoundProjectCategory = _db.ProjectCategories.Find(_projectCategory.Id);

			if (FoundProjectCategory.Name != null)
				FoundProjectCategory.Name = _projectCategory.Name;


			FoundProjectCategory.DateModified = DateTime.Now;

			_db.ProjectCategories.Update(FoundProjectCategory);
			_db.SaveChanges();
			return Json(FoundProjectCategory);
		}
	}
}

