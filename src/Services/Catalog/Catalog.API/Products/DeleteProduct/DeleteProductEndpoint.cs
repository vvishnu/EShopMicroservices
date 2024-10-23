
using MediatR;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var request = new DeleteProductRequest(id);

                var result = await sender.Send(request.Adapt<DeleteProductCommand>());

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDescription("Delete Product")
            .WithSummary("Delete Product");
        }
    }
}
