namespace APIService
{
    public enum ResponseCodeType
    {
        Unknown = 0,
        Success = 200,
        UserNameIsAreadyUsed = 303,
        BadRequest = 400,
        ServerConfict = 409,
    }
}