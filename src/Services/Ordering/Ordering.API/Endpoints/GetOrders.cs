﻿using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersRequest(PaginationRequest PaginationRequest);
    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
    
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters]PaginationRequest paginationRequest, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersQuery(paginationRequest)).ConfigureAwait(false);
                var response = result.Adapt<GetOrdersResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrders")
                .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Orders")
                .WithDescription("Gets Orders");
        }
    }
}