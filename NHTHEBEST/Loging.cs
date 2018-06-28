using System.Net;

namespace NHTHEBEST
{
    namespace Loging
    {
        public class Log
        {
            private string logtext = "";
            private WebClient www = new WebClient();
            public string LogServer;
            public int LogSendSize = 10000;
            public void SendLog()
            {
                www.UploadString(LogServer, logtext);
                logtext = "";
            }
            public void log(string text)
            {
                logtext = logtext + text;
                if (logtext.Length >= LogSendSize)
                {
                    SendLog();
                }
            }
        }
    }
}
