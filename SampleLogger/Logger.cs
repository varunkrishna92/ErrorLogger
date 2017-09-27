/*

This is a dumb example to make the logger harness work.. This should be removed... 

*/


namespace SampleLogger
{
    using System;

    public class Logger
    {
        /// <summary>
        /// Constructor. Put whatever initialization code in here that you need
        /// </summary>
        public Logger()
        {
        }

        /// <summary>
        /// This method is called by the test harness. So inside of it you should call your logger..
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="logLevel">Error Log Level</param>
        /// <param name="ex">Optional Exception</param>
        public void Log()
        {
            // this is a stub to allow us to do mean things....
            Common.Test test = new Common.Test();
            test.RunTest();
            //Console.WriteLine(errorMessage);
        }
    }
}
