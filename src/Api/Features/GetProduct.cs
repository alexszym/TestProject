using Api.Data;
using Api.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Features
{
    public class GetProduct
    {
        public class Query : IRequest<ActionResult<ProductModel>>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ActionResult<ProductModel>>
        {
            private readonly IMongoCollection<Product> collection;
            private readonly IMapper mapper;

            public Handler(IMongoDbContext dbContext, IOptions<DatabaseSettingsOptions> options, IMapper mapper)
            {
                collection = dbContext.GetCollection<Product>(options.Value.CollectionName);
                this.mapper = mapper;
            }

            public async Task<ActionResult<ProductModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await collection.Find(x => x.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

                if (product is null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(mapper.Map<ProductModel>(product));
            }
        }
    }
}
