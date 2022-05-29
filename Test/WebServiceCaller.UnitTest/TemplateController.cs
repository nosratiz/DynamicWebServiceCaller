using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebServiceCaller.Application.Templates.ModelDto;
using WebServiceCaller.Application.Templates.Queries;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Result;
using Xunit;

namespace WebServiceCaller.UnitTest
{
    public class TemplateController : BaseConfiguration
    {
        [Fact]
        public async Task When_ListOfTemplateCall_returnOK()
        {
            using var controller = new BaseConfiguration().BuildConfiguration();

            var result = await controller.Get(new PagingOptions());

            var objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }


        [Fact]
        public async Task When_TemplateNotFound_Return404Status()
        {
            var mockTemplate=new Mock<IMediator>();

            mockTemplate.Setup(x => x.Send(It.IsAny<GetTemplateQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<TemplateDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.TemplateNotFound))));
            
            using var controller = new BaseConfiguration().WithMediatorService(mockTemplate.Object).BuildConfiguration();

            var result = await controller.GetTemplate(It.IsAny<long>());
            
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }
        
        
    }
}