﻿using Microsoft.AspNetCore.Mvc;
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
        private static Validation validation = Validation.getInstance();
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

                var rules = new Dictionary<string, string>()
                {
                    {"Email","Url"},
                    {"Custom", "EmptyString" },
                    {"Phone","Phone" },
                    {"FullName","Required" },
                    {"Regex",@"regularexpression:^[0-9\-\+]{9,15}$" }


                  

                };
                var customMessage =  new Dictionary<string, string>()
                {
                    {"Email","Không phải url"},
                    {"Custom", "Không được để trống trường này" },
                    {"Phone","Phải là số điện thoại" },
                    {"FullName","Phải có trường này" },
                    {"Regex",@"Custome regex" }




                };
                var dic = validation.Validator(form, rules, customMessage);
                ViewBag.DicError = dic;


            }               
            
            catch (Exception ex)
            {

                throw ex;
            }
            return View(form);

        }
    }
}
