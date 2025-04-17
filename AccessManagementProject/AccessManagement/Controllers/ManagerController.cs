using Microsoft.AspNetCore.Mvc;
using AccessManagement.Data;
using AccessManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AccessManagement.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly AppDBContext _dbContext;

        public ManagerController(AppDBContext cc)
        {
            this._dbContext = cc;
        }

        [HttpDelete]

        public async Task<ActionResult<User>> Deleteuser(string username)
        {
            var user= await _dbContext.user.FirstOrDefaultAsync(ele=> ele.username == username);

            if (user == null)
            {
                var res1 = new
                {
                    message = "user not exists"
                };

                return Conflict(res1);
            }


            _dbContext.user.Remove(user);
            await _dbContext.SaveChangesAsync();


            var res = new
            {
                message = "user deleted successfully"
            };

            return Ok(res);

        }
    }
}
