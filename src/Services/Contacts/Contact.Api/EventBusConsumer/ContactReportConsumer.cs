using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Contact.Api.EventBusConsumer
{
    public class ContactReportConsumer : IConsumer<ContactReportEvent>
    {
        private readonly IMediator _mediator;

        public ContactReportConsumer(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Consume(ConsumeContext<ContactReportEvent> context)
        {
            var result = await _mediator.Send(context.Message);
        }
    }
}
