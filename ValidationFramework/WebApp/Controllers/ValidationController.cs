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
        private static Validation validation = Validation.getInstance();
        public IActionResult Index()
        {
            return View();
        }
        public  string Check(string Value)
        {
            if (Value == "" || Value==null) return "Khong đc để trống";
            return null;
            
        }
        [HttpPost]
        public IActionResult Index(Form form)
        {
            string message = string.Empty;
            try
            {            
                //Tham số rules là Dic chứa các trường valid và kiểu valid mong muốn
                var rules = new Dictionary<string, string>
                {
                    {"Email","email|required"},
                    {"Custom", "EmptyString" },
                    {"Phone","Phone" },
                    {"FullName","required"},
                    {"Regex",@"regularexpression:^[0-9\-\+]{9,15}$" }
                };
                //Tham số custom chính là Dic chứa các mã lỗi custom của các trường
                var customMessage =  new Dictionary<string, string>()
                {
                    {"Email","Không phải là email"},
                    {"Custom", "Trường này phải được để trống" },
                    {"Phone","Phải là số điện thoại" },
                    {"FullName","Full Name là trường bắt buộc" },
                    {"Regex",@"Custome regex" }
                };
                //Hàm DicFun chính là hàm tạo nhanh một Custom. Cho phép ta gửi một function custom vào
                var dicFun = new List<ListValidFunc>();
                dicFun.Add(new ListValidFunc() { FeildName="Email",Func=Check});
                dicFun.Add(new ListValidFunc() { FeildName = "Custom", Func = Check });
            
                var dic = validation.Validator(form, rules, customMessage);
                //Biến dic chính là kết quả mã lỗi trả ra cho từng trường
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
