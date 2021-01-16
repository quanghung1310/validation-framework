using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ValidationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(JObject form)
        {
            string message = string.Empty;
            try
            {
              
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View();

        }
    }
}
