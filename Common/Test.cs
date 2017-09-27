using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Test
    {

        public void RunTest()
        {
            for (int i = 0; i < 10; i++)
            {
                Random rand = new Random();
                int[] id = new int[] { 3, 4, 5 };
                int[] appId = new int[] { 3, 4, 5 };
                string[] log = new string[] { "new", "pending"};
                ErrorLoggerModel.LogStatus logStatus1 = new ErrorLoggerModel.LogStatus()
                {
                    logStatusID = rand.Next(1, 3)
                    //logStatusDesc = log[rand.Next(log.Length)]
                };

                ErrorLoggerModel.Users user1 = new ErrorLoggerModel.Users()
                {

                    userId = id[rand.Next(id.Length)]
                    //firstName = "John",
                    //lastName = "Doe"
                };

                ErrorLoggerModel.Application app1 = new ErrorLoggerModel.Application()
                {
                    appId = 6
                    //appName = "web-application",
                    //appType = "type_1",
                    //Errors = new List<ErrorLoggerModel.Errors>() { errorlog1 }
                };


                ErrorLoggerModel.Errors errorlog1 = new ErrorLoggerModel.Errors()
                {
                    Application = app1,
                    LogStatus = logStatus1,
                    Users = user1

                };
                RestInteraction.AddLog(errorlog1);
            }
        }
        static void Main(string[] args)
        {
            Common.Test test = new Common.Test();
            test.RunTest();
            

        }
    }
}
