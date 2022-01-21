using pruebatecnicapeaku.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruebatecnicapeaku.Client.Auth
{
    interface ILoginService
    {
        Task Login(UserToken userToken);
        Task Logout();
        Task HandleRenovationToken();
    }
}
