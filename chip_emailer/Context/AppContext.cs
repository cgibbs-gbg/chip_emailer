using SnpCaller.Models;

namespace SnpCaller.Contexts
{
    public class AppContext
    {
        private static AppContext _instance = null;

        private AppContext()
        {
        }

        public static AppContext Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new AppContext();
                }
                return _instance;

            }
        }

        public ClientTypes ClientType { get; private set; } = ClientTypes.WebClient;

        public void SetSystemClientMode()
        {
            ClientType = ClientTypes.ServiceClient;
        }

        public UserInfo ActualUser { get; set; }
    }

    public enum ClientTypes
    {
        WebClient= 0,
        ServiceClient
    }
}
