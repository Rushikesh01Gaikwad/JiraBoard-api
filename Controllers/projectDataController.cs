using JiraBoard_api.Context;
using JiraBoard_api.Modals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JiraBoard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class projectDataController : ControllerBase
    {
        private returnData rtn = new returnData();
        private readonly DataContext _dataContext;

        public projectDataController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]List<projectData> data)
        {
            try
            {
                if(data != null)
                {
                    _dataContext.AddRange(data);
                    await _dataContext.SaveChangesAsync();
                    rtn.data = data;
                    return Ok(rtn);

                }
                else
                {
                    rtn.statusCd = 0;
                    rtn.message = "Data is null here";
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
                var data = await _dataContext.projects.ToListAsync();
                if(data != null)
                {
                    rtn.data = data;
                    return Ok(rtn);
                }
                else
                {
                    rtn.statusCd = 0;
                    rtn.message = "Data is not present on the table";
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
                var data = await _dataContext.projects.FindAsync(id);
                if (data != null)
                {
                    rtn.data = data;
                    return Ok(rtn);
                }
                else
                {
                    rtn.statusCd = 0;
                    rtn.message = "Record is not present";
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
        public async Task<IActionResult> Update(int id, [FromBody] projectData data)
        {
            try
            {
                var existing = await _dataContext.projects.FindAsync(id);
                if (existing != null)
                {
                    _dataContext.Entry(existing).CurrentValues.SetValues(data);
                    await _dataContext.SaveChangesAsync();
                    rtn.data= data;
                    return Ok(rtn);
                }
                else
                {
                    rtn.statusCd = 0;
                    rtn.message = "Data is not present";
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete (int id)
        {
            try
            {
                var existing = await _dataContext.projects.FindAsync(id);
                if (existing != null)
                {
                    _dataContext.projects.Remove(existing);
                    await _dataContext.SaveChangesAsync();
                    return Ok(rtn);
                }
                else
                {
                    rtn.statusCd = 0;
                    rtn.message = "Id is not present";
                    return Ok(rtn);
                }
            }
            catch (Exception ex)
            {
                rtn.statusCd= 0;
                rtn.message = ex.Message;
                return Ok(rtn);
            }
        }

    }
}
