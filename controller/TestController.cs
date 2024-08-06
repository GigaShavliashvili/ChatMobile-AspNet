using chatmobile.DB;
using chatmobile.repositories;
using Microsoft.AspNetCore.Mvc;

namespace chatmobile.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IUnitOfWork dbContext) : Controller
    {
        private readonly IUnitOfWork _dbContext = dbContext;

        [HttpGet("test")]
        public async Task<IActionResult> Test(CancellationToken cancellationToken)
        {
            var newUser = new entites.User
            {
                CreateDate = DateTime.Now,
                IsVerify = true,
                Password = "admin123",
                UserName = "admin123",
                VerificationDate = DateTime.Now,
            };
            _dbContext.User.Add(newUser);
            await _dbContext.CompleteAsync(cancellationToken);
            return Ok(newUser);
        }
    }
}