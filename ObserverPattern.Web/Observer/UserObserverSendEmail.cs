using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ObserverPattern.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverPattern.Web.Observer
{
    public class UserObserverSendEmail : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverSendEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void UserCreated(AppUser user)
        {
            //SendEmail
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendEmail>>();
            logger.LogInformation("Email gönderildi");
        }
    }
}
