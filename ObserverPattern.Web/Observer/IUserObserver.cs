using ObserverPattern.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverPattern.Web.Observer
{
    public interface IUserObserver
    {
        void UserCreated(AppUser user);
    }
}
