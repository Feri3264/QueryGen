using System;
using QueryGen.Application.Common.Services;
using QueryGen.Domain.Common.Enums;

namespace QueryGen.Application.Common.Utilities;

public interface ILlmServiceFactory
{
    ILlmServices GetProvider(LlmTypeEnum LlmType);
}
