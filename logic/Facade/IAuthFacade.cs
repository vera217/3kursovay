using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace logic.Facade
{
    public interface IAuthFacade
    {
        User Login(string username, string password);
        bool RegisterClient(string clientId, string phone, string email, string password);
    }
}
