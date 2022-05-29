using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebServiceCaller.Application.Common.Interfaces;

namespace WebServiceCaller.Application.System
{
    public class SampleSeedDataCommand : IRequest
    {
    }

    public class SampleSeedDataCommandHandler : IRequestHandler<SampleSeedDataCommand>
    {
        private readonly IWebServiceNotificationContext _context;

        public SampleSeedDataCommandHandler(IWebServiceNotificationContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SampleSeedDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new SeedData(_context);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}