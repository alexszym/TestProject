using Api.Data;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Features
{
    public class CreateProduct
    {
        public class Command : IRequest<Unit>
        {
            public string Name { get; set; }

            public string Price { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMongoDbContext dbContext;
            private readonly string collectionName;

            public Handler(IMongoDbContext dbContext, IOptions<DatabaseSettingsOptions> options)
            {
                this.dbContext = dbContext;
                collectionName = options.Value.CollectionName;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var nextSequence = await dbContext.GetNextSequenceNumber<Product>(collectionName);
                var product = new Product()
                {
                    Name = request.Name,
                    Price = request.Price,
                    Id = nextSequence
                };
                var collection = dbContext.GetCollection<Product>(collectionName);
                await collection.InsertOneAsync(product);

                await dbContext.IncrementSequenceNumberAsync<Product>(collectionName);
                return Unit.Value;
            }
        }
    }
}
