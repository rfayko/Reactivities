using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest 
        { 
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime Date { get; set; }
            public string City { get; set; }   
            public string Venue { get; set; }   
        }
       

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext ctx;
            public Handler(DataContext ctx)
            {
                this.ctx = ctx;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
               var act = new Activity
               {
                   Id = request.Id,
                   Title = request.Title,
                   Description = request.Description,
                   Category = request.Category,
                   Date = request.Date,
                   City = request.City,
                   Venue = request.Venue
               };

               ctx.Activities.Add(act);

               var success = await ctx.SaveChangesAsync() > 0;

               if(success) return Unit.Value;

               throw new Exception("Problem saving changes.");
            }
        }
    }
}