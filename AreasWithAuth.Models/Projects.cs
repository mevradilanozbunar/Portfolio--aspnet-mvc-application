using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models
{
	[Table("Projects")]
	public class Projects :ModelBase

	{
		
        public string? Name { get; set; }
		public Guid ProjectCategoryId { get; set; }

		public ProjectCategory ProjectCategory { get; set; }
		
        public string? Baslik { get; set; }
		
		
        public string? Icerik { get; set; }

		public string? Foto { get; set; }

		public int ViewsCount { get; set; } = 0;

		public string? GithubLink { get; set; }

	
        public string? ProgramlamaDilleri { get; set; }
		
        public string? Teknolojiler { get; set; }
	}


}
