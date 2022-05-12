using Contact.Application.Features.ContactPersonInfos.Commands.DeleteContactPersonInfo;
using Contact.Application.Features.ContactPersonInfos.Commands.SaveContactPersonInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Contact.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContactPersonInfoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactPersonInfoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost(Name = "SaveContactInfo")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> SaveContactInfo([FromBody] SaveContactPersonInfoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete(Name = "DeleteContactPersonInfo")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteContactPersonInfo([FromBody] DeleteContactPersonInfoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
