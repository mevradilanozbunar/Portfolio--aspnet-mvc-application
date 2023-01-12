using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using System.Data;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using TinifyAPI;


using System.Linq;
using System.Drawing;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectPhotoController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ProjectPhotoController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
           return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddPhoto(ProjectPhoto projectPhoto)
        {      
         
                _db.ProjectPhotos.Add(projectPhoto);
                _db.SaveChanges();
                 return Json(projectPhoto);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.ProjectPhotos.Include(s => s.Project).ToList() });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetProjectsNames()
        {
            return Json(_db.Projects.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            ProjectPhoto projectPhoto= _db.ProjectPhotos.FirstOrDefault(au => au.Id == id);
            _db.Remove(projectPhoto);
            _db.SaveChanges();
            return Json(projectPhoto);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ProjectPhoto projectPhoto)
        {

            ProjectPhoto foundPhoto= _db.ProjectPhotos.FirstOrDefault(a => a.Id == projectPhoto.Id);


            foundPhoto.FileName = projectPhoto.FileName;
            foundPhoto.ProjectId = projectPhoto.ProjectId;

            _db.Update(foundPhoto);
            _db.SaveChanges();


            return Json(foundPhoto);
        }



        [Authorize(Roles = "Admin")]
        public IActionResult FindById(ProjectPhoto projectPhoto)
        {
            return Json(_db.ProjectPhotos.Include(au => au.Project).FirstOrDefault(au => au.Id == projectPhoto.Id));
        }

    }
}