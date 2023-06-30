using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Window_Services_SendEmail.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly string connectionString = "Server=PCA172\\SQL2017;Database=imagesdb;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=sa;Password=Tatva@123;Integrated Security=False; TrustServerCertificate=True";
        [HttpGet("getcall")]
        public ActionResult Get()
        {   
            return Ok("Hello from the API!");
        }
    }
}
