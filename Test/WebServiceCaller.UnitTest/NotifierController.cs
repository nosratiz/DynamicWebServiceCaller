using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebServiceCaller.Application.Common.Validator.SmtpNotifier;
using WebServiceCaller.Application.Notifiers.Command.CreateSmtpNotifier;
using WebServiceCaller.Application.Notifiers.Command.DeleteNotifier;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Application.Notifiers.Queries;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Result;
using Xunit;

namespace WebServiceCaller.UnitTest
{
    public class NotifierController : BaseConfiguration
    {
        [Fact]
        public async Task WhenNotifierListCalled_ReturnOkResult()
        {
            using var controller = new BaseConfiguration().Build();

            var result = await controller.NotifiersList(new PagingOptions());

            var objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact]
        public async Task WhenInvalidDataSend_ReturnNotFound()
        {
            var mockData = new Mock<IMediator>();

            mockData.Setup(x => x.Send(It.IsAny<GetSmtpNotifierQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(
                Result<SmtpNotifierDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.NotifierNotFound))));

            using var controller = new BaseConfiguration().WithMediatorService(mockData.Object).Build();

            var result = await controller.GetSmtpNotifier(It.IsAny<int>());

            var objectResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [Fact]
        public async Task WhenValidDataSend_RemoveNotifierSuccessfully()
        {
            var mockData = new Mock<IMediator>();

            mockData.Setup(x => x.Send(It.IsAny<DeleteNotifierCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.OperationSuccessful))));

            using var controller = new BaseConfiguration().WithMediatorService(mockData.Object).Build();

            var result = await controller.Delete(It.IsAny<int>());

            var objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact]
        public void WhenSendInvalidData_ReturnError()
        {
            var notifierValidator = new CreateSmtpNotifierValidator();

            var smtpNotifier = new CreateSmtpNotifierCommand();

            var result = notifierValidator.Validate(smtpNotifier);

            Assert.False(result.IsValid);

            Assert.Equal(ResponseMessage.NameIsRequired, result.Errors.FirstOrDefault()?.ErrorMessage);
        }

        [Fact]
        public void WhenInvalidEmailSent_ReturnError()
        {
            var notifierValidator = new CreateSmtpNotifierValidator();

            var smtpNotifier = new CreateSmtpNotifierCommand { Name = "بانک ملت", UserName = "ali" };

            var result = notifierValidator.Validate(smtpNotifier);

            Assert.False(result.IsValid);

            Assert.Equal(ResponseMessage.EmailIsNotWellFormed, result.Errors.FirstOrDefault()?.ErrorMessage);
        }
    }
}