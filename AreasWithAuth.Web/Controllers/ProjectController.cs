using AreasWithAuth.Data;
using AreasWithAuth.Models.ViewModels;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreasWithAuth.Web.Controllers
{

    public class ProjectController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ProjectController(ApplicationDbContext db)
        {

            _db = db;
        }

        public IActionResult Index()
        {

            Allinfo project = new()
            {
                
                Profiles = _db.Profiles.ToList<Profile>(),
                ProjectCategories = _db.ProjectCategories.Include(a=>a.Projects).ToList<ProjectCategory>(),
                Projects = _db.Projects.ToList<Projects>(),
				topViewsProjects = _db.Projects.OrderByDescending(m => m.ViewsCount).Take(10).ToList<Projects>(),
				RecentsProjects = _db.Projects.OrderByDescending(m => m.DateCreated).Take(10).ToList<Projects>(),

			};

            return View(project);
        }

        [Route("Projects/{id}")]
        public IActionResult Project(Guid id)
        {
			var post = _db.Projects.Where(m => m.Id == id).SingleOrDefault();
			post.ViewsCount += 1;
			_db.SaveChanges();

			ProjectModel project = new()
			{
				projectPhotos = _db.ProjectPhotos.Where(m => m.ProjectId == id).ToList<ProjectPhoto>(),
				projects = _db.Projects.Where(m => m.Id == id).SingleOrDefault()
			};
	
            return View(project);
        }

		[Route("ProjectsCategory/{id}")]
		public IActionResult ProjectCategory(Guid id)
		{
			Allinfo project = new()
			{

				Profiles = _db.Profiles.ToList<Profile>(),
				ProjectCategories = _db.ProjectCategories.Where(m => m.Id == id).ToList<ProjectCategory>(),
				Projects = _db.Projects.Where(m => m.ProjectCategoryId == id).ToList<Projects>(),
				topViewsProjects = _db.Projects.OrderByDescending(m => m.ViewsCount).Take(10).ToList<Projects>(),
				RecentsProjects = _db.Projects.OrderByDescending(m => m.DateCreated).Take(10).ToList<Projects>(),

			};



			return View(project);
		}

		public ActionResult Search(string SearchProject)
		{

			if (string.IsNullOrEmpty(SearchProject))
			{
				Allinfo project = new()
				{

					Profiles = _db.Profiles.ToList<Profile>(),
					ProjectCategories = _db.ProjectCategories.ToList<ProjectCategory>(),
					Projects = _db.Projects.ToList<Projects>(),
					topViewsProjects = _db.Projects.OrderByDescending(m => m.ViewsCount).Take(10).ToList<Projects>(),
					RecentsProjects = _db.Projects.OrderByDescending(m => m.DateCreated).Take(10).ToList<Projects>(),

				};
				return View("Index", project);
			}
			else
			{
				Allinfo project = new()
				{

					Profiles = _db.Profiles.ToList<Profile>(),
					ProjectCategories = _db.ProjectCategories.ToList<ProjectCategory>(),
					Projects = _db.Projects.Where(m => m.Name.Contains(SearchProject)).OrderByDescending(m => m.DateCreated).ToList<Projects>(),
					topViewsProjects = _db.Projects.OrderByDescending(m => m.ViewsCount).Take(10).ToList<Projects>(),
					RecentsProjects = _db.Projects.OrderByDescending(m => m.DateCreated).Take(10).ToList<Projects>(),

				};
			
				
					return View("Index", project);
				}
			}



		



	}
}
