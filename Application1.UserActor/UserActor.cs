using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using Application1.UserActor.Interfaces;

namespace Application1.UserActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class UserActor : Actor, IUserActor
    {
        public async Task<UserInfo> GetUserInfoAsync()
        {
            var userInfo = await this.StateManager.TryGetStateAsync<UserInfo>("userInfo");
            return userInfo.HasValue ? userInfo.Value : null;
        }

        public async Task<bool> SetUserInfoAsync(UserInfo userInfo)
        {
            userInfo.Id = this.Id.GetLongId();
            await this.StateManager.SetStateAsync("userInfo", userInfo);
            return true;
        }

        public async Task<bool> DeleteUserInfoAsync()
        {
            await this.StateManager.RemoveStateAsync("userInfo");
            return true;
        }

        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");
            return Task.FromResult(true);
        }
    }
}
