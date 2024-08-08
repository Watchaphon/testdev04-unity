namespace APIService.Server {

    public class GetUserDataResponse : BaseAPIReponse
    {
        public UserDataInfo userData;
    }

    public class UserDataInfo
    {
        public int userDiamonds;
        public int userHearts;
    }
}