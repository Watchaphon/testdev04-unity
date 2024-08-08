namespace APIService.Server
{
    /// <summary>
    /// ****
    /// in real case don't allow user to increase amount form client side, 
    /// Client should only send reward id to server and server will increase user currency follow reward id and response back to client
    /// ****
    /// </summary>
    public class UserModifyHeartRequest : IAPIRequest
    {
        public int userId;
        public int amount;
    }
}