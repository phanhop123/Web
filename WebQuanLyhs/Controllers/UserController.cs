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
					var claims = new List<Claim> {
								new Claim(ClaimTypes.Email, user.Email),
								new Claim(ClaimTypes.Name, user.Fullname),                                                                              
								//claim - role động
								new Claim(ClaimTypes.Role, user.Role_id == 1 ?"Admin" : user.Role_id == 2 ?"Get training staff" :  user.Role_id == 3 ?"Teacher" : user.Role_id == 4 ?"Student" :"Admin" )
							};

					var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);

                    if (user.Role_id == 1 )
                    {
                        return RedirectToAction("AdminPage", "Home");
                    }
                    else if (user.Role_id == 2)
                    {
                        return RedirectToAction("TrainingStaffPage", "Home");
                    }
                    else if (user.Role_id == 3)
                    {
                        return RedirectToAction("TeacherPage", "Home");
                    }
                    else if (user.Role_id == 4)
                    {
                        return RedirectToAction("StudentPage", "Home");
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
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

    }
}
