using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersByCustomerRequest(string CustomerId);
    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
    
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{customerId:guid}", async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId)).ConfigureAwait(false);
                var response = result.Adapt<GetOrdersByCustomerResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrdersByCustomer")
                .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Orders By Customer")
                .WithDescription("Gets Orders By Customer");
        }
    }
}
