using Application1.UserActor.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Application1.Gateway.Controllers
{
    /// <summary>
    /// Redirect HTTP requests to actors.
    /// </summary>
    public class UserController : ApiController
    {
        public async Task<UserInfo> Get(int id)
        {
            IUserActor actor = this.GetActor(id);
            UserInfo userInfo = await actor.GetUserInfoAsync();
            if (userInfo == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            } else
            {
                return userInfo;
            }
        }

        public async Task Post(int id, [FromBody]UserInfo value)
        {
            IUserActor actor = this.GetActor(id);
            await actor.SetUserInfoAsync(value);
        }

        public async Task Put(int id, [FromBody]UserInfo value)
        {
            IUserActor actor = this.GetActor(id);
            await actor.SetUserInfoAsync(value);
        }

        public async Task Delete(int id)
        {
            IUserActor actor = this.GetActor(id);
            await actor.DeleteUserInfoAsync();
        }

        private IUserActor GetActor(long id)
        {
            ActorId actorId = new ActorId(id);
            return ActorProxy.Create<IUserActor>(actorId, new Uri("fabric:/Application1/UserActorService"));
        }
    }
}
