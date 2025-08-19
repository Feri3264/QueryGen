using System;

namespace QueryGen.Application.Common.DTOs.Session;

public class GetHistoryResult
{
    public Guid Id { get; set; }

    public string Prompt { get; set; }

    public string Query { get; set; }

    public string Result { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid SessionId { get; set; }
}
