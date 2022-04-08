using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaController : ControllerBase
    {


        [HttpGet]

        public IActionResult GetPrueba()
        {

            return Content("success baby");
        }


    }
}
