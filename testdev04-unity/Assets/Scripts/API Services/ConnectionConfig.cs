namespace APIService
{
    public class ConnectionConfig
    {
        private string m_baseUrl;
        private string m_format;

        public ConnectionConfig(string baseUrl, string format)
        {
            m_baseUrl = baseUrl;
            m_format = format;
        }

        public string GetUrlDestination(string destination)
        {
            return $"{m_baseUrl}{destination}.{m_format}";
        }
    }
}