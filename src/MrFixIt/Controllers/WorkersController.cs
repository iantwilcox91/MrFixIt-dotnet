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
    public class WorkersController : Controller
    {
        private MrFixItContext db = new MrFixItContext();
        // GET: /<controller>/
        public IActionResult Index()
        {
            var thisWorker = db.Workers.Include(i =>i.Jobs).FirstOrDefault(i => i.UserName == User.Identity.Name);
            if (thisWorker != null)
            {
                return View(thisWorker);
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Worker worker)
        {
            worker.UserName = User.Identity.Name;
            db.Workers.Add(worker); 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult MarkAsActive(int jobid, string title)
        {
            var worker = db.Workers.Include(i => i.Jobs).FirstOrDefault(i => i.UserName == User.Identity.Name);
            foreach (var job in worker.Jobs)
            {
                if (job.JobId == jobid)
                {
                    job.Active = true;
                }
                else
                {
                    job.Active = false;
                }
            }
            db.SaveChanges();
            return Content("Your active job has been updated to: "+ title, "text/plain");
        }

        public IActionResult MarkAsCompleated(int jobid)
        {
            var job = db.Jobs.FirstOrDefault(j => j.JobId == jobid);
            job.Completed = true;
            db.Entry(job).State = EntityState.Modified;
            db.SaveChanges();
            return Content(" ", "text/plain");
        }



    }
}
