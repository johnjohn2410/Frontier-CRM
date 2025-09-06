using MediatR;
using Microsoft.Extensions.Logging;

namespace FrontierCRM.Application.Common.Behaviors;

/// <summary>
/// Pipeline behavior for measuring request performance
/// </summary>
/// <typeparam name="TRequest">The request type</typeparam>
/// <typeparam name="TResponse">The response type</typeparam>
public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;

    public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        try
        {
            var response = await next();
            
            stopwatch.Stop();
            
            if (stopwatch.ElapsedMilliseconds > 1000)
            {
                _logger.LogWarning("Long running request: {RequestName} took {ElapsedMilliseconds}ms", 
                    requestName, stopwatch.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogInformation("Request {RequestName} completed in {ElapsedMilliseconds}ms", 
                    requestName, stopwatch.ElapsedMilliseconds);
            }
            
            return response;
        }
        catch (Exception)
        {
            stopwatch.Stop();
            _logger.LogError("Request {RequestName} failed after {ElapsedMilliseconds}ms", 
                requestName, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}
