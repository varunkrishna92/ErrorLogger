using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ErrorLoggerModel
{
    public class Application
    {
        [Key]
        public int appId { get; set; }

        [Required, MaxLength(50)]
        public string appName { get; set; }

        [Required, MaxLength(50)]
        public string appType { get; set; }

        public string appStatus { get; set; }

        /// <summary>
        /// !!!!! If you do not make this virtual, navigational properties will NOT work
        /// </summary>
        public virtual ICollection<Errors> Errors { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
