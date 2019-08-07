using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicStore.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectronicStore.API.UserAPI
{
    [Route("api/Order")]
    public class UserRestController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserRestController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: api/<controller>
        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _db.ApplicationUsers.Where(p => p.FullName.Contains(term)).Select(u => u.FullName).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }
        // GET: api/<controller>
        [Produces("application/json")]
        [HttpGet("searchEmail")]
        public async Task<IActionResult> SearchEmail()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var emails = _db.ApplicationUsers.Where(p => p.Email.Contains(term)).Select(u => u.Email).ToList();
                return Ok(emails);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
