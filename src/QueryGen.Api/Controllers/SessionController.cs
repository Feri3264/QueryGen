using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryGen.Application.Session.Query.GetMySessions;

namespace QueryGen.Api.Controllers
{
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

            var result = mediator.Send(
                new GetMySessionsQuery(UserId)
            );

            return result.Match(
                sessions => Ok(
                    
                )
            );
        }

        #endregion
    }
}
