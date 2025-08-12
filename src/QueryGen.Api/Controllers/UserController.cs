using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueryGen.Application.User.Command.Login;
using QueryGen.Application.User.Command.RefreshToken;
using QueryGen.Application.User.Command.Register;
using QueryGen.Contracts.DTOs.User.Login;
using QueryGen.Contracts.DTOs.User.RefreshToken;
using QueryGen.Contracts.DTOs.User.Register;

namespace QueryGen.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController
        (IMediator mediator) : ApiController
    {

        #region Register

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var result = await mediator.Send(
                new RegisterUserCommand(request.username, request.password)
            );

            return result.Match(
                user => Ok(
                    new RegisterResponseDto(
                        user.Id,
                        user.Username,
                        user.Password,
                        user.RefreshToken,
                        user.TokenExpire,
                        user.JwtToken
                    )
                ),
                Problem);
        }

        #endregion

        #region Login

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await mediator.Send(
                new LoginUserCommand(request.Username, request.Password)
            );

            return result.Match(
                user => Ok(
                    new LoginResponseDto(
                        user.Id,
                        user.Username,
                        user.Password,
                        user.RefreshToken,
                        user.TokenExpire,
                        user.JwtToken
                    )
                ),
                Problem
            );
        }

        #endregion

        #region RefreshToken

        [HttpPost("/api/auth/refreshtoken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await mediator.Send(
                new ValidateRefreshTokenCommand(request.RefreshToken)
            );

            return result.Match(
                token => Ok(
                    new RefreshTokenResponseDto(
                        token.JwtToken,
                        token.RefreshToken
                    )
                ),
                Problem
            );
        }

        #endregion
    }
}
