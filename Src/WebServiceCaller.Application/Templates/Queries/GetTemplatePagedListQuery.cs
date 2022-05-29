using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Templates.ModelDto;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Helper;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Application.Templates.Queries
{
    public class GetTemplatePagedListQuery : PagingOptions, IRequest<PagedList<TemplateDto>>
    {
    }

    public class GetTemplatePagedListQueryHandler : PagingService<Template>, IRequestHandler<GetTemplatePagedListQuery, PagedList<TemplateDto>>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IMapper _mapper;

        public GetTemplatePagedListQueryHandler(IWebServiceNotificationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<TemplateDto>> Handle(GetTemplatePagedListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Template> templates = _context.Templates.Where(x => !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(request.Query))
                templates = templates.Where(x => x.Title.Contains(request.Query));

            var templateList = await GetPagedAsync(request.Page, request.Limit, templates, cancellationToken);

            return templateList.MapTo<TemplateDto>(_mapper);
        }
    }
}