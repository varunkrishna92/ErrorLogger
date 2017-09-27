//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace ErrorLoggerModel
{
    using System.ComponentModel.DataAnnotations;

    public class ErrorLogger
    {
        [Required]
        public int logId { get; set; }
        public int appId { get; set; }
        public int user { get; set; }
        //public string assignedBy { get; set; }
        //public string desc { get; set; }
        public int logStatus { get; set; }


       // public List<string> err = new List<string>();
        //public Dictionary<int, List<string>> errorData;

    }
}
