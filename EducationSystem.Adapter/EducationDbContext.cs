using EducationSystem.Domain.AuthModels;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Relationships;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Adapter
{
    public class EducationDbContext : DbContext
		{
			public EducationDbContext(DbContextOptions options) : base(options)
			{
				//Database.EnsureCreated();
			}

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				modelBuilder.Entity<ItemInCurriculum>().HasKey(u => new { u.ItemId,u.CurriculumId});// комбинированный ключ
				modelBuilder.Entity<StudentInClass>().HasKey(u => new { u.StudentId, u.SchoolClassId });// комбинированный ключ
				//modelBuilder.Entity<Curriculum>().ToTable(t => t.HasCheckConstraint("YearFormation", "YearFormation>0"));
			}


			//modelBuilder.Entity<Curriculum>().ToTable(t => t.HasCheckConstraint("Age", "Age > 0 AND Age < 120"));
			//public DbSet<User> UserData { get; set; }
			public DbSet<School>Schools { get; set; }
			public DbSet<SchoolItem> Items { get; set; }
			public DbSet<Curriculum> Curriculums { get; set; }
			public DbSet<Person> Teachers { get; set; }
			public DbSet<SchoolClass> Classes { get; set; }
			public DbSet<ItemInCurriculum> RelationItemsInCurriculums{ get; set; }
			public DbSet<StudentInClass> RelationStudentsInClasses { get; set; }
			public DbSet<Assessment> Assessments { get; set; }
			public DbSet<User> Users { get; set; }
	}
}
