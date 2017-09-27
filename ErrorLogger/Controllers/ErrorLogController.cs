using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErrorLoggerModel;
using Microsoft.AspNet.Identity;
using PagedList;
using ErrorLogger.ChartsHelper;

namespace ErrorLogger.Controllers
{
    using ErrorLogger.Models;
    using System.Threading.Tasks;

    public class ErrorLogController : Controller
    {
        ICharts _ICharts;

        public ErrorLogController()
        {
            _ICharts = new Charts();
        }
        // GET: ErrorLog
        [Authorize]
        public ActionResult Index()
        {
            LoaderandLogic.ErrorData dataSource = new LoaderandLogic.ErrorData();
            ICollection<ErrorLoggerModel.Errors> data = dataSource.GetAllErrors();
            return View(data);
        }

        
        [HttpPost]
      public ActionResult SelectAction()
        {

            return View();
        }

        public ActionResult ViewApplications()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            Model1 context = new Model1();
            string id = User.Identity.GetUserId();
            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            var appUser = context.Users.FirstOrDefault(x => x.email == user.Email);
            //var apps = context.Application.Select(x => x.Users.Contains(appUser)).ToList();
            List<ErrorLoggerModel.Application> userApps = new List<Application>();
            LoaderandLogic.ApplicationDataHandler newLoader = new LoaderandLogic.ApplicationDataHandler();
            foreach (var i in appUser.Applications)
            {
                userApps.Add(newLoader.getData(i.appId));
            }
            return View(userApps);
        }

        [HttpGet]
        public ActionResult ErrorLog()
        {
            //ErrorLoggerModel.ErrorLogger.bugId = new ErrorLoggerModel.ErrorLogger.bugId();
            ErrorLoggerModel.Errors data = new ErrorLoggerModel.Errors() { };
            List<SelectListItem> AppNames = new List<SelectListItem>();
            using (Model1 context = new Model1())
            {
                
                foreach (Application application in context.Application.ToList())
                {
                    AppNames.Add(new SelectListItem { Text = application.appName, Value = application.appName });
                }

            }
            ViewData["appName"] = AppNames;
            //data.bugType = "UI";
            //data.bugId = 2;
            //data.desc = "Error";
            //List<string> newList = new List<string>;
            //newList.Add(data.bugType);

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ErrorLog(ErrorLoggerModel.Errors errorData)
        {
            LoaderandLogic.ErrorData newLoader = new LoaderandLogic.ErrorData();
            newLoader.AddError(errorData);
            return RedirectToAction("ErrorLog");
        }

        
        public ActionResult ViewDetails(int bugId)
        {
            int id = bugId;
            LoaderandLogic.ErrorData newLoader = new LoaderandLogic.ErrorData();
            ErrorLoggerModel.Errors data = newLoader.getData(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult ViewApplications(string searchString, string sortOrder)
        {
            Model1 dbContext = new Model1();

            var applications = from s in dbContext.Application
                               select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                applications = applications.Where(s => s.appName.Contains(searchString));
            }
            
            return View(applications.ToList());            
        }

        [Authorize]
        public ActionResult ViewLogs(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.StatusSortParm = String.IsNullOrEmpty(sortOrder) ? "status" : "status_asc";
            ViewBag.AppSortParm = String.IsNullOrEmpty(sortOrder) ? "app" : "app_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            
            if(sortOrder == null)
            {
                sortOrder = "name_desc";
            }

            ApplicationDbContext dbContext = new ApplicationDbContext();
            Model1 context = new Model1();
            
            //Get current user
            string id = User.Identity.GetUserId();
            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            var appUser = context.Users.FirstOrDefault(x => x.email == user.Email);
            var errors = from s in context.Errors where (s.Users.email == appUser.email) select s;
            switch(sortOrder)
            {
                case "name_desc":
                    errors = errors.OrderByDescending(s => s.logID);
                    break;
                case "status":
                     errors = errors.OrderByDescending(s => s.LogStatus.logStatusDesc);
                    break;
                case "app":
                     errors = errors.OrderByDescending(s => s.Application.appName);
                    break;
                case "status_asc":
                    errors = errors.OrderBy(s => s.LogStatus.logStatusDesc);
                    break;
                case "app_asc":
                    errors = errors.OrderBy(s => s.Application.appName);
                    break;
                default:
                    errors = errors.OrderBy(s => s.logID);
                    break;
            }
            //if (sortOrder == "name_desc")
            //    errors = errors.OrderByDescending(s => s.logID);
            //errors = errors.OrderByDescending(s => s.logID);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(errors.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ApplicationDetail(string sortOrder, string currentFilter, string searchString, int? appId, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ApplicationDbContext dbContext = new ApplicationDbContext();
            Model1 context = new Model1();
            string id = User.Identity.GetUserId();
            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            var appUser = context.Users.FirstOrDefault(x => x.email == user.Email);
            //var application = context.Application.FirstOrDefault(x => x.appId == appId);
            var errors = from s in context.Errors where (s.Application.appId == appId)  select s;
            if (sortOrder == "name_desc")
                errors = errors.OrderByDescending(s => s.logID);
            errors = errors.OrderByDescending(s => s.logID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(errors.ToPagedList(pageNumber, pageSize));
        }

       public ActionResult ViewLogsGraphical()
        {
           try
            {
                string appCount = string.Empty;
                string errorCount = string.Empty;
                _ICharts.applicationErrors(out appCount, out errorCount);
                ViewBag.appCount_List = appCount;
                ViewBag.errorCount_List = errorCount.Trim();
            }
            catch(Exception e)
            {
                string a = e.Message;
            }            
            return View();
        }
    }

    

}