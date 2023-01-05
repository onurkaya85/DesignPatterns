using MediatR;
using Microsoft.Extensions.Logging;
using ObserverPattern.Web.ObserverWithMediatR.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ObserverPattern.Web.ObserverWithMediatR.EventHandlers
{
    public class CreatedUserWriteConsoleEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<CreatedUserWriteConsoleEventHandler> _logger;

        public CreatedUserWriteConsoleEventHandler(ILogger<CreatedUserWriteConsoleEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"user created: { notification.AppUser.Id}");
            return Task.CompletedTask;
        }
    }
}
