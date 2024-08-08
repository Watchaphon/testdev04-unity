using System;

namespace APIService
{
    public class BaseAPIResonseException : Exception
    {
        public int responseCodeType { get; private set; }

        public BaseAPIResonseException(int responseCodeType, string message) : base(message) 
        { 
            this.responseCodeType = responseCodeType;
        }
    }
}