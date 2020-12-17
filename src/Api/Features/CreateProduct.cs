using Api.Data;
using Api.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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
            private readonly IMongoCollection<Product> collection;

            public Handler(IMongoDbContext dbContext, IOptions<DatabaseSettingsOptions> options)
            {
                collection = dbContext.GetCollection<Product>(options.Value.CollectionName);
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var count = await collection.CountDocumentsAsync(x => true);
                var product = new Product()
                {
                    Name = request.Name,
                    Price = request.Price,
                    Id = count + 1
                };
                await collection.InsertOneAsync(product);
                return Unit.Value;
            }
        }
    }
}
