using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Execution.Processing;
using HotChocolate.Resolvers;
using Server.Helpers;
using Server.Utils;
using ErrorCodes = Shared.Helpers.ErrorCodes;

namespace Server.GraphQL;

/// <summary>
/// Handle all GraphQL errors
/// </summary>
public class GraphQlErrorFilter : IErrorFilter
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IErrorMessages _errorMessages;

    public GraphQlErrorFilter(IHostEnvironment hostEnvironment, IErrorMessages errorMessages)
    {
        _hostEnvironment = hostEnvironment;
        _errorMessages = errorMessages;
    }

    public IError OnError(IError error)
    {
        if (error.Code == "AUTH_NOT_AUTHORIZED" || error.Message == ErrorCodes.CODE_NOT_AUTHORIZED)
        {
            IError errorResponse = ErrorBuilder
                .New()
                .SetMessage(_errorMessages.ERROR_INSUFFICIENT_AUTHENTICATION())
                .SetCode(ErrorCodes.CODE_NOT_AUTHORIZED)
                .Build();

            return errorResponse;
        }

        if (!EnvironmentUtil.AllowDebugForEnvironments(_hostEnvironment.EnvironmentName))
            return error.WithMessage(error.Exception?.Message ?? ErrorCodes.CODE_ERROR_GRAPHQL);
        return error.WithMessage(error.Exception?.Message ?? error.Message);
    }
}

/// <summary>
/// Advance GraphQL error for debugging
/// </summary>
public class ErrorLoggingDiagnosticsEventListener : ExecutionDiagnosticEventListener
{
    private ILogger<ErrorLoggingDiagnosticsEventListener> Logger { get; }

    public ErrorLoggingDiagnosticsEventListener(ILogger<ErrorLoggingDiagnosticsEventListener> logger)
    {
        Logger = logger;
    }

    public override void ResolverError(IMiddlewareContext context, IError error)
    {
        string pathName = context.Path.ToString();
        Logger.LogError(
            "Path: {PathName}, Code: {ErrorCode}, Message: {ErrorMessage}",
            pathName,
            error.Code,
            error.Message
        );
    }

    public override void TaskError(IExecutionTask task, IError error)
    {
        Logger.LogError("GraphQL TaskError: {ErrorMessage}", error.Message);
    }

    public override void RequestError(IRequestContext context, Exception exception)
    {
        Logger.LogError(exception, "GraphQL RequestError");
    }

    public override void SubscriptionEventError(SubscriptionEventContext context, Exception exception)
    {
        Logger.LogError(exception, "GraphQL SubscriptionEventError");
    }

    public override void SubscriptionTransportError(ISubscription subscription, Exception exception)
    {
        Logger.LogError(exception, "GraphQL SubscriptionTransportError");
    }
}
