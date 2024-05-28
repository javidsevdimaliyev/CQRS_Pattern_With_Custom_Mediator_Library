using CustomMediator.Api.Commands.Requests;
using CustomMediator.Api.Commands.Responses;
using CustomMediator.Api.Models;
using CustomMediator.Library.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CustomMediator.Api.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request)
        {
            var id = Guid.NewGuid();
            ApplicationDbContext.ProductList.Add(new()
            {
                Id = id,
                Name = request.Name,
                CreateTime = DateTime.Now,
                Price = request.Price,
                Quantity = request.Quantity
            });

            return new CreateProductCommandResponse
            {
                ProductId = id,
                IsSuccess = true
            };
        }

    }
}
