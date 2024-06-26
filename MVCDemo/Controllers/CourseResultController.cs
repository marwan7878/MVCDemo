﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCDemo.Models;
using MVCDemo.ViewModels;

namespace MVCDemo.Controllers
{
    public class CourseResultController : Controller
    {
        DBEntity context = new DBEntity();
        public IActionResult Index()
        {
            List<CourseResult> courseResults = context.CourseResult.Include(e => e.Course).
                Include(e => e.Trainee).ToList();
            
            HttpContext.Session.SetString("Name","Marooo");

            List<CourseResultViewModel> courseResultsVM = new List<CourseResultViewModel>();
            foreach (CourseResult courseResult in courseResults)
            {
                CourseResultViewModel temp = new CourseResultViewModel()
                {
                    Id = courseResult.Id,
                    Course_Name = courseResult.Course.Name,
                    Trainee_Name = courseResult.Trainee.Name,
                    Course_Degree = courseResult.Degree,
                };
                    if (courseResult.Degree > 40)
                        temp.Color = "red";
                    else
                        temp.Color = "yellow";
                courseResultsVM.Add(temp);
            }
            return View(courseResultsVM);
        }
        public IActionResult ShowResult(int tid , int cid)
        {
            return Content(HttpContext.Session.GetString("Name"));
        }

        public IActionResult Primitive(int age ,Department dept, string name, string[] colors)
        {
            return Content("age and name");
        
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save(Department dept)
        {
            if (dept.Name != null)
            {
                dept.Location = "Cairo";
                context.Department.Add(dept);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create",dept);
        }
        [HttpGet]
        public ActionResult Method1()
        {
            return Content("M1");
        }
        [HttpPost]
        public ActionResult Method1(int id)
        {
            return Content("M11111");
        }
    }
}
