using MediatR;
using Microsoft.Extensions.Logging;
using ObserverPattern.Web.Models;
using ObserverPattern.Web.ObserverWithMediatR.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ObserverPattern.Web.ObserverWithMediatR.EventHandlers
{
    public class CreateDiscountEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly AppIdentityDbContext _context;
        private readonly ILogger<CreateDiscountEventHandler> _logger;

        public CreateDiscountEventHandler(ILogger<CreateDiscountEventHandler> logger, AppIdentityDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _context.Discounts.AddAsync(new Discount
            {
                Rate = 10,
                UserId = notification.AppUser.Id
            });

            await _context.SaveChangesAsync();
            _logger.LogInformation("Discount oluşturuldu");
        }
    }
}
