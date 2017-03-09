using System;
using System.IO;
using System.Net;
using System.Windows;

namespace BPIDM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // stuff needed to download json only once :)
            Application.Current.Properties["menuJSON"] = "";
            string data = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string appPath = Path.Combine(data, name);
            string dataPath = Path.Combine(appPath, "menu.json");
            string src = "https://images.krruzic.xyz/BPIDM/menu-min.json";

            if (!File.Exists(dataPath)) // only created after first run
            {
                // first run of app, download JSON from server
                if (!Directory.Exists(appPath))
                    Directory.CreateDirectory(appPath);
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.DownloadFile(src, dataPath);
                }
            }
            else // redownload if modified
            {
                if (IsResourceModified(src, File.GetLastAccessTimeUtc(dataPath)))
                {
                    using (var client = new WebClient())
                    {
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        client.Headers[HttpRequestHeader.Accept] = "application/json";
                        File.Delete(dataPath);
                        client.DownloadFile(new Uri(src), dataPath);
                    }
                }
            }
            Application.Current.Properties["menuJSON"] = dataPath;
            InitializeComponent();
        }

        bool IsResourceModified(string url, DateTime dateTime)
        {
            try
            {
                var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.IfModifiedSince = dateTime;
                request.Method = "HEAD";
                var response = (HttpWebResponse)request.GetResponse();

                return true;
            }
            catch (WebException ex)
            {
                if (ex.Status != WebExceptionStatus.ProtocolError)
                    throw;

                var response = (HttpWebResponse)ex.Response;
                if (response.StatusCode != HttpStatusCode.NotModified)
                    throw;

                return false;
            }
        }
    }
}
