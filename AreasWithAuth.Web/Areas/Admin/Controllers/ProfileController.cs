using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using System.Data;
using System.Drawing;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProfileController : Controller
	{
        private IWebHostEnvironment _env;
        private readonly ApplicationDbContext _db;
        public ProfileController(IConfiguration configuration, IWebHostEnvironment env, ApplicationDbContext db)
        {
            _env = env;
            _db = db;
        }

        [Authorize(Roles = "Admin")]
		[Route("Admin/PersonalInfo/Profile")]
		public IActionResult Index()
		{

			return View();
		}


		public IActionResult GetAll()
		{
			return Json(new { data = _db.Profiles.Where(p => p.IsActive == true).Where(p => p.IsDeleted == false).ToList<Profile>() }); ;
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Add(Profile profil)
		{
            if (profil != null)
            {
                string filename = "";
                if (profil.ProfilePicture.Contains("png"))
                {
                    filename = Base64ToImageSave(profil.ProfilePicture, profil.Id + ".png");
                }
                else if (profil.ProfilePicture.Contains("jpg") || profil.ProfilePicture.Contains("jpeg"))
                {
                    filename = Base64ToImageSave(profil.ProfilePicture, profil.Id + ".jpg");
                }

                profil.ProfilePicture = filename;

                _db.Profiles.Add(profil);
                _db.SaveChanges();

            }
            return Json(profil);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Delete(Guid id)
		{
			Profile FoundProfile = _db.Profiles.Find(id);
			_db.Profiles.Remove(FoundProfile);
			_db.SaveChanges();
			return Json(FoundProfile);
		}


		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult GetById(Guid id)
		{
			Profile FoundProfile = _db.Profiles.Find(id);
			return Json(FoundProfile);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Edit(Profile _profile)
		{
			Profile FoundProfile = _db.Profiles.Find(_profile.Id);

			if (FoundProfile.Name != null)
				FoundProfile.Name = _profile.Name;
			if (FoundProfile.AboutMePartOne != null)
				FoundProfile.AboutMePartOne = _profile.AboutMePartOne;
			if (FoundProfile.AboutMePartTwo != null)
				FoundProfile.AboutMePartTwo = _profile.AboutMePartTwo;
			if (FoundProfile.Nationality != null)
				FoundProfile.Nationality = _profile.Nationality;
			if (FoundProfile.Marital != null)
				FoundProfile.Marital = _profile.Marital;
			if (FoundProfile.YearOfBirth != null)
				FoundProfile.YearOfBirth = _profile.YearOfBirth;
			if (FoundProfile.Phone != null)
				FoundProfile.Phone = _profile.Phone;
			if (FoundProfile.Email != null)
				FoundProfile.Email = _profile.Email;
			if (FoundProfile.Github != null)
				FoundProfile.Github = _profile.Github;
			if (FoundProfile.Skype != null)
				FoundProfile.Skype = _profile.Skype;
			if (FoundProfile.Linkedin != null)
				FoundProfile.Linkedin = _profile.Linkedin;
			if (FoundProfile.City != null)
				FoundProfile.City = _profile.City;
			if (FoundProfile.Summary != null)
				FoundProfile.Summary = _profile.Summary;

			FoundProfile.DateModified = DateTime.Now;

			_db.Profiles.Update(FoundProfile);
			_db.SaveChanges();
			return Json(FoundProfile);
		}

        private string Base64ToImageSave(string base64String, string name)
        {
            try
            {

                // Convert Base64 String to byte[] 
                byte[] imageBytes = Convert.FromBase64String(base64String.Split(',')[1]);

                var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                // Convert byte[] to Image 
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);

                image.Save(_env.ContentRootPath + "/wwwroot/ProfilePhotos/" + name);

                return name;


            }
            catch (Exception ex)
            {
                return "0";
            }
        }
    }
}
