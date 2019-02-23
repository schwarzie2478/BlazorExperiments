using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example01.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Example01BackEnd.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationController : Controller
    {
        // GET: api/<controller>  
        [HttpGet]
        public IEnumerable<Application> Get()
        {
            return new List<Application>() { new Application("TestApplication","Testing from GetApplications") };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Application Get(int id)
        {
            return new Application("SingleApplication","Testing from Get");
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Application value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Application value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
