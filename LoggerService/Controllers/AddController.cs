using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LoggerService.Controllers
{
    //public class AddController : ApiController
    //{
    //    private ErrorLoggerModel.Model1 db = new ErrorLoggerModel.Model1();
    //    [HttpPost]
    //    public IHttpActionResult PostLogDetails([FromBody] ErrorLoggerModel.Errors errorlog )
    //    {
    //        if(!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        db.Errors.Add(errorlog);
    //    }
    //}
    public class AddController : ApiController
    {
        [HttpPost]
        public String AddLog(ErrorLoggerModel.Errors errorLog)
        {
            LoaderandLogic.ErrorData newLoader = new LoaderandLogic.ErrorData();
            newLoader.AddError(errorLog);
            return "success";
        }

        
    }
}
