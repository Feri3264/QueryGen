using System;
using QueryGen.Domain.Common.Enums;

namespace QueryGen.Application.Common.DTOs.Session;

public class ChangeModelResult
{
    public string Name { get; set; }

    public string ConnectionString { get; set; }

    public string Metadata { get; set; }

    public string ApiToken { get; set; }

    public string LlmModel { get; set; }

    public DatabaseTypeEnum DbType { get; set; }

    public LlmTypeEnum LlmType { get; set; }

    public Guid UserId { get; set; }
}
