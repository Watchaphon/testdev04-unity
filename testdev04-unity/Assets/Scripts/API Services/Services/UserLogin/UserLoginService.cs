using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace APIService.Server.Database
{
    public class UserLoginService : BaseAPIService
    {
        public UserLoginService(ConnectionConfig connectionConfig) : base(connectionConfig) { }

        /// <summary>
        /// Send request to login, this will throw error exception.
        /// </summary>
        /// <returns>UserLoginResponse if successfully will throw error if not success</returns>
        public async UniTask<UserLoginResponse> SendRequest(UserLoginRequest loginRequest)
        {
            return await Post<UserLoginResponse>("userLogin", loginRequest);
        }
    }
}