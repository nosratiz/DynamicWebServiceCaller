using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Templates.ModelDto;
using WebServiceCaller.Common.Result;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Application.Templates.Command.CreateTemplate
{
    public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, Result<TemplateDto>>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IMapper _mapper;

        public CreateTemplateCommandHandler(IWebServiceNotificationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<TemplateDto>> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = _mapper.Map<Template>(request);

            await _context.Templates.AddAsync(template, cancellationToken);

            await _context.SaveAsync(cancellationToken);

            return Result<TemplateDto>.SuccessFul(_mapper.Map<TemplateDto>(template));
        }
    }
}