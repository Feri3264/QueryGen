using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueryGen.Application.LLM.CompletePrompt;
using QueryGen.Application.Session.Command.Create;
using QueryGen.Application.Session.Command.Delete;
using QueryGen.Application.Session.Query.GetMySessions;
using QueryGen.Application.Session.Query.GetSession;
using QueryGen.Contracts.DTOs.Session.CreateSession;
using QueryGen.Contracts.DTOs.Session.GetMySessions;
using QueryGen.Contracts.DTOs.Session.GetSession;
using QueryGen.Contracts.DTOs.Session.SendPrompt;

namespace QueryGen.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SessionController
        (IMediator mediator) : ApiController
    {
        #region GetUserSessions

        [HttpGet("/api/sessions")]
        public async Task<IActionResult> GetMySessions()
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send(
                new GetMySessionsQuery(UserId)
            );

            var sessions = new List<GetMySessionsResponseDto>();

            if (result.Value is not null)
            {
                sessions = result.Value.Select(s => new GetMySessionsResponseDto(
                    s.Name,
                    s.ConnectionString,
                    s.Metadata,
                    s.ApiToken,
                    s.UserId
                )).ToList();
            }

            return result.Match(
                _ => Ok(sessions),
                Problem
            );
        }

        #endregion

        #region GetSession

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetSession([FromRoute] Guid sessionId)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send(
                new GetSessionQuery(sessionId , UserId)
            );

            return result.Match(
                session => Ok(
                    new GetSessionResponseDto(
                        session.Name,
                        session.ConnectionString,
                        session.Metadata,
                        session.ApiToken,
                        session.UserId
                    )
                ),
                Problem
            );
        }

        #endregion

        #region CreateSession

        [HttpPost]
        public async Task<IActionResult> CreateSession(CreateSessionRequestDto request)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send(
                new CreateSessionCommand(
                    request.SessionName,
                    UserId,
                    request.ApiToken,
                    request.Server,
                    request.DbName,
                    request.useWinAuth,
                    request.username,
                    request.password,
                    request.port
                )
            );

            return result.Match(
                session => Ok(
                    new CreateSessionResponseDto(
                        session.Name,
                        session.ConnectionString,
                        session.Metadata,
                        session.ApiToken,
                        session.UserId
                    )
                ),
                Problem
            );
        }

        #endregion

        #region Delete

        [HttpDelete("{sessionId}")]
        public async Task<IActionResult> DeleteSession([FromRoute] Guid sessionId)
        {
            var result = await mediator.Send(
                new DeleteSessionCommand(sessionId)
            );

            return result.Match(
                _ => Ok(),
                Problem
            );
        }

        #endregion

        #region Send Prompt

        [HttpPost("{sessionId}/prompt")]
        public async Task<IActionResult> SendPrompt([FromRoute] Guid sessionId, [FromBody] SendPromptRequestDto request)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send(
                new CompletePromptCommand(sessionId, UserId, request.prompt)
            );

            return result.Match(
                response => Ok(response),
                Problem
            );
        }

        #endregion
    }
}
