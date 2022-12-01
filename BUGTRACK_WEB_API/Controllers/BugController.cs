using AppData;
using AppData.Models;
using BugTrack_UI.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BugTrack_WEB_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugController : Controller
    {
        public ApplicationDbContext _DatabaseContext;
        public BugController(ApplicationDbContext applicationDbContext)
        {
            _DatabaseContext = applicationDbContext;
        }
        [HttpGet("SearchBugs")]
        public ActionResult SearchBugs(System.String SearchQuery)
        {
            try
            {
                var BugsFound = _DatabaseContext.Bug
                    .Where(x => x.Title!.Contains(SearchQuery) || x.Description!.Contains(SearchQuery))
                    .OrderByDescending(x => x.CreatedDate);                    
                if (BugsFound != null)
                {
                    return Ok(BugsFound);
                }
                else
                {
                    return BadRequest(new { Message = "No bugs exist" });
                }
            }
            catch (Exception e)
            {
                //TO DO LOG!                
                return BadRequest(new { Message = "An error occured while requesting your bugxs, if this continues to happen please contact your support representitive, the error has been logged. " });
            }
        }
        [HttpGet("GetBugsCreatedBy")]
        public ActionResult GetBugsCreatedBy(string UserName)
        {
            try
            {
                var BugsFound = _DatabaseContext.Bug
                    .Where(x=>x.CreatedBy == UserName)
                    .OrderByDescending(x=>x.CreatedDate).ToList();
                if (BugsFound != null)
                {
                    return Ok(BugsFound);
                }
                else
                {
                    return BadRequest(new { Message = "No bugs exist" });
                }
            }
            catch (Exception e)
            {
                //TO DO LOG!                
                return BadRequest(new { Message = "An error occured while requesting your bugxs, if this continues to happen please contact your support representitive, the error has been logged. " });
            }
        }

        [HttpGet("GetAllBugs")]
        public ActionResult GetAllBugs()
        {
            try
            {
                var BugsFound = _DatabaseContext.Bug
                    .OrderByDescending(x => x.CreatedDate);                    
                if (BugsFound != null)
                {
                    return Ok(BugsFound);
                }
                else
                {
                    return BadRequest(new { Message = "No bugs exist" });
                }
            }
            catch (Exception e)
            {
                //TO DO LOG!                
                return BadRequest(new { Message = "An error occured while requesting your bugxs, if this continues to happen please contact your support representitive, the error has been logged. " });
            }
        }

        [HttpGet("GetBugById")]
        public ActionResult GetBugById(int Id)
        {
            try
            {
                var BugFound = _DatabaseContext.Bug.Find(Id);
                if (BugFound != null)
                {
                    return Ok(BugFound);
                }
                else
                {
                    return BadRequest(new { Message = "No bug with this ID exists" });
                }               
            }
            catch (Exception e)
            {
                //TO DO LOG!                
                return BadRequest(new { Message = "An error occured while requesting your bug, if this continues to happen please contact your support representitive, the error has been logged. " });
            }
        }

        [HttpPost("UpdateBug")]
        public ActionResult UpdateBug(int Id, [FromBody] Bug ChangedBug)
        {
            try
            {         
                    _DatabaseContext.Update(ChangedBug);
                    _DatabaseContext.SaveChanges();
                    return Ok(ChangedBug);               
            }
            catch (Exception e)
            {
                //TO DO LOG!                
                return BadRequest(new { Message = "An error occured while requesting your bug, if this continues to happen please contact your support representitive, the error has been logged. " });
            }
        }


        [HttpPost("CreateBug")]
        public ActionResult CreateBug(Bug TheBug)
        {
            try
            {
                _DatabaseContext.Bug.Add(TheBug);
                _DatabaseContext.SaveChanges();
                return Ok(TheBug);
            }
            catch (Exception e)
            {
                //TO DO LOG!                
                return BadRequest(new {Message ="An error occured while creating your bug, if this continues to happen please contact your support representitive, the error has been logged. "});
            }           
        }
        [HttpPost("DeleteBug")]
        public ActionResult DeleteBug(int Id)
        {
            try
            {
                var BugDelete = _DatabaseContext.Bug.Find(Id);
                if (BugDelete != null)
                {
                    _DatabaseContext.Bug.Remove(BugDelete);
                    _DatabaseContext.SaveChanges();
                }
                else
                {
                    return BadRequest(new { Message = "No bug with this ID exists" });
                }
                return Ok(new { Message = "Bug deleted successfully." });
            }
            catch (Exception e)
            {
                //TO DO LOG!                
                return BadRequest(new { Message = "An error occured while deleting your bug, if this continues to happen please contact your support representitive, the error has been logged. " });
            }
        }


    }
}
