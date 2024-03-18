using AutoMapper;
using BusinessObject.Context;
using BusinessObject.Data;
using BusinessObject.Viewmodel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace WebQuanLyhs.Controllers
{
    public class AdminController : Controller
    {
        private readonly ConnectDB db;
        private readonly IMapper _mapper;

        public AdminController(ConnectDB context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        
       


        public IActionResult Index()
        {
            var usersWithRoles = db.Users.Include(u => u.Role).ToList();
            var admin = db.Users;
            
            return View(admin);
        }
        public ActionResult AddAccount()
        {
            var phanLoaiSVList = db.Roles.ToList();
            ViewBag.PhanLoaiSVList = new SelectList(phanLoaiSVList, "Role_id", "Role_name");
            return View();
        }
        [HttpPost]
        public IActionResult AddAccount(UserLogin model)
        {
            if (ModelState.IsValid)
            {
               
                 var admin = _mapper.Map<User>(model);
                 db.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult EditAcc(int id)
        {
            var item = db.Users.Find(id);
            var phanLoaiSVList = db.Roles.ToList();
            ViewBag.PhanLoaiSVList = new SelectList(phanLoaiSVList, "Role_id", "Role_name");
            return View(item);
        }
        [HttpPost]
        public IActionResult EditAcc(User model)
        {
           
                db.Users.Attach(model);

                db.Update(model);

                db.SaveChanges();
                    return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteAcc(int id)
        {
            var item = db.Users.Find(id);
            if (item != null)
            {
                /*var DeleteItem=db.Categories.Attach(item);*/
                db.Users.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
