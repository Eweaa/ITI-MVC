using ITI_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_MVC.Context
{
    public class ITIContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string cnnStr = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build().GetSection("ConnectionString").Value;
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(cnnStr);
        }
    }
}
