using ErrorLoggerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LoaderandLogic
{
    public class ApplicationDataHandler
    {
        private static List<ErrorLoggerModel.Application> applicationData;

        public ApplicationDataHandler()
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    db.Database.Initialize(false);
                    db.Application.Count();
                }

                using (Model1 context = new Model1())
                {
                    applicationData = context.Application.Include(m => m.Errors).ToList();
                    applicationData = context.Application.Include(m => m.Users).ToList();

                }
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
        }

        public bool AddApplication(ErrorLoggerModel.Application newApplication)
        {
            using (Model1 context = new Model1())
            {
                Application dummy = new Application()
                {

                    //Application = newError.Application,
                    //LogStatus = newError.LogStatus,
                    //Users = newError.Users
                };
                
                try
                {
                    context.Application.Add(newApplication);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    string b = "a";
                }
            }
            //applicationData.Add(newApplication);

            return true;
        }

        public ErrorLoggerModel.Application getData(int id)
        {
            ErrorLoggerModel.Application aData = null;
            if (applicationData.Any(x => x.appId == id))
            {
                aData = applicationData.Single(x => x.appId == id);
            }
            return aData;
        }

        public List<ErrorLoggerModel.Application> getAllapps()
        {
            return applicationData;
        }

        //public ICollection<ErrorLoggerModel.Errors> GetAllErrors()
        //{
        //    return errorData;
        //}


    }
}
