using ErrorLoggerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace LoaderandLogic
{
    public class UserDataHandler
    {
        private static List<ErrorLoggerModel.Users> userData;
        Model1 context = new Model1();

        public UserDataHandler()
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    db.Database.Initialize(false);
                    db.Users.Count();
                }

                using (Model1 context = new Model1())
                {
                    userData = context.Users.Include(m => m.Errors).ToList();                    
                }
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
        }

        public bool AddUser(ErrorLoggerModel.Users newUser)
        {
           using (Model1 context = new Model1())
            {
                Users dummy = new Users()
                {

                    //Application = newError.Application,
                    //LogStatus = newError.LogStatus,
                    //Users = newError.Users
                };
                //dummy.Courses.Add(context.Classes.First(x => x.ClassCode == "CSE581"));
                try
                {
                    newUser.status = "active";
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    string b = "a";
                }
            }
            userData.Add(newUser);

            return true;
        }
//        var entry = context.Users.SingleOrDefault(u => u.emailID == emailID);
//                if (entry != null)
//                {
//                    try
//                    {
//                        entry.lastLoginDate = DateTime.Now;
//                        entry.userType = entry.userType;
//                        context.SaveChanges();

//                        result = true;
//                    }

//                    catch (DbEntityValidationException dbEx)
//                    {
//                        foreach (var validationErrors in dbEx.EntityValidationErrors)
//                        {
//                            foreach (var validationError in validationErrors.ValidationErrors)
//                            {
//                                string property = validationError.PropertyName;
//    string msg = validationError.ErrorMessage;
//    //System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
//}
//                        }
//                    }
//                }
        public bool editLogin(string time, string mail)
        {
            try
            {
                using (Model1 context = new Model1())
                {
                    ErrorLoggerModel.Users uData = null;
                    if (userData.Any(x => x.email == mail))
                    {
                        uData = context.Users.FirstOrDefault(x => x.email == mail);
                        //uData.Applications = uData.Applications;
                        uData.lastLogin = time;
                        context.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string property = validationError.PropertyName;
                        string msg = validationError.ErrorMessage;
                        //System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        
            return true;
        }

        //public ErrorLoggerModel.Users getData(int id)
        //{
        //    ErrorLoggerModel.Users uData = null;
        //    if (userData.Any(x => x.logID == id))
        //    {
        //        eData = errorData.Single(x => x.logID == id);
        //    }
        //    return eData;
        //}

        public List<ErrorLoggerModel.Errors> GetAllErrors(string email)
        {
            var user = context.Users.FirstOrDefault(x => x.email == email);
            return user.Errors.ToList();
        }


    }
}
