using Microsoft.AspNetCore.Mvc;
using JiraBoard_api.Modals;
using JiraBoard_api.Context;
using Microsoft.EntityFrameworkCore;

namespace JiraBoard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private returnData rtn = new returnData();
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] User user)
        {
            try
            {
                if (user != null)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    _dataContext.Add(user);
                    await _dataContext.SaveChangesAsync();
                    rtn.data = user;
                    return Ok(rtn);
                }
                else
                {
                    rtn.statusCd = 0;
                    rtn.message = "User data is null";
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _dataContext.users.ToListAsync();
                if (users != null)
                {
                    rtn.data = users;
                    return Ok(rtn);
                }
                else
                {
                    rtn.statusCd = 0;
                    rtn.message = "No users found";
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _dataContext.users.FindAsync(id);
                if (user != null)
                {
                    rtn.data = user;
                    return Ok(rtn);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    rtn.statusCd = 0;
                    rtn.message = "Invalid user data";
                    return Ok(rtn);
                }
                var existingUser = await _dataContext.users.FindAsync(id);
                if (existingUser == null)
                {
                    rtn.statusCd = 0;
                    rtn.message = "User not found";
                    return Ok(rtn);
                }
                _dataContext.Entry(existingUser).CurrentValues.SetValues(user);
                await _dataContext.SaveChangesAsync();
                rtn.data = existingUser;
                return Ok(rtn);
            }
            catch (Exception ex)
            {
                rtn.statusCd = 0;
                rtn.message = ex.Message;
                return Ok(rtn);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _dataContext.users.FindAsync(id);
                if (user == null)
                {
                    rtn.statusCd = 0;
                    rtn.message = "User not found";
                    return Ok(rtn);
                }
                _dataContext.users.Remove(user);
                await _dataContext.SaveChangesAsync();
                rtn.data = user;
                return Ok(rtn);
            }
            catch (Exception ex)
            {
                rtn.statusCd = 0;
                rtn.message = ex.Message;
                return Ok(rtn);
            }
        }
    }

}
