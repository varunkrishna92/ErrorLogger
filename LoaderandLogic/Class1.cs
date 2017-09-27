using ErrorLoggerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace LoaderandLogic
{
    public class ErrorData
    {
        private static List<ErrorLoggerModel.Errors> errorData;

        public ErrorData()
        {            
            try
            {
                using (Model1 db = new Model1())
                {
                    db.Database.Initialize(false);
                    db.Errors.Count();
                }

                using (Model1 context = new Model1())
                {
                    errorData = context.Errors.Include(m => m.Application).ToList();
                    errorData = context.Errors.Include(m => m.LogStatus).ToList();
                    errorData = context.Errors.Include(m => m.Users).ToList();

                }
            }
            catch(Exception e)
            {
                string s = e.Message;
            }
        }

        public bool AddError(ErrorLoggerModel.Errors newError)
        {
            //errorData = new List<ErrorLoggerModel.ErrorLogger>();
            //List<string> err = new List<string>();
            using (Model1 context = new Model1())
            {
                Errors dummy = new Errors()
                {

                    //Application = newError.Application,
                    //LogStatus = newError.LogStatus,
                    //Users = newError.Users
                };
                //dummy.Courses.Add(context.Classes.First(x => x.ClassCode == "CSE581"));
                //if (newError.Application.appStatus == "active")
                //{
                    try
                    {
                        dummy.Application = context.Application.FirstOrDefault(x => x.appId == newError.Application.appId);
                        dummy.LogStatus = context.LogStatus.FirstOrDefault(x => x.logStatusID == newError.LogStatus.logStatusID);
                        dummy.Users = context.Users.FirstOrDefault(x => x.userId == newError.Users.userId);

                        context.Errors.Add(dummy);
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        string b = "a";
                    }
                //}
            }
            errorData.Add(newError);
            
            return true;
        }

        public ErrorLoggerModel.Errors getData(int id)
        {
            ErrorLoggerModel.Errors eData = null;
            if(errorData.Any(x => x.logID == id))
            {
                eData = errorData.Single(x => x.logID == id);
            }
            return eData;
        }

        public List<ErrorLoggerModel.Errors> GetAllErrors()
        {
            return errorData;
        }


    }
}
