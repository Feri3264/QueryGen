using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueryGen.Domain.Common.Enums;
using QueryGen.Application.LLM.CompletePrompt;
using QueryGen.Application.LLM.QueryPreview;
using QueryGen.Application.Session.Command.ChangeLlmModel;
using QueryGen.Application.Session.Command.ChangeName;
using QueryGen.Application.Session.Command.Create;
using QueryGen.Application.Session.Command.Delete;
using QueryGen.Application.Session.Query.GetHistory;
using QueryGen.Application.Session.Query.GetMySessions;
using QueryGen.Application.Session.Query.GetSession;
using QueryGen.Application.Session.Query.GetSessionHistories;
using QueryGen.Contracts.DTOs.Session.ChangeLlmModel;
using QueryGen.Contracts.DTOs.Session.ChangeName;
using QueryGen.Contracts.DTOs.Session.CreateSession;
using QueryGen.Contracts.DTOs.Session.GetHistory;
using QueryGen.Contracts.DTOs.Session.GetMySessions;
using QueryGen.Contracts.DTOs.Session.GetSession;
using QueryGen.Contracts.DTOs.Session.GetSessionHistories;
using QueryGen.Contracts.DTOs.Session.PreviewQuery;
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
                    s.LlmModel,
                    s.DbType.ToString(),
                    s.LlmType.ToString(),            
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
                new GetSessionQuery(sessionId, UserId)
            );

            return result.Match(
                session => Ok(
                    new GetSessionResponseDto(
                        session.Name,
                        session.ConnectionString,
                        session.Metadata,
                        session.ApiToken,
                        session.LlmModel,
                        session.DbType.ToString(),
                        session.LlmType.ToString(),
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

            if (!Enum.TryParse<DatabaseTypeEnum>(request.DbType, true, out var dbType))
                return BadRequest("Invalid database type.");

            if (!Enum.TryParse<LlmTypeEnum>(request.LlmType, true, out var llmType))
                return BadRequest("Invalid LLM Provider Type.");

            var result = await mediator.Send(
                new CreateSessionCommand(
                    request.SessionName,
                    UserId,
                    request.ApiToken,
                    request.LlmModel,
                    dbType,
                    llmType,
                    request.Server,
                    request.DbName,            
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
                        session.LlmModel,
                        session.DbType.ToString(),
                        session.LlmType.ToString(),
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

        #region ChangeName

        [HttpPatch("{sessionId}/name")]
        public async Task<IActionResult> ChangeName([FromRoute] Guid sessionId, ChangeNameRequestDto request)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send(
                new ChangeNameCommand(sessionId, request.Name, UserId)
            );

            return result.Match(
                session => Ok(
                    new ChangeNameResponseDto(
                        session.Name,
                        session.ConnectionString,
                        session.Metadata,
                        session.ApiToken,
                        session.LlmModel,
                        session.DbType.ToString(),
                        session.LlmType.ToString(),
                        session.UserId
                    )
                ),
                Problem
            );
        }

        #endregion

        #region ChangeLlmModel

        [HttpPatch("{sessionId}/llmmodel")]
        public async Task<IActionResult> ChangeModel([FromRoute] Guid sessionId, ChangeLlmModelRequestDto request)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send(
                new ChangeLlmModelCommand(sessionId, request.model, UserId)
            );

            return result.Match(
                session => Ok(
                    new ChangeLlmModelResponseDto(
                        session.Name,
                        session.ConnectionString,
                        session.Metadata,
                        session.ApiToken,
                        session.LlmModel,
                        session.DbType.ToString(),
                        session.LlmType.ToString(),
                        session.UserId
                    )
                ),
                Problem
            );
        }

        #endregion

        #region GetHistory

        [HttpGet("{sessionId}/history/{historyId}")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid sessionId, [FromRoute] Guid historyId)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send(
                new GetHistoryQuery(sessionId, UserId, historyId)
            );

            return result.Match(
                history => Ok(
                    new GetHistoryResponseDto(
                        history.Id,
                        history.Prompt,
                        history.Query,
                        history.Result,
                        history.CreatedAt,
                        history.SessionId
                    )
                ),
                Problem
            );
        }

        #endregion

        #region GetSessionHistories

        [HttpGet("{sessionId}/history")]
        public async Task<IActionResult> GetSessionHistories([FromRoute] Guid sessionId)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send(
                new GetSessionHistoriesQuery(sessionId, UserId)
            );

            var histories = new List<GetSessionHistoriesResponseDto>();

            if (result.Value is not null)
            {
                histories = result.Value.Select(h => new GetSessionHistoriesResponseDto(
                    h.Id,
                    h.Prompt,
                    h.Query,
                    h.Result,
                    h.CreatedAt,
                    h.SessionId
                )).ToList();
            }

            return result.Match(
                _ => Ok(histories),
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

        #region QueryPreview

        [HttpPost("{sessionId}/preview")]
        public async Task<IActionResult> PreviewQuery([FromRoute] Guid sessionId, PreviewQueryRequestDto request)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send(
                new QueryPreviewCommand(sessionId, UserId, request.prompt)
            );

            return result.Match(
                query => Ok(query),
                Problem
            );
        }

        #endregion
    }
}
