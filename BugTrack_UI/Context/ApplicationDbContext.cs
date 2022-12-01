using AppData.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTrack_UI.Context
{
    public class ApplicationDbContext : IdentityDbContext<Data.Models.ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Bug> Bug { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bug>().HasData(
               new Bug { Id = 1, BugStatus = BugStatus.Open, Title = "Divide by zero error", Description = "When creating a new widget, if I select quantity 0 I get a divide by 0 exception", PriorityStatus = Priority.Medium, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech" },
               new Bug { Id = 2, BugStatus = BugStatus.Open, PriorityStatus = Priority.High, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech", Title = "Sql injection vulnerability", Description = "on the add new widget page the input is unsantised and allow for SQL injecton in the widget description field." }
           );
            modelBuilder.Entity<Data.Models.ApplicationUser>().HasData(
                new Data.Models.ApplicationUser { Email = "joshua@mvc.tech", Id = Guid.NewGuid().ToString(), UserName = "joshua@mvc.tech", FirstName = "Joshua", LastName = "Holden" }
                );
        }
    }
}
