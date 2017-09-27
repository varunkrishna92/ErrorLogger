using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLoggerModel
{
    public class Errors
    {
        [Key]
        public int logID { get; set; }

        [Required]
        public virtual Application Application { get; set; }
        //[Required]
        public virtual LogStatus LogStatus { get; set; }
        //[Required]
        public virtual Users Users { get; set; }
    }
}
