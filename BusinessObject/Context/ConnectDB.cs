using Microsoft.EntityFrameworkCore;
using BusinessObject.Data;
using Microsoft.Extensions.Configuration;
using BusinessObject.Viewmodel;

namespace BusinessObject.Context
{
	public class ConnectDB : DbContext
	{
		public ConnectDB() { }
		public ConnectDB(DbContextOptions<ConnectDB> options) : base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
			IConfigurationRoot configuration = builder.Build();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("Qlhs"));
		}
		public virtual DbSet<Role> Roles { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Course> Courses { get; set; }
		public virtual DbSet<Student_Course> Student_Courses { get; set; }
		public virtual DbSet<Category_Course> Category_Courses { get; set; }
		public virtual DbSet<Teacher_Course> Teacher_Courses { get; set; }
		public virtual DbSet<Teacher_Class> Teacher_Classes { get; set; }
		public virtual DbSet<Exercise> Exercises { get; set; }
		public virtual DbSet<Class_Role> Class_Roles { get; set; }
		public virtual DbSet<Student_Class> Student_Classes { get; set; }



	}
}
