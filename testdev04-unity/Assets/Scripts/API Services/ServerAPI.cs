using APIService.Server.Database;

namespace APIService.Server
{
    public class ServerAPI
    {
        private const string HostUrl = "https://test-piggy.codedefeat.com/worktest/dev04/";
        private const string DestinationFormat = "php";

        private static ServerAPI m_instance;

        public static ServerAPI Instance
        {
            get
            {
                if(m_instance == null)
                    m_instance = new ServerAPI();

                return m_instance;
            }
        }


        private ConnectionConfig m_connectionConfig;

        #region Services
        public UserLoginService userLoginService { get; private set; }
        public UserSingUpService userSingUpService { get; private set; }
        public UserDataService userDataService { get; private set; }
        #endregion

        public ServerAPI()
        {
            m_connectionConfig = new ConnectionConfig(HostUrl, DestinationFormat);
            userLoginService = new UserLoginService(m_connectionConfig);
            userSingUpService = new UserSingUpService(m_connectionConfig);
            userDataService = new UserDataService(m_connectionConfig);
        }
    }
}