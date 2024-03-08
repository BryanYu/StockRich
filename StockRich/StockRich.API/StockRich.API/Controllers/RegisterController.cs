using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockRich.Application.Command;
using StockRich.Domain.Request;

namespace StockRich.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// 註冊會員
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("User")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var response = await _mediator.Send(new RegisterUserCommand
            {
                Request = request
            });
            return Ok(response);
        }
    }
}
