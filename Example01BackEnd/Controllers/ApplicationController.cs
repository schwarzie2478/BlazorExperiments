using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example01.Models;
using Example01.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Example01BackEnd.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationController : Controller
    {
        private ExampleDbContext _db;
        
        public ApplicationController(ExampleDbContext dBContext)
        {
            _db = dBContext;
        }
        // GET: api/<controller>  
        [HttpGet]
        public async Task<IEnumerable<Application>> OnGetAsync()
        {
            return await _db.Applications.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<Application> OnGetAsync(int id) => await _db.Applications.FindAsync(id);

        // POST api/<controller>
        [HttpPost]
        public async Task OnPostAsync([FromBody]Application value)
        {
            _db.Applications.Add(value);
            await _db.SaveChangesAsync();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Application value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var application = new Application();
            application.Id = id;
            _db.Applications.Attach(application).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }
    }
}
