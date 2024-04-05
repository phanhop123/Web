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
        public static UserController ?instance;
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
		public async Task<IActionResult>Login(UserLogin model, string? ReturnUrl)
		{
            ViewBag.ReturnUrl = ReturnUrl;
            var user = db.Users.SingleOrDefault(kh => kh.Email == model.Email && kh.Password == model.Password);
            
            if (user?.Email != model.Email || user?.Password != model.Password)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View();
            }
            if(user == null)
            {
                return View();
            }   
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("Name", user.Fullname);
                HttpContext.Session.SetInt32("ID", user.User_id);
                HttpContext.Session.SetInt32("Role", user.Role_id);

            if (user.Role_id == 1)
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

            return RedirectToAction("Login");


        }
       
        public IActionResult DangXuat()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear(); // Clear the session
            return Redirect("/");
        }
        public ActionResult Profile()
        {
            // Lấy ID của người dùng từ session
            int? userId = HttpContext.Session.GetInt32("ID");

            if (userId == null)
            {
                // Xử lý khi không có ID trong session
                return RedirectToAction("Login", "User"); // Chuyển hướng đến trang đăng nhập nếu không có ID
            }

            // Tìm thông tin người dùng từ ID
            var user = db.Users.FirstOrDefault(u => u.User_id == userId);

            if (user == null)
            {
                // Xử lý khi không tìm thấy thông tin người dùng
                return NotFound();
            }

            return View(user);
        }
        public ActionResult EditProfile(int id)
        {
            var item = db.Users.Find(id);
            return View(item);
        }
        [HttpPost]
        public IActionResult EditProfile(User model)
        {
            // Tìm thông tin người dùng từ cơ sở dữ liệu
            var user = db.Users.FirstOrDefault(u => u.User_id == model.User_id);

            if (user == null)
            {
                // Xử lý khi không tìm thấy thông tin người dùng
                return NotFound();
            }

            user.User_id = model.User_id;
            user.Password = model.Password;
            user.Phone = model.Phone;
            user.Fullname = model.Fullname;
            user.Detail = model.Detail;
            user.Sex_name = model.Sex_name;
            user.CCCD = model.CCCD;

            // Cập nhật thông tin của người dùng
            // Cập nhật các trường thông tin khác nếu cần

            // Cập nhật role của người dùng

            db.Entry(user).State = EntityState.Modified;

            db.SaveChanges();

            // Chuyển hướng đến trang Profile
            return RedirectToAction("Profile");
        }


    }
}

