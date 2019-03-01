using System.Net;

namespace NHTHEBEST
{
    namespace Loging
    {
        public class NetworkLog
        {
            private string logtext = "";
            private WebClient www = new WebClient();
            public string LogServer;
            private int LogSendSize = 10000;
            public void SendLog()
            {
                www.UploadString(LogServer, logtext);
                logtext = "";
            }
            private void logadd(string text)
            {
                logtext = logtext + text;
                if (logtext.Length >= LogSendSize)
                {
                    SendLog();
                }
            }
            public void Log(object text)
            {
                logadd(text.ToString());
            }
        }
    }
}
