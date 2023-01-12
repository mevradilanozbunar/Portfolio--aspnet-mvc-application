using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models.ViewModels
{
	public class ProjectModel
	{
		public ProjectCategory projectCategories { get; set; }
		public Projects projects { get; set; }
		public List<ProjectPhoto> projectPhotos { get; set; }
	}
}
