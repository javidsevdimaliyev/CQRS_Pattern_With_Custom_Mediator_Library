using CustomMediator.Api.Commands.Responses;
using CustomMediator.Library.Interfaces;

namespace CustomMediator.Api.Commands.Requests
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
