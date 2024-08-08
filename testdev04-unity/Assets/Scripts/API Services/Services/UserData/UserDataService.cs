using Cysharp.Threading.Tasks;
using UnityEngine;

namespace APIService.Server.Database
{

    public class UserDataService : BaseAPIService
    {
        public UserDataService(ConnectionConfig connectionConfig) : base(connectionConfig) { }

        //With this statement your can wirting many api request about user data here such increaseRewrad, decreaseReward etc...,

        public async UniTask<GetUserDataResponse> SendGetUserDataRequest(GetUserDataRequest getUserDataRequest)
        {
            return await Post<GetUserDataResponse>("getUserData", getUserDataRequest);
        }

        /// <summary>
        /// Send request to increase user diamounds.
        /// 
        /// ****
        /// in real case don't allow user to increase amount form client side, 
        /// Client should only send reward id to server and server will increase user currency follow reward id and response back to client
        /// ****
        /// 
        /// </summary>
        /// <param name="modifyUserDiamondRequest"></param>
        /// <returns></returns>
        public async UniTask SendModifyDiamondRequest(UserModifyDiamonRequest modifyUserDiamondRequest)
        {
            await Post("modifyUserDiamonds", modifyUserDiamondRequest);
        }

        /// <summary>
        /// Send request to modifly user heart.
        /// 
        /// ****
        /// in real case don't allow user to increase amount form client side, 
        /// Client should only send reward id to server and server will increase user currency follow reward id and response back to client
        /// ****
        /// 
        /// </summary>
        /// <param name="modifyHeartRequest"></param>
        /// <returns></returns>
        public async UniTask SendModifyHeartRequest(UserModifyHeartRequest modifyHeartRequest)
        {
            await Post("modifyUserHearts", modifyHeartRequest);
        }
    }
}