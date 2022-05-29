using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Helper;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Application.Notifiers.Queries
{
    public class GetNotifierPagedListQuery : PagingOptions, IRequest<PagedList<NotifierListDto>>
    {
    }

    public class GetNotifierPagedListQueryHandler : PagingService<Notifier>, IRequestHandler<GetNotifierPagedListQuery, PagedList<NotifierListDto>>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IMapper _mapper;

        public GetNotifierPagedListQueryHandler(IWebServiceNotificationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<NotifierListDto>> Handle(GetNotifierPagedListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Notifier> notifiers = _context.Notifiers.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreateDate);

            if (!string.IsNullOrWhiteSpace(request.Query))
                notifiers = notifiers.Where(x => x.Name.Contains(request.Query));

            var notifierList = await GetPagedAsync(request.Page, request.Limit, notifiers, cancellationToken);

            return notifierList.MapTo<NotifierListDto>(_mapper);
        }
    }
}