using Api.Data;
using Api.Models;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Features
{
    public class ListProducts
    {
        public class Query : IRequest<IEnumerable<ProductModel>>
        {
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ProductModel>>
        {
            private readonly IMongoCollection<Product> collection;
            private readonly IMapper mapper;

            public Handler(IMongoDbContext dbContext, IOptions<DatabaseSettingsOptions> options, IMapper mapper)
            {
                collection = dbContext.GetCollection<Product>(options.Value.CollectionName);
                this.mapper = mapper;
            }

            public async Task<IEnumerable<ProductModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var products = await collection.Find(x => true).SortBy(x => x.Id).ToListAsync(cancellationToken);
                return mapper.Map<IEnumerable<ProductModel>>(products);
            }
        }
    }

}
