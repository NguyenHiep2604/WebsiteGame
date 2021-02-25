﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGame.Models;

namespace WebGame.Controllers
{
    public class HomeController : Controller
    {
        //--Index--\\
        public ActionResult Index()
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listGameFeatured = from gameFeatured in entities.List_Game
                                       where gameFeatured.Featured_Games == true
                                       select gameFeatured;
                ViewBag.DataListGameFeatured = listGameFeatured.ToList();
            }
            return View();
        }
        //--Game--\\
        public ActionResult Games()
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listGameFeatured = from gameFeatured in entities.List_Game
                                       where gameFeatured.Featured_Games == true
                                       select gameFeatured;
                ViewBag.DataListGameFeatured = listGameFeatured.ToList();

                var listAllGame = from game in entities.List_Game
                                  select game;
                ViewBag.DataListAllGame = listAllGame.ToList();
            }
            return View();
        }
        //--About--\\
        public ActionResult About()
        {
            return View();
        }
        //--Jobs--\\
        public ActionResult Jobs()
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listJob = from job in entities.List_Jobs
                              select job;
                ViewBag.ListJob = listJob.ToList();

                //var listGroup = listJob.GroupBy(a => a.Team).Select(grp => grp.ToList()).ToList();

                var listGroup = from p in entities.List_Jobs
                                group p by p.Team into g
                                select new GroupJob {
                                    GroupName = g.Key,
                                    Jobs = (from i in g.ToList()
                                            select new Job_Model { 
                                            ID = i.ID,
                                            Description = i.Description,
                                            Location = i.Location,
                                            Team = i.Team,
                                            Vacancies = i.Vacancies
                                            }).ToList()
                                };

                //var groupJob = listGroup.GroupBy(group => group.Group).Select(a => a.ToList());
                ViewBag.GroupJob = listGroup.ToList();

                var listJobsAnalytics = from jobAnalytics in entities.List_Jobs
                                        where jobAnalytics.Team == "Analytics"
                                        select jobAnalytics;
                ViewBag.ListJobsAnalytics = listJobsAnalytics.ToList();

                var listBusinessDev = from jobBusiness in entities.List_Jobs
                                      where jobBusiness.Team == "Business Development"
                                      select jobBusiness;
                ViewBag.ListJobsBusinessDev = listBusinessDev.ToList();
            }
            return View();
        }
        //--Case--\\
        public ActionResult Case()
        {
            return View();
        }
        //--FAQ--\\
        public ActionResult FAQ()
        {
            return View();
        }
        //--Game Submissions--\\
        public ActionResult GameSubmissions()
        {
            return View();
        }
        //--Contact--\\
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //--Game Submit--\\
        GameSubmit_DB gameSubmit_DB = new GameSubmit_DB();
        public ActionResult SubmitGame()
        {
            GameSubmit_Model gameSubmit_Model = new GameSubmit_Model();
            gameSubmit_Model.FullName = Request["FullName"];
            gameSubmit_Model.Email = Request["Email"];
            gameSubmit_Model.GameTitle = Request["Title"];
            gameSubmit_Model.VideoFootageLink = Request["FootageLink"];
            gameSubmit_Model.CompanyName = Request["CompanyName"];
            gameSubmit_Model.Country = Request["Country"];
            gameSubmit_Model.LinkAppStore = Request["LinkStore"];
            gameSubmit_Model.MoreAbout = Request["MoreAbout"];
            gameSubmit_Model.DateSubmit = DateTime.Now;

            var game = gameSubmit_DB.GameSubmit(gameSubmit_Model);
            return Json(game, JsonRequestBehavior.AllowGet);
        }
        //--Details Description Job--\\
        public ActionResult DetailsJob(int id)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listJob = from job in entities.List_Jobs
                                     select job;
                ViewBag.Data = listJob.ToList();
                var Job = listJob.Where(b => b.ID == id).FirstOrDefault();
                ViewBag.ID = Job.ID;
                ViewBag.Team = Job.Team;
                ViewBag.Location = Job.Location;
                ViewBag.Vacancies = Job.Vacancies;
                ViewBag.Description = Job.Description;
            }
            return View();
        }
        //--Apply for job--\\
        public ActionResult ApplyJob(int id)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listJob = from job in entities.List_Jobs
                              select job;
                ViewBag.Data = listJob.ToList();
                var Job = listJob.Where(b => b.ID == id).FirstOrDefault();
                ViewBag.Team = Job.Team;
                ViewBag.Location = Job.Location;
                ViewBag.Vacancies = Job.Vacancies;
                ViewBag.Description = Job.Description;
            }
            return View();
        }
        //-----------\\
        //--Apply Job--\\
        //--Submit Apply Job--\\
        Apply_Job_DB apply_Job_DB = new Apply_Job_DB();
        public ActionResult SubmitApplyJob()
        {
            Apply_Job_Model apply_Job_Model = new Apply_Job_Model();
            apply_Job_Model.FullName = Request["FullName"];
            apply_Job_Model.Email = Request["Email"];
            apply_Job_Model.Phone = Request["Phone"];
            byte[] file = null;
            var fileSubmit = Request.Files[0];
            using (var binaryReader = new BinaryReader(fileSubmit.InputStream))
            {
                file = binaryReader.ReadBytes(fileSubmit.ContentLength);
            }
            apply_Job_Model.CV = file;
            apply_Job_Model.CurrentCompany = Request["CurrentCompany"];
            apply_Job_Model.LinkedInURL = Request["LinkedInURL"];
            apply_Job_Model.TwitterURL = Request["TwitterURL"];
            apply_Job_Model.GithubURL = Request["GithubURL"];
            apply_Job_Model.PortfolioURL = Request["PortfolioURL"];
            apply_Job_Model.OtherWebsite = Request["OtherWebsite"];
            apply_Job_Model.More = Request["More"];

            var submit = apply_Job_DB.SubmitApplyJob(apply_Job_Model);
            return Json(submit, JsonRequestBehavior.AllowGet);
        }
        //--View Image
        [HttpGet]
        public ActionResult ViewImage(int id)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var game = entities.List_Game.FirstOrDefault(select => select.ID == id);
                if (game != null)
                {
                    if (game.Image != null)
                    {
                        byte[] image = game.Image;
                        return File(image, "image/jpg", string.Format("{0}.jpg", id));
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}