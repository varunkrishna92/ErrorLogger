using ErrorLoggerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErrorLogger.ChartsHelper
{
    public interface ICharts
    {
        void applicationErrors(out string appCount, out string errorCount);
    }
}