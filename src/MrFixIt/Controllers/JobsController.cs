﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrFixIt.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MrFixIt.Controllers
{
    public class JobsController : Controller
    {
        //define db for this context
        private MrFixItContext db = new MrFixItContext();

        // GET: /<controller>/
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

            return View(db.Jobs.Include(i => i.Worker).ToList());
            }else
            {
                return RedirectToAction("Public");
            }
        }

        public IActionResult Public()
        {
            return View(db.Jobs.Include(i => i.Worker).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Job job)
        {
            db.Jobs.Add(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //route to the claim page and see the job u want to claim
        //public IActionResult Claim(int id)
        //{
        //    var thisItem = db.Jobs.FirstOrDefault(items => items.JobId == id);
        //    return View(thisItem);
        //}

        //[HttpPost]
        public IActionResult Claim(int jobId, string userName)
        {
            var job = db.Jobs.FirstOrDefault(items => items.JobId == jobId);
            job.Worker = db.Workers.FirstOrDefault(i => i.UserName == userName);

            db.Entry(job).State = EntityState.Modified;
            db.SaveChanges();

            return Content("You've claimed this job", "text/plain");
        }
    }
}
