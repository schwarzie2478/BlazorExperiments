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
            return await _db.Applications.Include( app => app.ApplicationRoles).ToListAsync<Application>();
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
        public async Task OnPutAsync(int id, [FromBody]Application value)
        {
            _db.Applications.Attach(value).State = EntityState.Modified;
            await _db.SaveChangesAsync();
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

        //ApplicationRoles Management
        [Route("{id}/applicationrole")]
        [HttpPost]
        public async Task OnCreateApplicationRole(int id,  ApplicationRole role)
        {
            var application = _db.Applications.Include(app => app.ApplicationRoles).First(app => app.Id == id);
            application.ApplicationRoles.Add(role);
            await _db.SaveChangesAsync();
        }
        [Route("{id}/applicationrole")]
        [HttpDelete]
        public async Task OnDeleteApplicationRole(int id, ApplicationRole role)
        {
            var application = _db.Applications.Include(app => app.ApplicationRoles).First(app => app.Id == id);
            var roleDelete = application.ApplicationRoles.Where(r => r.Id == role.Id).First();
            application.ApplicationRoles.Remove(roleDelete);
            await _db.SaveChangesAsync();
        }
    }
}
