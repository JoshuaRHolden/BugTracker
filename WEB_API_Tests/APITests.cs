using AppData.Models;
using BugTrack_UI.Context;
using BugTrack_WEB_API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace WEB_API_Tests
{
    [TestFixture]
    public class APITests
    {
        private ApplicationDbContext _Context { get; set; }
        private BugController _BugController { get; set; }
        #region setup and teardown
        [OneTimeSetUp]
        //Create an in memory testing EF context for passing into the controller.
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BugTrackDb")
                .Options;
            _Context = new ApplicationDbContext(options);

            _Context.Bug.AddRange(
                new Bug
                {
                    AssignedTo = "joshua",
                    BugStatus = BugStatus.Open,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "joshua",
                    Description = "test",
                    Id = 1,
                    PriorityStatus = Priority.Critical,
                    Title = "test",

                },
                new Bug
                {
                    AssignedTo = "joshua",
                    BugStatus = BugStatus.Open,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "joshua",
                    Description = "test",
                    Id = 2,
                    PriorityStatus = Priority.Critical,
                    Title = "test"
                },
                new Bug
                {
                    AssignedTo = "joshua",
                    BugStatus = BugStatus.Open,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "joshua",
                    Description = "test",
                    Id = 3,
                    PriorityStatus = Priority.Critical,
                    Title = "test"
                });
            _Context.SaveChanges();
            _BugController = new BugController(_Context);

        }
        [OneTimeTearDown]
        public void Cleanup()
        {
            _BugController.Dispose();
            _Context.Dispose();
        }
        #endregion


        [Test]
        public void Get_Returns3Bugs()
        {
            var bugs = _BugController.GetAllBugs();
            var Result = bugs as OkObjectResult;
            var x = Result!.Value as Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<AppData.Models.Bug>;
            Assert.IsInstanceOf(typeof(OkObjectResult), bugs as OkObjectResult);
            Assert.AreEqual(x!.Count(), 3);
        }

        [Test]
        public void GetSingle_Returns1Bug()
        {
            var bugs = _BugController.GetBugById(1);
            var Result = (bugs as OkObjectResult).Value as Bug;
            Assert.AreEqual(Result.Id, 1);
            Assert.IsInstanceOf(typeof(OkObjectResult), bugs as OkObjectResult);
        }

        [Test]
        public void GetSingle_WithOutOfRangeReturnsBadrequest()
        {
            var bugs = _BugController.GetBugById(454);
            var Result = (bugs as BadRequestObjectResult).Value;
            Assert.AreEqual("{ Message = No bug with this ID exists }", Result.ToString());
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), bugs as BadRequestObjectResult);
        }


        [Test]
        public void GetCreatedBy_Returns3Bug()
        {
            var bugs = _BugController.GetBugsCreatedBy("joshua");
            var Result = (bugs as OkObjectResult);
            var x = Result!.Value as List<AppData.Models.Bug>;
            Assert.IsInstanceOf(typeof(OkObjectResult), bugs as OkObjectResult);
            Assert.AreEqual(x!.Count(), 3);
        }

        [Test]
        public void GetCreatedBy_UnknownUser_ReturnsBadRequest()
        {
            var bugs = _BugController.GetBugsCreatedBy("John Doe");
            var Result = (bugs as BadRequestObjectResult).Value;
            Assert.AreEqual("{ Message = No bugs exist }", Result.ToString());
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), bugs as BadRequestObjectResult);
        }



        [Test]
        public void CreateBug_CreatesBug()
        {
            var bugs = _BugController.CreateBug(new Bug { Id = 4, AssignedTo = "joshua", BugStatus = BugStatus.Open, CreatedBy = "joshua", CreatedDate = DateTime.Now, Description = "Test", PriorityStatus = Priority.Critical, Title = "test" });
            var Result = (bugs as OkObjectResult);
            var x = Result!.Value as List<AppData.Models.Bug>;
            Assert.IsInstanceOf(typeof(OkObjectResult), bugs as OkObjectResult);
            var bug = _Context.Bug.Any(x => x.Id == 4);
            Assert.IsTrue(bug);
        }

        [Test]
        public void CreateBug_InvalidReturnsBadRequest()
        {
            var bugs = _BugController.CreateBug(new Bug { Id = null });
            var Result = (bugs as BadRequestObjectResult).Value;
            Assert.AreEqual("{ Message = An error occured while creating your bug, if this continues to happen please contact your support representitive, the error has been logged.  }", Result.ToString());
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), bugs as BadRequestObjectResult);
        }

        [Test]
        public void DeleteBug_DeletesBug()
        {
            var bugs = _BugController.DeleteBug(4);
            var Result = (bugs as OkObjectResult);
            var x = Result!.Value as List<AppData.Models.Bug>;
            Assert.IsInstanceOf(typeof(OkObjectResult), bugs as OkObjectResult);
            var bug = _Context.Bug.Any(x => x.Id == 4);
            Assert.IsFalse(bug);
        }

        [Test]
        public void DeleteBug_InvalidID_ReturnsBadRequest()
        {
            var bugs = _BugController.DeleteBug(499);
            var Result = (bugs as BadRequestObjectResult).Value;
            Assert.AreEqual("{ Message = }", Result.ToString());
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), bugs as BadRequestObjectResult);
        }
    }
}

