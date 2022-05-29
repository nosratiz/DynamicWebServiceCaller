using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebServiceCaller.Application.Notification.Command.TestNotifier;
using WebServiceCaller.Application.Notifiers.Command.CreateSmtpNotifier;
using WebServiceCaller.Application.Notifiers.Command.CreateWebService;
using WebServiceCaller.Application.Notifiers.Command.DeleteNotifier;
using WebServiceCaller.Application.Notifiers.Command.UpdateSmtpNotifier;
using WebServiceCaller.Application.Notifiers.Command.UpdateWebService;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Application.Notifiers.Queries;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Helper;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCall.Api.Controllers
{
    public class NotifiersController : BaseController
    {
        private readonly IMediator _mediator;

        public NotifiersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// List Of Notifiers
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <returns> notifier list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<SmtpNotifierDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> NotifiersList([FromQuery] PagingOptions pagingOptions) => Ok(await _mediator.Send(new GetNotifierPagedListQuery
        {
            Page = pagingOptions.Page,
            Limit = pagingOptions.Limit,
            Query = pagingOptions.Query
        }));

        /// <summary>
        /// get Notifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns> notifier </returns>
        /// <response code="200">if notifier found </response>
        /// <response code="404">If notifier not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(SmtpNotifierDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("Smtp/{id}", Name = "GetSmtpNotifier")]
        public async Task<IActionResult> GetSmtpNotifier(long id)
        {
            var result = await _mediator.Send(new GetSmtpNotifierQuery { Id = id });

            return result.ApiResult;
        }

        /// <summary>
        /// get Notifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns> notifier </returns>
        /// <response code="200">if notifier found </response>
        /// <response code="404">If notifier not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(WebServiceNotifierDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("webService/{id}", Name = "GetNotifier")]
        public async Task<IActionResult> GetWebServiceNotifier(long id)
        {
            var result = await _mediator.Send(new GetWebServiceNotifierQuery { Id = id });

            return result.ApiResult;
        }



        /// <summary>
        /// Create Notifier
        /// </summary>
        /// <param name="createSmtpNotifierCommand"></param>
        /// <returns> Create notifier </returns>
        /// <response code="201">if notifier Create Successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(SmtpNotifierDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> CreateNotifier(CreateSmtpNotifierCommand createSmtpNotifierCommand)
        {
            var result = await _mediator.Send(createSmtpNotifierCommand);

            if (!result.Success)
                return result.ApiResult;

            return Created(Url.Link(result.Data.ServiceType == ServiceType.Smtp ? "GetSmtpNotifier" : "GetNotifier", new { result.Data.Id }), result.Data);
        }

        /// <summary>
        /// update Notifier
        /// </summary>
        /// <param name="updateSmtpNotifierCommand"></param>
        /// <returns> Create notifier </returns>
        /// <response code="200">if notifier update Successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="404">If notifier not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> UpdateNotifier(UpdateSmtpNotifierCommand updateSmtpNotifierCommand)
        {
            var result = await _mediator.Send(updateSmtpNotifierCommand);

            return result.ApiResult;
        }

        /// <summary>
        /// Delete  Notifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Delete notifier </returns>
        /// <response code="200">if notifier Delete Successfully </response>
        /// <response code="404">If notifier not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _mediator.Send(new DeleteNotifierCommand { Id = id });

            return result.ApiResult;
        }

        /// <summary>
        /// Create Notifier
        /// </summary>
        /// <param name="createWebServiceCommand"></param>
        /// <returns> Create notifier </returns>
        /// <response code="201">if notifier Create Successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(SmtpNotifierDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost("addWebService")]
        public async Task<IActionResult> CreateWebService(CreateWebServiceCommand createWebServiceCommand)
        {
            var result = await _mediator.Send(createWebServiceCommand);

            return result.ApiResult;
        }

        /// <summary>
        /// update Web Service Notifier
        /// </summary>
        /// <param name="updateWebServiceCommand"></param>
        /// <returns> update notifier </returns>
        /// <response code="200">if notifier Create Successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="404">If notifier Not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut("updateWebService")]
        public async Task<IActionResult> UpdateWebService(UpdateWebServiceCommand updateWebServiceCommand)
        {
            var result = await _mediator.Send(updateWebServiceCommand);

            return result.ApiResult;
        }


        [HttpPost("test")]
        public async Task<IActionResult> TestNotifier(CreateTestNotifierCommand createTestNotifier)
        {
            var result = await _mediator.Send(createTestNotifier);


            return result.ApiResult;
        }
    }
}