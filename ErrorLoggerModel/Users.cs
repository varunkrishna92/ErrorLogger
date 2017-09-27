using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLoggerModel
{
    public class Users
    {
        [Key]
        public int userId { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        public string lastLogin { get; set; }
        public string email { get; set; }
        public string status { get; set; }

       
        //public String role { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Errors> Errors { get; set; }
    }
}
