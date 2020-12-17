using Api.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Features
{
    public class UpdateProduct
    {
        public class Command : IRequest<ActionResult>
        {
            [JsonIgnore]
            public long Id { get; set; }
            public string Name { get; set; }

            public string Price { get; set; }
        }

        public class Handler : IRequestHandler<Command, ActionResult>
        {
            private readonly IMongoCollection<Product> collection;

            public Handler(IMongoDbContext dbContext, IOptions<DatabaseSettingsOptions> options)
            {
                collection = dbContext.GetCollection<Product>(options.Value.CollectionName);
            }

            public async Task<ActionResult> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Name is null && request.Price is null)
                {
                    // if no changes to data return OK
                    return new OkResult();
                }

                UpdateDefinition<Product> updateDefinition = null;

                if (request.Name != null)
                {
                    updateDefinition = Builders<Product>.Update.Set(s => s.Name, request.Name);
                }

                if (request.Price != null)
                {
                    updateDefinition = Builders<Product>.Update.Set(s => s.Price, request.Price);
                }

                var result = await collection.UpdateOneAsync(x => x.Id == request.Id, updateDefinition, cancellationToken: cancellationToken);

                if (result.MatchedCount == 0)
                {
                    return new NotFoundResult();
                }

                return new OkResult();
            }
        }
    }
}
