using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Services
{
    public interface IAuthService
    {
        Task<bool> Signup(string userName, string password, int restaurantId);
        Task<bool> Login(string userName, string password, int restaurantId);
        Task Logout();
    }
}
