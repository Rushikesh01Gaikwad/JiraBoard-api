using Microsoft.AspNetCore.Mvc;
using JiraBoard_api.Modals;
using JiraBoard_api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using JiraBoard_api.Services;

namespace JiraBoard_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private returnData rtn = new returnData();
        private readonly DataContext _dataContext;
        private readonly JwtTokenService _jwtService;

        public LoginController(DataContext dataContext, JwtTokenService jwtTokenService)
        {
            _dataContext = dataContext;
            _jwtService = jwtTokenService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ObjectResult> Login(string email, string password)
        {
            try
            {
                var user = await _dataContext.users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                    {
                        var token = _jwtService.generateToken(user);
                            rtn.data = new
                            {
                                user = user,
                                token = token,
                            };

                            return Ok(rtn);
                    }
                    else
                    {
                        rtn.statusCd = 0;
                        rtn.message = "Invalid password";
                        return Ok(rtn);
                    }
                }
                else
                {
                    rtn.statusCd = 0;
                    rtn.message = "User not found";
                    return Ok(rtn);
                }
            }
            catch (Exception ex)
            {
                rtn.statusCd = 0;
                rtn.message = ex.Message;
                return Ok(rtn);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult RefreshToken([FromBody] TokenModel tokenModel)
        {

            string accessToken = tokenModel.AccessToken;
            string refreshToken = tokenModel.RefreshToken;

            if (tokenModel is null)
                return BadRequest("Invalid client request");

            var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
            var email = principal.Identity.Name;

            var user =  _dataContext.users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return BadRequest("Invalid refresh token");


            var newToken = _jwtService.generateToken(user);


            return Ok( new TokenModel
            {
                AccessToken = newToken.AccessToken,
                RefreshToken = newToken.RefreshToken,
                Expiration = newToken.Expiration,
            });
        }




    }
}
