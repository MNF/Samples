using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace MyControllers
{
    [Route("[controller]")]
    public class SwashbuckleTest : Controller
    {
        [FromHeader(Name = "x-guid-id")]
        [Required]
        public Guid GuidId { get; set; }

        [FromHeader(Name = "x-string-id")]   
        [Required]
        public string StringId { get; set; }
        [HttpPost]

        [Route("{id}")]
        public SwashbuckleTestProfile Post(Guid id, List<SwashbuckleTestProfile> companies)
        {
            return companies.FirstOrDefault();
        }
    }
    public class SwashbuckleTestProfile
    {
  
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
