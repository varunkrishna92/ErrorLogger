using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLoggerModel
{
    public class LogStatus
    {
        [Key]
        public int logStatusID { get; set; }

        [Required, MaxLength(100)]
        public string logStatusDesc { get; set; }

        public virtual ICollection<Errors> Errors { get; set; }
    }
}
