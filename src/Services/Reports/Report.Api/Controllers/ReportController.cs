using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Report.Api.Communicator.Contact;
using Report.Api.Entities;
using Report.Api.Entities.Enums;
using Report.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Report.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IContactReportRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IContactCommunicator _communicator;

        public ReportController(IContactReportRepository repository,
            IPublishEndpoint publishEndpoint, IContactCommunicator communicator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _communicator = communicator ?? throw new ArgumentNullException(nameof(communicator));
        }

        [HttpPost("{location}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetReport(string location)
        {
            await _repository.CreateReport(
                new ContactReport()
                {
                    CreatedDate = DateTime.Now,
                    Status = State.Pending
                });

            await _publishEndpoint.Publish(new ContactReportEvent { Location = location });
            var res = await _communicator.GetInfoByLocation(location);

            return Ok(res);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ContactReport>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ContactReport>>> GetReports()
        {
            var reports = await _repository.GetReports();
            return Ok(reports);
        }
    }
}