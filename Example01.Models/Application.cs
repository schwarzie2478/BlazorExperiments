using System;
using System.Collections.Generic;

namespace Example01.Models
{
    public class Application
    {
        public Application()
        {
            ApplicationRoles = new HashSet<ApplicationRole>();
        }
        public Application( string name, string description)
        {
            Name = name;
            Description = description;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ApplicationRole> ApplicationRoles { get; private set;}
    }
}
