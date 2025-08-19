using System;
using ErrorOr;
using QueryGen.Domain.Common;

namespace QueryGen.Domain.SessionHistory;

public class SessionHistoryModel : BaseClass
{
    public string Prompt { get; private set; }

    public string Query { get; private set; }

    public string Result { get; private set; }

    public DateTime CreatedAt { get; private set; }


    //navigation
    public Guid SessionId { get; private set; }


    //ctor
    public SessionHistoryModel(
        string _prompt,
        string _query,
        string _result,
        DateTime _createdAt,
        Guid _sessionId
    )
    {
        Id = Guid.NewGuid();
        Prompt = _prompt;
        Query = _query;
        Result = _result;
        CreatedAt = _createdAt;
        SessionId = _sessionId;
    }


    //methods
    public static ErrorOr<SessionHistoryModel> Create(
        string _prompt,
        string _query,
        string _result,
        DateTime _createdAt,
        Guid _sessionId
    )
    {
        if (String.IsNullOrEmpty(_prompt))
            return SessionHistoryError.PromptIsEmpty;

        if (String.IsNullOrEmpty(_query))
            return SessionHistoryError.QueryIsEmpty;

        if (String.IsNullOrEmpty(_result))
            return SessionHistoryError.ResultIsEmpty;

        return new SessionHistoryModel(_prompt, _query, _result, _createdAt, _sessionId);
    }
    
    private SessionHistoryModel() {}
}
