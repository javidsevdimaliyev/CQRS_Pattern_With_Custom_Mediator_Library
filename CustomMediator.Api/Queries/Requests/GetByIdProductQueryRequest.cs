using CustomMediator.Api.Queries.Responses;
using CustomMediator.Library.Interfaces;
using System;

namespace CustomMediator.Api.Queries.Requests
{
    public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
    {
        public Guid ProductId { get; set; }
    }
}
