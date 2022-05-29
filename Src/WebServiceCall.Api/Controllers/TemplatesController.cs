using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebServiceCaller.Application.Templates.Command.CreateTemplate;
using WebServiceCaller.Application.Templates.Command.DeleteTemplate;
using WebServiceCaller.Application.Templates.Command.UpdateTemplate;
using WebServiceCaller.Application.Templates.ModelDto;
using WebServiceCaller.Application.Templates.Queries;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Helper;

namespace WebServiceCall.Api.Controllers
{
    public class TemplatesController : BaseController
    {
        private readonly IMediator _mediator;

        public TemplatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// List Of template
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <returns> template list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<TemplateDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingOptions pagingOptions) => Ok(await _mediator.Send(
            new GetTemplatePagedListQuery
            {
                Page = pagingOptions.Page,
                Limit = pagingOptions.Limit,
                Query = pagingOptions.Query
            }));

        /// <summary>
        /// Get  template
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Get Template</returns>
        /// <response code="200">get Template back </response>
        /// <response code="404">If template not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(TemplateDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplate(long id)
        {
            var result = await _mediator.Send(new GetTemplateQuery {Id = id});

            return result.ApiResult;
        }

        /// <summary>
        /// create template
        /// </summary>
        /// <param name="createTemplateCommand"></param>
        /// <returns> Add new template</returns>
        /// <response code="201">if template generate successful </response>
        /// <response code="400">If validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(TemplateDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> CreateTemplate(CreateTemplateCommand createTemplateCommand)
        {
            var result = await _mediator.Send(createTemplateCommand);

            return result.ApiResult;
        }

        /// <summary>
        /// update template
        /// </summary>
        /// <param name="updateTemplateCommand"></param>
        /// <returns> update existing template template</returns>
        /// <response code="200">if template update successfully </response>
        /// <response code="400">If validation Failed</response>
        /// <response code="404">If template not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> UpdateTemplate(UpdateTemplateCommand updateTemplateCommand)
        {
            var result = await _mediator.Send(updateTemplateCommand);

            return result.ApiResult;
        }

        /// <summary>
        /// Delete template
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Delete template</returns>
        /// <response code="200">if template Delete successfully </response>
        /// <response code="404">If template not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate(long id)
        {
            var result = await _mediator.Send(new DeleteTemplateCommand {Id = id});

            return result.ApiResult;
        }
    }
}