using System;
using ErrorOr;

namespace QueryGen.Domain.SessionHistory;

public class SessionHistoryError
{
    public static Error HistoryNotFound = Error.Validation
        (code: "hsitory.not.found", description: "History Not Found");

    public static Error PromptIsEmpty = Error.Validation
        (code: "prompt.is.empty", description: "History Propmt Is Empty");

    public static Error QueryIsEmpty = Error.NotFound
        (code: "query.is.empty", description: "History Query Is Empty");

    public static Error ResultIsEmpty = Error.NotFound
        (code: "result.is.empty", description: "History Result Is Empty");

}
