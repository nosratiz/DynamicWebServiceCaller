using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebServiceCaller.Application.Notification.Command.BulkInsertNotification;
using WebServiceCaller.Application.Notification.Command.CreateNotification;
using WebServiceCaller.Common.General;

namespace WebServiceCall.Api.Controllers
{
    public class NotificationsController : BaseController
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create batch Notification
        /// </summary>
        /// <param name="createNotificationCommand"></param>
        /// <returns> Create batch notification </returns>
        /// <response code="200">if Notification Create Successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost("BulkInsert")]
        public async Task<IActionResult> SendNotifications(CreateNotificationsCommand createNotificationCommand)
        {
            var result = await _mediator.Send(createNotificationCommand);

            return result.ApiResult;
        }


        /// <summary>
        /// Create Notification
        /// </summary>
        /// <param name="createNotificationCommand"></param>
        /// <returns> Create notification </returns>
        /// <response code="200">if Notification Create Successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> SendNotification(CreateNotificationCommand createNotificationCommand)
        {
            var result = await _mediator.Send(createNotificationCommand);

            return result.ApiResult;
        }
    }
}