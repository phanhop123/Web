﻿using AutoMapper;
using BusinessObject.Context;
using BusinessObject.Data;
using BusinessObject.Viewmodel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Security.Claims;

namespace WebQuanLyhs.Controllers
{
    public class StaffTrainController : Controller
    {

        private readonly ConnectDB db;
        private readonly IMapper _mapper;

        public StaffTrainController(ConnectDB context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;


        }
        #region Teacher
        public IActionResult TeacherIndex()
        {
            var usersWithRoles = db.Users.Include(u => u.Role).ToList();
            var admin = db.Users.Where(u => u.Role_id == 3).ToList();
            return View(admin);
        }

        public ActionResult AddAccount()
        {
            var course = db.Courses.ToList();
            ViewBag.KhoaHocSVList = new SelectList(course, "Coures_id", "Coures_name");


            return View();
        }
        [HttpPost]
        public IActionResult AddAccount(CStudent user)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    Email = user.Email,
                    Password = user.Password,
                    Fullname = user.Fullname,
                    Role_id = 3


                    // Gán các thuộc tính của User tương ứng từ model
                };

                db.Users.Add(newUser);
                db.SaveChanges();
                var users = db.Users.FirstOrDefault(u => u.Email == user.Email);
                var course = db.Courses.FirstOrDefault(u => u.Coures_id == user.Coures_id);

                var newDetail = new Teacher_Course
                {
                    Teacher_Coures_id = users.User_id,
                    Teaching_major = user.Major,
                    Fullname = user.Fullname,
                    Course_id = course.Coures_id

                    // Gán các thuộc tính của UserDetail tương ứng từ model
                };

                db.Teacher_Courses.Add(newDetail);

                db.SaveChanges();

