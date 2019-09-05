using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest 
        { 
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime? Date { get; set; }
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
                var act = await ctx.Activities.FindAsync(request.Id);
                
                if(act == null)
                    throw new Exception("Could not find Activity");

                act.Title = request.Title ?? act.Title;
                act.Description = request.Description ?? act.Description;
                act.Category = request.Category ?? act.Category;
                act.Date = request.Date ?? act.Date;
                act.City = request.City ?? act.City;
                act.Venue = request.Venue ?? act.Venue;
 
                var success = await ctx.SaveChangesAsync() > 0;

                if(success) return Unit.Value;

                throw new Exception("Problem saving changes.");
            }
        }
    }
}