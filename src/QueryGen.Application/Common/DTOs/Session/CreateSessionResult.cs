using System;
using System.Text.Json.Nodes;

namespace QueryGen.Application.Common.DTOs.Session;

public class CreateSessionResult
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string ConnectionString { get; set; }

    public string Metadata { get; set; }

    public Guid UserId { get; set; }
}
