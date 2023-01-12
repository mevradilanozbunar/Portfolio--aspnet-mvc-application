using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProjectController : Controller
	{
		private readonly ApplicationDbContext _db;
		public ProjectController(ApplicationDbContext db)
		{
			_db = db;
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{

			return View(_db.ProjectCategories.ToList<ProjectCategory>());
		}


		public IActionResult GetAll()
		{
			return Json(new { data = _db.Projects.ToList() });
		}
   
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Add(Projects _project)
		{			
			_db.Projects.Add(_project);
			_db.SaveChanges();
			return Json(_project);
			
		}


		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult CounterUpPost(Projects _project)
		{			
			var category = _db.ProjectCategories.Where(m => m.Id == _project.ProjectCategoryId).SingleOrDefault();
			category.ProjectCount += 1;
			_db.SaveChanges();
			return Json(_project);
		}



     



        [Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Delete(Guid id)
		{
			Projects FoundProject = _db.Projects.Find(id);
			_db.Projects.Remove(FoundProject);
			_db.SaveChanges();
			return Json(FoundProject);
		}


		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult GetById(Guid id)
		{
			Projects FoundProject = _db.Projects.Find(id);
			return Json(FoundProject);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Edit(Projects _project)
		{
			Projects FoundProject = _db.Projects.Find(_project.Id);

			if (FoundProject.Name != null)
				FoundProject.Name = _project.Name;
			if (FoundProject.Baslik != null)
				FoundProject.Baslik = _project.Baslik;
			if (FoundProject.Icerik != null)
				FoundProject.Icerik = _project.Icerik;
			if (FoundProject.GithubLink != null)
				FoundProject.GithubLink = _project.GithubLink;
			if (FoundProject.ProgramlamaDilleri != null)
				FoundProject.ProgramlamaDilleri = _project.ProgramlamaDilleri;
			if (FoundProject.Teknolojiler != null)
				FoundProject.Teknolojiler = _project.Teknolojiler;
			if (FoundProject.ProjectCategoryId != null)
				FoundProject.ProjectCategoryId = _project.ProjectCategoryId;
			if (FoundProject.Foto != null)
				FoundProject.Foto= _project.Foto;

			FoundProject.DateModified = DateTime.Now;

			_db.Projects.Update(FoundProject);
			_db.SaveChanges();
			return Json(FoundProject);
		}
	}
}
