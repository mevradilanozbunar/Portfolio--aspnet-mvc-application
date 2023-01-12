using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models
{
	[Table("ProjectsCategories")]

	public class ProjectCategory : ModelBase
	{
		
       
        public string? Name { get; set; }
		public int? ProjectCount { get; set; } = 0;
		public ICollection<Projects> Projects { get; set; }

	}
}
