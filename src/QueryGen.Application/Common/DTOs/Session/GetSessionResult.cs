using System;

namespace QueryGen.Application.Common.DTOs.Session;

public class GetSessionResult
{
    public string Name { get; set; }

    public string ConnectionString { get; set; }

    public string Metadata { get; set; }

    public string ApiToken { get; set; }

    public string LlmModel { get; set; }

    public Guid UserId { get; set; }
}
