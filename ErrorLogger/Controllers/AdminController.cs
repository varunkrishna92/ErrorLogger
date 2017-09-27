using ErrorLogger.Models;
using ErrorLoggerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErrorLogger.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext context;
        static string xString;
        public AdminController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminAppData()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditApplication()
        {
            var allUsers = context.Users.Select(x => new
            {
                userId = x.Email,
                firstName = x.firstName
            }).ToList();
            ViewBag.allUsers = new MultiSelectList(allUsers, "userId", "firstName");

            //}
            LoaderandLogic.ApplicationDataHandler newLoader = new LoaderandLogic.ApplicationDataHandler();
            List<ErrorLoggerModel.Application> appData = newLoader.getAllapps();
            return View(appData);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditApplication(string searchstring, string userId)
        {
            var allUsers = context.Users.Select(x => new
            {
                userId = x.Email,
                firstName = x.firstName
            }).ToList();
            ViewBag.allUsers = new MultiSelectList(allUsers, "userId", "firstName");

            Model1 dbContext = new Model1();
            
            var applications = from s in dbContext.Application
                                   select s;
            if (!String.IsNullOrEmpty(searchstring))
               {
                    applications = applications.Where(s => s.appName.Contains(searchstring));
                }
            xString = searchstring;
            return View(applications.ToList());
            
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminApplicationDetail(int appId)
        {
            Model1 dbContext = new Model1();
            var allUsers = context.Users.Select(x => new
            {
                userId = x.Email,
                firstName = x.firstName
            }).ToList();
            ViewBag.allUsers = new MultiSelectList(allUsers, "userId", "firstName");
            LoaderandLogic.ApplicationDataHandler newLoader = new LoaderandLogic.ApplicationDataHandler();
            ErrorLoggerModel.Application app = newLoader.getData(appId);
            return View(app);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminApplicationDetail(Application app, string userId)
        {
            Model1 dbContext = new Model1();
            var allUsers = context.Users.Select(x => new
            {
                userId = x.Email,
                firstName = x.firstName
            }).ToList();
            ViewBag.allUsers = new MultiSelectList(allUsers, "userId", "firstName");
            //List<Users> userList = new List<Users>();
            var user = dbContext.Users.FirstOrDefault(x => x.email == userId);

            var application = dbContext.Application.FirstOrDefault(x => x.appId == app.appId);
            application.Users.Add(user);
            dbContext.SaveChanges();
            return RedirectToAction("EditApplication");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteApplication(Application application)
        {
            Model1 dbContext = new Model1();
            var app = dbContext.Application.FirstOrDefault(x => x.appName == application.appName);
            try
            {
                dbContext.Application.Remove(app);
            }
            catch(Exception e)
            {

            }
            dbContext.SaveChanges();
            return RedirectToAction("EditApplication");
        }

        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public ActionResult UserPool()
        {
            //var context = new ApplicationDbContext();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var activeUsers = context.Users.Where(x => x.LockoutEndDateUtc.Value == null).Select(x => new
                {
                    userId = x.Email,
                    firstName = x.firstName
                }).ToList();
                ViewBag.activeUsers = new MultiSelectList(activeUsers, "userID", "firstName");

                var inactiveUsers = context.Users.Where(x => x.LockoutEndDateUtc.Value != null).Select(x => new
                {
                    userId = x.Email,
                    firstName = x.firstName
                }).ToList();
                ViewBag.inactiveUsers = new MultiSelectList(inactiveUsers, "userID", "firstName");
            }
            Model1 dbcontext = new Model1();
            var users = dbcontext.Users.Select(x => x);
            return View(users.ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]        
        public ActionResult UserPoolDeact(string[] userID)
        {
            Model1 dbContext = new Model1();
            //var context = new ApplicationDbContext();
            foreach (var i in userID)
            {
                var user = context.Users.FirstOrDefault(x => x.Email == i);
                if (user.LockoutEnabled)
                {
                    user.LockoutEndDateUtc = DateTime.MaxValue;
                    var appUser = dbContext.Users.FirstOrDefault(x => x.email == user.Email);
                    appUser.status = "inactive";
                }

             }
            dbContext.SaveChanges();
            context.SaveChanges();
            
            return RedirectToAction("UserPool");
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UserPoolAct(string[] userID)
        {
            Model1 dbContext = new Model1();

            foreach (var i in userID)
                {
                    var user = context.Users.FirstOrDefault(x => x.Email == i);
                    user.LockoutEndDateUtc = null;
                var appUser = dbContext.Users.FirstOrDefault(x => x.email == user.Email);
                appUser.status = "active";

            }
            dbContext.SaveChanges();
            context.SaveChanges();

            return RedirectToAction("UserPool");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddApplication(ErrorLoggerModel.Application appData, string[] userId)
        {
            List<Users> userList = new List<Users>();
            using (Model1 model = new Model1())
            {
                var users = context.Users.Select(x => new
                {
                    userId = x.Id,
                    firstName = x.firstName
                }).ToList();
                ViewBag.Users = new MultiSelectList(users, "userID", "firstName");

                if (userId == null)
                {
                    var user = context.Users.FirstOrDefault(x => x.firstName == "Chris");
                    Users appUser = model.Users.FirstOrDefault(x => x.email == user.Email);
                    userList.Add(appUser);
                }
                else
                {
                    foreach (var i in userId)
                    {
                        var user = context.Users.FirstOrDefault(x => x.Id == i.ToString());
                        Users appUser = model.Users.FirstOrDefault(x => x.email == user.Email);
                        userList.Add(appUser);
                    }
                }
                appData.appStatus = "active";
                appData.Users = userList;
                model.Application.Add(appData);
                model.SaveChanges();
            
            }
            

            //LoaderandLogic.ApplicationDataHandler newLoader = new LoaderandLogic.ApplicationDataHandler();
            //newLoader.AddApplication(appData);
            return RedirectToAction("EditApplication");
        }

        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public ActionResult AddApplication()
        {
            //ErrorLoggerModel.Application app = new Application();
            using (Model1 model = new Model1())
            {
                var users = context.Users.Select(x => new
                {
                    userId = x.Id,
                    firstName = x.firstName
                }).ToList();
                ViewBag.Users = new MultiSelectList(users, "userID", "firstName");
            }
            
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewApplications(int appId)
        {            
            int id = appId;
            LoaderandLogic.ApplicationDataHandler newLoader = new LoaderandLogic.ApplicationDataHandler();
            ErrorLoggerModel.Application data = newLoader.getData(id);
            return View(data);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ToggleApplication(Application app)
        {
            Model1 dbContext = new Model1();
            Application application = dbContext.Application.FirstOrDefault(x => x.appId == app.appId);
            if (application.appStatus == "active")
            {
                application.appStatus = "inactive";
            }
                
            else
            {
                application.appStatus = "active";
            }
            dbContext.SaveChanges();
            return RedirectToAction("EditApplication");
        }

        //public ActionResult AdminApplicationDetail(int appId)
        //{
        //    Model1 context = new Model1();
        //    var application = context.Application.FirstOrDefault(x => x.appId == appId); 
        //    return View(application);
        //}
     }
}