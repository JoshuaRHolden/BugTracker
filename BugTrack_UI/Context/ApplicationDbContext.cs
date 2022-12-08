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
               new Bug { Id = 2, BugStatus = BugStatus.Open, PriorityStatus = Priority.High, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech", Title = "Sql injection vulnerability", Description = "on the add new widget page the input is unsantised and allow for SQL injecton in the widget description field." },
               new Bug { Id = 4, BugStatus = BugStatus.Open, PriorityStatus = Priority.Medium, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech", Title = "Test bug 1", Description = "test bug 1." },
               new Bug { Id = 5, BugStatus = BugStatus.Open, PriorityStatus = Priority.Medium, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech", Title = "Test bug 2", Description = "test bug 2." },
               new Bug { Id = 6, BugStatus = BugStatus.Open, PriorityStatus = Priority.Medium, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech", Title = "Test bug 3", Description = "test bug 3." },
               new Bug { Id = 7, BugStatus = BugStatus.Open, PriorityStatus = Priority.Medium, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech", Title = "Test bug 4", Description = "test bug 4." },
               new Bug { Id = 8, BugStatus = BugStatus.Open, PriorityStatus = Priority.Medium, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech", Title = "Test bug 5", Description = "test bug 5." },
               new Bug { Id = 9, BugStatus = BugStatus.Open, PriorityStatus = Priority.Medium, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech", Title = "Test bug 6", Description = "test bug 6." },
               new Bug { Id = 10, BugStatus = BugStatus.Open, PriorityStatus = Priority.Medium, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech", Title = "Test bug 7", Description = "test bug 7." },
               new Bug { Id = 11, BugStatus = BugStatus.Open, PriorityStatus = Priority.Medium, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech", Title = "Test bug 8", Description = "test bug 8." },
               new Bug { Id = 12, BugStatus = BugStatus.Open, PriorityStatus = Priority.Medium, CreatedBy = "joshua@mvc.tech", AssignedTo = "joshua@mvc.tech", Title = "Test closed bug 1", Description = "test closed bug 1." }
           );
            modelBuilder.Entity<Data.Models.ApplicationUser>().HasData(
                new Data.Models.ApplicationUser { Email = "joshua@mvc.tech", Id = Guid.NewGuid().ToString(), UserName = "joshua@mvc.tech", FirstName = "Joshua", LastName = "Holden" },
                new Data.Models.ApplicationUser { Email = "joshua@fakeemail.com", Id = Guid.NewGuid().ToString(), UserName = "joshua@fakeemail.com", FirstName = "Joshua", LastName = "Jones" }
                );
        }
    }
}