using AreasWithAuth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Experience> Experiences { get; set; }
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<AppUserRole> AppUserRoles { get; set; }
		public DbSet<Profile> Profiles { get; set; }

		public DbSet<Education> Educations { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<Skills> Skills { get; set; }
		public DbSet<Projects> Projects { get; set; }
		public DbSet<ProjectCategory> ProjectCategories { get; set; }

		public DbSet<ProjectPhoto> ProjectPhotos { get; set; }

   










    }
}
