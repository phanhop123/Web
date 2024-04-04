using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BusinessObject.Context;
using BusinessObject.Data;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using BusinessObject.Viewmodel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebQuanLyhs.Controllers
{
	public class UserController : Controller
	{
        public static UserController instance;
        public static readonly object instanceLock = new object();
        public static UserController Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserController();
                    }
                    return instance;
                }
            }
        }
        private readonly ConnectDB db = new ConnectDB();
		public IActionResult Login(string? ReturnUrl)
		{
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
		}
	
		[HttpPost]
		public async Task<IActionResult> Login(UserLogin model, string? ReturnUrl)
		{
            ViewBag.ReturnUrl = ReturnUrl;
            var user = db.Users.SingleOrDefault(kh => kh.Email == model.Email);

			if (user == null)
			{
				ModelState.AddModelError("Error ", "Account or Password is incorrect");
			}
			else
			{
				if (user.Password != model.Password)
				{
					ModelState.AddModelError("Error", "Account or Password is incorrect");
				}
				else
				{
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Name", user.Fullname);
                    HttpContext.Session.SetInt32("ID", user.User_id);
                    HttpContext.Session.SetInt32("Role", user.Role_id);
              
                    if (user.Role_id == 1 )
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (user.Role_id == 2)
                    {
                        return RedirectToAction("TeacherIndex", "StaffTrain");
                    }
                    else if (user.Role_id == 3)
                    {
                        return RedirectToAction("index", "Teacher");
                    }
                    else if (user.Role_id == 4)
                    {
                        return RedirectToAction("index", "Student");
                    }
                    if (Url.IsLocalUrl(ReturnUrl))
					{
						return Redirect(ReturnUrl);
					}
                    return Redirect("User/Login");

                }

            }
				return View();
		}
        public IActionResult DangXuat()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear(); // Clear the session
            return Redirect("/");
        }

    }
}
