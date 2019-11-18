using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BoletoGen2.Controllers
{
    public class BoletoGenController : ApiController
    {
        private BoletoGenService _boletoGenService = new BoletoGenService();
        public BoletoGenController() { }
        
        // GET: api/BoletoGen
        [HttpGet]
        public JsonResult<string> Get(Guid instid,Guid userid)
        {
            var ret = _boletoGenService.BoletoGenGet(instid, userid);
            return Json(ret);
        }
    }
}
