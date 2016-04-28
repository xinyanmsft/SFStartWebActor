using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace Application1.UserActor.Interfaces
{
    public interface IUserActor : IActor
    {
        Task<UserInfo> GetUserInfoAsync();

        Task<bool> SetUserInfoAsync(UserInfo userInfo);

        Task<bool> DeleteUserInfoAsync();
    }
}
