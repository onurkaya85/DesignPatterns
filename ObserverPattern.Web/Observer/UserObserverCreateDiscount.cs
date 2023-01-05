using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ObserverPattern.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverPattern.Web.Observer
{
    public class UserObserverCreateDiscount : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverCreateDiscount(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void UserCreated(AppUser user)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();
            
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                context.Discounts.Add(new Discount
                {
                    Rate = 10,
                    UserId = user.Id
                });

                context.SaveChanges();
                logger.LogInformation("Discount oluşturuldu");
            }
        }
    }
}