                return RedirectToAction("TeacherCourseIndex");
            }
            return View(user);

        }
        public IActionResult EditTeacher(int id)
        {

            var item = db.Users.Find(id);
            return View(item);
        }
        [HttpPost]
        public IActionResult EditTeacher(User model)
        {
            db.Users.Attach(model);
            db.Update(model);

            db.SaveChanges();
            return RedirectToAction("TeacherIndex");
        }
        [HttpPost]
        public ActionResult DeleteTeacher(int id)
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


        #endregion
        #region Student
        public IActionResult StudentIndex()
        {
            var usersWithRoles = db.Users.Include(u => u.Role).ToList();
            var admin = db.Users.Where(u => u.Role_id == 4).ToList();
            return View(admin);
        }

        public ActionResult AddStudent()
        {
            var phanLoaiSVList = db.Roles.ToList();
            var phanLoaiSVListFiltered = phanLoaiSVList.Where(item => item.Role_id == 4).ToList();
            ViewBag.PhanLoaiSVList = new SelectList(phanLoaiSVListFiltered, "Role_id", "Role_name");

            return View();
        }
        [HttpPost]
        public IActionResult AddStudent(CStudent user)
        {
            // Kiểm tra xem có User nào có user_id bằng 4 không


            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    Email = user.Email,
                    Password = user.Password,
                    Fullname = user.Fullname,
                    Role_id = 4


                    // Gán các thuộc tính của User tương ứng từ model
                };

                db.Users.Add(newUser);
                db.SaveChanges();
                var users = db.Users.FirstOrDefault(u => u.Email == user.Email);
                var newDetail = new Student_Course
                {
                    Student_id = users.User_id,
                    Major = user.Major,

                    // Gán các thuộc tính của UserDetail tương ứng từ model
                };

                db.Student_Courses.Add(newDetail);

                db.SaveChanges();

                return RedirectToAction("StudentCourseIndex");
            }
            return View(user);
        }
        public IActionResult EditStudent(int id)
        {
            var phanLoaiSVList = db.Roles.ToList();
            var phanLoaiSVListFiltered = phanLoaiSVList.Where(item => item.Role_id == 4).ToList();
            ViewBag.PhanLoaiSVList = new SelectList(phanLoaiSVListFiltered, "Role_id", "Role_name");
            var item = db.Users.Find(id);
            return View(item);
        }
        [HttpPost]
        public IActionResult EditStudent(User model)
        {
            db.Users.Attach(model);
            db.Update(model);

            db.SaveChanges();
            return RedirectToAction("StudentIndex");
        }
        [HttpPost]
        public ActionResult DeleteStudent(int id)
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

        #endregion
        #region Category_Course
        public IActionResult CategoryIndex()
        {
            var category = db.Category_Courses.ToList();
            return View(category);
        }
        public ActionResult AddCategory()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category_Course model)
        {
            if (ModelState.IsValid)
            {
                db.Category_Courses.Add(model);
                db.SaveChanges();
                return RedirectToAction("CategoryIndex");
            }
            return View();

        }
        public IActionResult EditCategory(int id)
        {
            var item = db.Category_Courses.Find(id);
            return View(item);
        }
        [HttpPost]
        public IActionResult EditCategory(Category_Course model)
        {
            db.Category_Courses.Attach(model);
            db.Update(model);

            db.SaveChanges();
            return RedirectToAction("CategoryIndex");
        }
        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            var item = db.Category_Courses.Find(id);
            if (item != null)
            {
                /*var DeleteItem=db.Categories.Attach(item);*/
                db.Category_Courses.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion
        #region Course

        public IActionResult CourseIndex()
        {
            var usersWithRoles = db.Courses.Include(u => u.Category_Course).ToList();
            var course = db.Courses.ToList();
            return View(course);
        }
        public ActionResult AddCourse()
        {

            var phanLoaiSVList = db.Category_Courses.ToList();
            ViewBag.KhoaHocSVList = new SelectList(phanLoaiSVList, "Category_coures_id", "Category_name");
            return View();
        }
        [HttpPost]
        public IActionResult AddCourse(Course model)
        {
            if (model != null)
            {
                db.Courses.Add(model);
                db.SaveChanges();
                return RedirectToAction("CourseIndex");
            }
            return View();

        }
        public IActionResult EditCourse(int id)
        {
            var phanLoaiSVList = db.Category_Courses.ToList();
            ViewBag.KhoaHocSVList = new SelectList(phanLoaiSVList, "Category_coures_id", "Category_name");
            var item = db.Courses.Find(id);
            return View(item);
        }
        [HttpPost]
        public IActionResult EditCourse(Course model)
        {
            db.Courses.Attach(model);
            db.Update(model);

            db.SaveChanges();
            return RedirectToAction("CourseIndex");
        }
        [HttpPost]
        public ActionResult DeleteCourse(int id)
        {
            var item = db.Courses.Find(id);
            if (item != null)
            {
                /*var DeleteItem=db.Categories.Attach(item);*/
                db.Courses.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        #endregion
        #region Class_Role

        public IActionResult ClassRoleIndex()
        {

            var classRole = db.Class_Roles;
            return View(classRole);
        }
        public ActionResult AddClassRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddClassRole(Class_Role model)
        {
            if (model != null)
            {
                db.Class_Roles.Add(model);
                db.SaveChanges();
                return RedirectToAction("ClassRoleIndex");
            }
            return View();

        }
        public IActionResult EditClassRole(int id)
        {

            var item = db.Class_Roles.Find(id);
            return View(item);
        }
        [HttpPost]
        public IActionResult EditClassRole(Class_Role model)
        {
            db.Class_Roles.Attach(model);
            db.Update(model);

            db.SaveChanges();
            return RedirectToAction("ClassRoleIndex");
        }
        [HttpPost]
        public ActionResult DeleteClassRole(int id)
        {
            var item = db.Class_Roles.Find(id);
            if (item != null)
            {
                /*var DeleteItem=db.Categories.Attach(item);*/
                db.Class_Roles.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        #endregion
        #region StudentClass
        public IActionResult StudentClassIndex()
        {

           
            var studentclass = db.Student_Classes.
                Include(sc => sc.Class_Role).
                Include(sc => sc.Student_Course);
            return View(studentclass);
        }
        public ActionResult AddStudentClass()
        {
            var sv = db.Class_Roles.ToList();
            ViewBag.KhoaHocSVList = new SelectList(sv, "Class_Role_id", "Name");
            var st = db.Student_Courses.ToList();
            ViewBag.Student_Course_id = new SelectList(st, "Student_id", "Fullname");
            return View();
        }
        [HttpPost]
        public IActionResult AddStudentClass(Student_Class model)
        {
            if (model != null)
            {
                db.Student_Classes.Add(model);
                db.SaveChanges();
                return RedirectToAction("StudentClassIndex");
            }
            return View();

        }
        public IActionResult EditStudentClass(int id)
        {
            var sv = db.Class_Roles.ToList();
            ViewBag.KhoaHocSVList = new SelectList(sv, "Class_Role_id", "Name");
            var st = db.Student_Courses.ToList();
            ViewBag.Student_Course_id = new SelectList(st, "Student_id", "Fullname");
            var item = db.Student_Classes.Find(id);
            return View(item);
        }
        [HttpPost]
        public IActionResult EditStudentClass(Student_Course model)
        {
            db.Student_Courses.Attach(model);
            db.Update(model);

            db.SaveChanges();
            return RedirectToAction("StudentClassIndex");
        }
        #endregion
        #region TeacherClass

        public IActionResult TeacherClassIndex()
        {

            
            var teacherclass = db.Teacher_Classes.
                Include(sc => sc.Class_Role).
                Include(sc => sc.Teacher_Course);
            return View(teacherclass);
        }
        public ActionResult AddTeacherClass()
        {
            var sv = db.Class_Roles.ToList();
            ViewBag.KhoaHocSVList = new SelectList(sv, "Class_Role_id", "Name");
            var st = db.Teacher_Courses.ToList();
            ViewBag.Teacher_Coures_id = new SelectList(st, "Teacher_Coures_id", "Fullname");
            return View();
        }
        [HttpPost]
        public IActionResult AddTeacherClass(Teacher_Class model)
        {
            if (model != null)
            {
                db.Teacher_Classes.Add(model);
                db.SaveChanges();
                return RedirectToAction("TeacherClassIndex");
            }
            return View();

        }
        public IActionResult EditTeacherClass(int id)
        {
            var sv = db.Class_Roles.ToList();
            ViewBag.KhoaHocSVList = new SelectList(sv, "Class_Role_id", "Name");
            var st = db.Teacher_Courses.ToList();
            ViewBag.Teacher_Course_id = new SelectList(st, "Teacher_Coures_id", "Fullname");
            var item = db.Teacher_Classes.Find(id);
            return View(item);
        }
        [HttpPost]
        public IActionResult EditTeacherClass(Teacher_Class model)
        {
            db.Teacher_Classes.Attach(model);
            db.Update(model);

            db.SaveChanges();
            return RedirectToAction("TeacherClassIndex");
        }
        #endregion

    }
}


