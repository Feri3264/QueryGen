using System;
using Microsoft.Extensions.DependencyInjection;
using QueryGen.Application.Common.Services;
using QueryGen.Application.Common.Utilities;
using QueryGen.Domain.Common.Enums;
using QueryGen.Infrastructure.Common.Services;

namespace QueryGen.Infrastructure.Common.Utilities;

public class LlmServiceFactory
    (IEnumerable<ILlmServices> providers) : ILlmServiceFactory
{
    public ILlmServices GetProvider(LlmTypeEnum providerName)
    {
        return providerName switch
        {
            LlmTypeEnum.openrouter => providers.OfType<OpenRouterLlmServices>().First(),
            LlmTypeEnum.ollama    => providers.OfType<OllamaLlmServices>().First(),
            _ => throw new ArgumentException("Invalid provider")
        };
    }
}
