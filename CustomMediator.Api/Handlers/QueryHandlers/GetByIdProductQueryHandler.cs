using CustomMediator.Api.Models;
using CustomMediator.Api.Queries.Requests;
using CustomMediator.Api.Queries.Responses;
using CustomMediator.Library.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMediator.Api.Handlers.QueryHandlers
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request)
        {
            var product = ApplicationDbContext.ProductList.FirstOrDefault(p => p.Id == request.ProductId);
            return new GetByIdProductQueryResponse
            {
                Id = product.Id,
                CreateTime = product.CreateTime,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
            };
        }
    }
}
