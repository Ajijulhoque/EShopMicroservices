namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> (ILogger<LoggingBehavior<TRequest, TResponse>> logger): IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull , IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={RequestName} - Response={ResponseName} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;

            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] The Request={RequestName} - Response={ResponseName} - TimeTaken={TimeTaken}",
                    typeof(TRequest).Name, typeof(TResponse).Name, timeTaken);
            }

            logger.LogInformation("[END] Handle request={RequestName} - Response={ResponseName} - ResponseData={ResponseData}",
                typeof(TRequest).Name, typeof(TResponse).Name, response);

            return response;
        }
    }
}
