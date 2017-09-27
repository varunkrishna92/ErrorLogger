using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLoggerModel
{
    public class ErrorLogdbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<Model1>
    {
        protected override void Seed(Model1 context)
        {
            Console.WriteLine(" ### Seeding ###");

            LogStatus logStatus1 = new LogStatus()
            {
                logStatusID = Convert.ToInt32(1),
                logStatusDesc = "new"
            };

            LogStatus logStatus2 = new LogStatus()
            {
                logStatusID = Convert.ToInt32(2),
                logStatusDesc = "pending"
            };

            Users user1 = new Users
            {
                userId = 1,
                firstName = "Varun",
                lastName = "Krishna",
                //role = "admin"
            };

            Users user2 = new Users
            {
                userId = 2,
                firstName = "Peruru",
                lastName = "Vanaparthy"
            };

            Errors errorlog1 = new Errors()
            {
                logID = 1,
                LogStatus = logStatus1,
                Users = user1
                
            };

            Errors errorlog2 = new Errors()
            {
                logID = 2,
                LogStatus = logStatus2,
                Users = user2
            };

            Application app1 = new Application()
            {
                appId = 1,
                appName = "web-application",
                appType = "type_1",
                Errors = new List<Errors>() { errorlog1 }
            };
            Application app2 = new Application
            {
                appId = 2,
                appName = "db-application",
                appType = "type_2",
                Errors = new List<Errors>() { errorlog2 }
            };

            

            context.LogStatus.Add(logStatus1);
            context.LogStatus.Add(logStatus2);
           
            context.Users.Add(user1);
            context.Users.Add(user2);
            
            context.Errors.Add(errorlog1);
            context.Errors.Add(errorlog2);

            context.Application.Add(app1);
            context.Application.Add(app2);

            


            //context.

            // letting the base method do anything it needs to get done
            base.Seed(context);

            // Save the changes you made, when adding the data above
            try
            {
                context.SaveChanges();
            }
            catch(Exception e)
            {
                string s = "a";
            }
        }
    }
}
