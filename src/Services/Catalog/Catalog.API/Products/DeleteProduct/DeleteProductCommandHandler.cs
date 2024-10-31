﻿
using Catalog.API.Exceptions;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
        }
    }
    internal class DeleteProductCommandHandler
        (IDocumentSession documentSession)
        : ICommandHandler<DeleteProductCommand,DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            documentSession.Delete<Product>(command.Id);

            await documentSession.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }

    }
}
