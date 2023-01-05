using ObserverPattern.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverPattern.Web.Observer
{
    public class UserObserverSubject
    {
        private readonly List<IUserObserver> _userObservers;

        public UserObserverSubject()
        {
            _userObservers = new List<IUserObserver>();
        }

        public void RegisterObserver(IUserObserver userObserver)
        {
            _userObservers.Add(userObserver);
        }

        public void UnRegisterObserver(IUserObserver userObserver)
        {
            _userObservers.Remove(userObserver);
        }

        public void NotifyObserver(AppUser appUser)
        {
            _userObservers.ForEach(v =>
            {
                v.UserCreated(appUser);
            });
        }
    }
}
