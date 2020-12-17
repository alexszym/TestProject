using Api.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Features
{
    public class DeleteProduct
    {
        public class Command : IRequest<ActionResult>
        {
            public long Id { get; set; }
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
                var result = await collection.DeleteOneAsync(x => x.Id == request.Id, cancellationToken);

                if (result.DeletedCount == 0)
                {
                    return new NotFoundResult();
                }

                return new OkResult();
            }
        }
    }
}
