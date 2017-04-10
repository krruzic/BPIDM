using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace BPIDM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Task _initializingTask;

        public App()
        {
            _initializingTask = Init();
        }

        private async Task Init()
        {
            // stuff needed to download json only once :)
            Application.Current.Properties["menuJSON"] = "";
            string data = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string appPath = Path.Combine(data, name);
            string dataPath = Path.Combine(appPath, "menu.json");
            Application.Current.Properties["menuJSON"] = dataPath;

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
                    await client.DownloadFileTaskAsync(src, dataPath);
                }
            }
            else // redownload if modified
            {
                if (await IsResourceModified(src, File.GetLastAccessTimeUtc(dataPath)))
                {
                    using (var client = new WebClient())
                    {
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        client.Headers[HttpRequestHeader.Accept] = "application/json";
                        File.Delete(dataPath);
                        await client.DownloadFileTaskAsync(new Uri(src), dataPath);
                    }
                }
            }
            InitializeComponent();
        }

        async Task<bool> IsResourceModified(string url, DateTime dateTime)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.IfModifiedSince = dateTime;
                var request = await client.GetAsync(new Uri(url));
                if (request.StatusCode != HttpStatusCode.NotModified) return false;
                return true;
            }
            catch (Exception e)
            {
                return true;
            }
        }
    }
}
