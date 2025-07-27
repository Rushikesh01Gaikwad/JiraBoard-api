using Microsoft.AspNetCore.Mvc;
using JiraBoard_api.Modals;
using JiraBoard_api.Context;

namespace JiraBoard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private returnData rtn = new returnData();
        private readonly DataContext _dataContext;

        public LoginController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }




    }
}
