﻿using kdh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CaptchaMvc.HtmlHelpers;

namespace kdh.Controllers
{
    public class JobApplicantController : Controller
    {
        HospitalContext db = new HospitalContext();
        // Only display view for admin
        // GET: JobApplicant
        [Authorize(Roles = "hr")]
        public ActionResult Index_Admin()
        {
            try
            {
                var applicants = db.Applicants.Include(a => a.Job);
                List<Applicant> applicant = db.Applicants.ToList();
                return View(applicant);
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: new applicant
        [HttpGet]
        public ActionResult Create(string job_id)
        {
            try
            {
                ViewBag.jobs = db.Jobs.ToList();
                return View();
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        // POST: new applicant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicantId,FirstName,LastName,DateOfBirth,Email,Phone,JobId")] Applicant applicant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (this.IsCaptchaValid("Validate your captcha"))
                    //{
                        db.Applicants.Add(applicant);
                        db.SaveChanges();
                        ViewBag.SuccessMessage = "Application was successfully submitted.";
                        return RedirectToAction("Index", "Job");
                    //}
                    //ViewBag.ErrorCaptcha = "Captcha is not valid.";
                    //return View(applicant);
                }
                ViewBag.jobs = db.Jobs.ToList();
                return View(applicant);
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlException = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        // Neither admins or applicants can edit applications

        //GET application
        [Authorize(Roles = "hr")]
        //[HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                Applicant applicant = db.Applicants.SingleOrDefault(model => model.ApplicantId == id);
                if (id == null)
                {
                    return RedirectToAction("Index_Admin");
                }
                return View(applicant);
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlException = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        //POST delete applicant
        [Authorize(Roles = "hr")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Applicant applicant = db.Applicants.Find(id);
                db.Applicants.Remove(applicant);
                db.SaveChanges();
                return RedirectToAction("Index_Admin");
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlException = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
    }
}