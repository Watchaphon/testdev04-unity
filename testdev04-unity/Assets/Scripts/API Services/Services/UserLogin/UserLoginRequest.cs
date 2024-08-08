namespace APIService.Server
{
    public class UserLoginRequest : IAPIRequest
    {
        public string userName;
        public string userPassword;
    }
}