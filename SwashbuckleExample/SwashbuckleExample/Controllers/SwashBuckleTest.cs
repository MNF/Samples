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

        /// <summary>
        ///  line 1.    
        ///   line 2.    
        ///<br />
        ///   line 3.    
        /// </summary>
        /// <remarks>
        ///  before.  
        /// &lt;br /&gt;
        ///  after.  
        /// </remarks>
        [HttpPost]
        [Route("{id}")]
        public SwashbuckleTestProfile Post(Guid id, List<SwashbuckleTestProfile> companies)
        {
            return companies.FirstOrDefault();
        }
        /// <summary>
        ///  line 1.    
        ///   line 2.    
        ///<br />
        ///   line 3.    
        /// </summary>
        /// <remarks>
        ///  before.\
        ///  after.  
        /// </remarks>
        [HttpPost]
        [Route("WithoutBr/{id}")]
        public SwashbuckleTestProfile WithoutBr(Guid id, List<SwashbuckleTestProfile> companies)
        {
            return companies.FirstOrDefault();
        }
        /// <summary>
        ///  before.\
        ///  after.  
        /// </summary>
        /// <remarks>
        ///  before. \
        ///  after.  
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="companies"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TwoLinesInRemarks{id}")]
        public SwashbuckleTestProfile TwoLinesInRemarks(Guid id, List<SwashbuckleTestProfile> companies)
        {
            return companies.FirstOrDefault();
        }
        /// <summary>
        ///  During transition period the method could return 501 HttpStatusCode(NotImplemented) .  
        ///  If the method returns 200(success) client need to use the results  .  
        /// </summary>
        /// <remarks>
        ///  NotImplemented) .  
        ///  results  .  
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="companies"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MinimalTwoLinesInRemarks{id}")]
        public SwashbuckleTestProfile MinimalTwoLinesInRemarks(Guid id, List<SwashbuckleTestProfile> companies)
        {
            return companies.FirstOrDefault();
        }
        /// <remarks>
        ///line without dot 
        ///         ///  remarks line 1   endng with . and 2 spaces.  
        ///  trailing 2 spaces  line 2   endng with 3 spaces.     
        /// 
        ///  summary line 1  endng with 2 spaces.  
        ///  &lt;br /&gt;
        ///  summary line 2  
        /// remarks line 3    endng with 2 spaces
        ///line 4 no trailing spaces  
        ///  &lt;br /&gt;
        /// 
        ///  During transition period the method could return 501 HttpStatusCode(NotImplemented) .  
        ///  If the method returns 200(success) client need to use the results  .  
        ///line 5 no trailing spaces   &lt;br /&gt;
        ///line 6 no trailing spaces   &lt;br /&gt;
        ///</remarks>
        [HttpPost]
        [Route("PostWithFromBody/{id}")]
        public SwashbuckleTestProfile PostWithFromBody(Guid id, [FromBody]List<SwashbuckleTestProfile> companies)
        {
            return companies.FirstOrDefault();
        }
    }
    public class SwashbuckleTestProfile
    {
  
        public string Email { get; set; }

        public DateTime DatOfBirth { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
       [DataType(DataType.Date)]
        public DateTime DatOfBirth2prop { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DatOfBirthDisplayFormat { get; set; }
     
        [DataType(DataType.Date)]
        public DateTime DatOfBirthDataType { get; set; }
        public string LastName { get; set; }
    }
}
