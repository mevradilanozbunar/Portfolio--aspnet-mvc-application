using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models.ViewModels
{
    public class Allinfo
    {
        public List<Education> Educations { get; set; }
        public List<Experience> Experiences { get; set; }
		public List<Experience> LastExperience { get; set; }
		public List<Skills> Skills { get; set; }
        public List<Language> Languages { get; set; }

        public List<Profile> Profiles { get; set; }
        public List<ProjectCategory> ProjectCategories { get; set; }
        public List<Projects> Projects { get; set; }

        public List<ProjectPhoto> projectPhotos { get; set; }

		public List<Projects> topViewsProjects { get; set; }
		public List<Projects> RecentsProjects { get; set; }

        public List<AppUser>  appUsers{ get; set; }
    }
}
