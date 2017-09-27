using ErrorLoggerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErrorLogger.ChartsHelper
{
    public class Charts : ICharts
    {
        public void applicationErrors(out string appCount, out string errorCount)
        {
            Model1 context = new Model1();
            List<string> apps = new List<string>();
            var applications = context.Application.Select(x => x.appName).ToList();
            foreach (string item in applications)
            {
                apps.Add("\"" + item + "\"");
            }
            var errors = (from s in context.Application select s.Errors.Count).ToList();
            appCount = string.Join(",", apps);
            errorCount = string.Join(",", errors);    
        }
    }
}