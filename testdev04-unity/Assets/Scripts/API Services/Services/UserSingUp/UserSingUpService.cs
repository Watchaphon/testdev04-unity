using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace APIService.Server.Database
{
    public class UserSingUpService : BaseAPIService
    {
        public UserSingUpService(ConnectionConfig connectionConfig) : base(connectionConfig) { }

        /// <summary>
        /// Send request to sigup a user data, this will throw error exception.
        /// </summary>
        /// <returns>Nothing if successfully will throw error if not success</returns>
        public async UniTask SendRequest(UserSingUpRequest singUpRequest)
        {
            await Post("userSingup", singUpRequest);
        }
    }
}