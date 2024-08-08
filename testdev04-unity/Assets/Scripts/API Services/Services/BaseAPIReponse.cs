using UnityEngine;

namespace APIService.Server
{
    public class BaseAPIReponse
    {
        public int code;
        public string message;
        
        public BaseAPIResonseException GetException()
        {
            return new BaseAPIResonseException(code, message);
        }
    }
}