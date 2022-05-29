using MediatR;
using Moq;
using WebServiceCall.Api.Controllers;

namespace WebServiceCaller.UnitTest
{
    public class BaseConfiguration
    {
        private IMediator _mediator;

        public BaseConfiguration()
        {
            _mediator = new Mock<IMediator>().Object;
        }

        internal BaseConfiguration WithMediatorService(IMediator mediator)
        {
            _mediator = mediator;
            return this;
        }

        internal NotifiersController Build()
        {
            return new NotifiersController(_mediator);
        }

        internal TemplatesController BuildConfiguration()
        {
            return new TemplatesController(_mediator);
        }
    }
}