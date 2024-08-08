using APIService.Server;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine;
using System.Threading.Tasks;

namespace APIService
{
    public abstract class BaseAPIService
    {
        protected ConnectionConfig connectionConfig { get; private set; }

        public BaseAPIService(ConnectionConfig connectionConfig)
        {
            this.connectionConfig = connectionConfig;
        }

        private void SetupProperty(string destination, IAPIRequest request, out string url, out string requestJson, out WWWForm form)
        {
            url = connectionConfig.GetUrlDestination(destination);
            requestJson = JsonConvert.SerializeObject(request);
            form = new();
            form.AddField("requestJson", requestJson);
        }

        protected async UniTask<BaseAPIReponse> Post(string destination, IAPIRequest request)
        {
            return await Post<BaseAPIReponse>(destination, request);
        }

        protected async UniTask<TResponse> Post<TResponse>(string destination, IAPIRequest request) where TResponse : BaseAPIReponse
        {
            SetupProperty(destination, request, out string url, out string requestJson, out WWWForm form);

            using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
            {
                TResponse reponse = await SendRequest<TResponse>(url, requestJson, webRequest);

                return reponse;
            }
        }

        private static async Task<TResponse> SendRequest<TResponse>(string url, string requestJson, UnityWebRequest webRequest) where TResponse : BaseAPIReponse
        {
            Debug.Log($"Send Request : <{url}> method <{webRequest.method}> \n Data : <{requestJson}>");

            await webRequest.SendWebRequest();

            //Check result of unity web request is fail or not.
            if (webRequest.result != UnityWebRequest.Result.Success)
                throw new System.Exception(webRequest.error);

            string resposeJson = webRequest.downloadHandler.text;

            Debug.Log($"ressponse : {resposeJson}");

            TResponse reponse = JsonConvert.DeserializeObject<TResponse>(resposeJson);

            //Check server response code is faill or not.
            if (reponse.code != 200)
                throw reponse.GetException();

            Debug.Log($"Send Request : <{url}> method <{webRequest.method}> : successfully");
            return reponse;
        }
    }
}