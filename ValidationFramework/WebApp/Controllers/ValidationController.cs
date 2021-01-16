using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValidationFramework;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ValidationController : Controller
    {
        private static Validation validation = new Validation();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Form form)
        {
            string message = string.Empty;
            try
            {

                var Rules = new Dictionary<string, string>()
                {
                    {"Email","Email"},
                    {"Password", "Password"}

                };
                validation.Validator(form, Rules);


            }               
            
            catch (Exception ex)
            {

                throw ex;
            }
            return View();

        }
    }
}
