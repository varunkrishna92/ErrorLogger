using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLoggerModel.Models
{
    public class DBHandler
    {
        public static void CreateDB()
        {
            Console.WriteLine("~~~~ Creating the DB ~~~~");
            Console.WriteLine();

            using (Model1 db = new Model1())
            {
                // Initialize the DB - false doesn't force reinitialization if the DB already exists
                db.Database.Initialize(false);

                // Seeding runs the first time you try to use the DB, so we make it seed here..
                // It only runs IF the initializer condition is met, regardless of the True/False above
                db.Errors.Count();
            }
        }

        public static void DeleteDB()
        {
            Console.WriteLine("~~~~ Deleting the DB ~~~~");
            Console.WriteLine();

            using (Model1 db = new Model1())
            {
                if (db.Database.Exists())
                {
                    db.Database.Delete();
                }
            }
        }

        public static string SPACE = "   ";

        /// <summary>
        /// Gets the data grouped by Courses
        /// </summary>
        public static void PullOutDataByApplication()
        {
            Console.WriteLine();
            Console.WriteLine("~~~~ Data in the DB (by Application): ~~~~");
            Console.WriteLine();

            // the using statement will make sure the object is disposed when it goes out of scope
            using (Model1 context = new Model1())
            {
                foreach (Application application in context.Application.ToList())
                {
                    Console.WriteLine("\tApplication ID: {0}, Application Name: {1}, Application Type: {2}",
                    application.appId, application.appName, application.appType);

                    Console.WriteLine("\t Related Error Logs: ");

                    foreach (Errors log in application.Errors)
                    {
                        Console.WriteLine("\t Log Id: {0}, Name: {1}",
                            log.logID, log.LogStatus.logStatusID);
                        Console.WriteLine("\n");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the data grouped by Applications
        /// </summary>
        public static void PullOutDataByErrorLog()
        {
            Console.WriteLine();
            Console.WriteLine("~~~~ Data in the DB (by error-log file): ~~~~");
            Console.WriteLine();

            // the using statement will make sure the object is disposed when it goes out of scope
            using (Model1 context = new Model1())
            {
                foreach (Errors errorlog in context.Errors.ToList())
                {
                    Console.WriteLine("\tErrorLog Id: {0}", errorlog.logID);
                    //Console.WriteLine("\t  App ID: {0}", errorlog.appId);
                    //Console.WriteLine("\t  Log Status: {0}", errorlog.logStatusID);
                    Console.WriteLine("\t  source_application: {0}", errorlog.Application.appId);
                    Console.WriteLine("\n");
                }

                               
            }
        }

        public static void PullOutDataByLogStatus()
        {
            Console.WriteLine();
            Console.WriteLine("~~~~ Data in the DB (by error-log file): ~~~~");
            Console.WriteLine();

            // the using statement will make sure the object is disposed when it goes out of scope
            using (Model1 context = new Model1())
            {
                foreach (LogStatus stat in context.LogStatus.ToList())
                {
                    Console.WriteLine("\tLog status Id: " + stat.logStatusID);
                    Console.WriteLine("\tLog status description: " + stat.logStatusDesc);
                }
            }
        }

        /// <summary>
        /// Inserts a dummy student & repulls the data
        /// </summary>
        public static void InsertApplication()
        {
            Console.WriteLine();
            Console.WriteLine("~~~~ Inserting a demo application ~~~~");

            // the using statement will make sure the object is disposed when it goes out of scope
            using (Model1 context = new Model1())
            {
                Application newApp = new Application()
                {
                    appName = "test-app",
                    appType = "test"
                };

                //newApp.ErrorLogs = new List<ErrorLog>();
                //newApp.ErrorLogs.Add(context.ErrorLogs.First(x => x.logID == 1));

                context.Application.Add(newApp);
                context.SaveChanges();
            }

            PullOutDataByApplication();
        }

        /// <summary>
        /// Deletes the dummy student & repulls the data
        /// </summary>
        public static void DeleteApplication()
        {
            Console.WriteLine();
            Console.WriteLine("~~~~ Deleting the demo application ~~~~");

            // the using statement will make sure the object is disposed when it goes out of scope
            using (Model1 context = new Model1())
            {
                Application app = context.Application.First(x => x.appName == "test-app");

                context.Application.Remove(app);
                context.SaveChanges();
            }

            PullOutDataByApplication();
        }
    }
}
