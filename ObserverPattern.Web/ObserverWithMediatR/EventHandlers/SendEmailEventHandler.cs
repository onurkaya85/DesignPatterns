using MediatR;
using ObserverPattern.Web.ObserverWithMediatR.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ObserverPattern.Web.ObserverWithMediatR.EventHandlers
{
    public class SendEmailEventHandler : INotificationHandler<UserCreatedEvent>
    {
        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            //Send Email
            return Task.CompletedTask;
        }
    }
}
