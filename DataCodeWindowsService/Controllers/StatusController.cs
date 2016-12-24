using System.Collections.Generic;
using System.Web.Http;

namespace DataCodeWindowsService
{
    public class StatusController : ApiController
    {
        //static-field which store service status
        //if service is started then State = true;
        public static bool State { get; set; } = false;

        [HttpGet]
        public bool CheckStatus()
        {
            return State;
        }
    }
}