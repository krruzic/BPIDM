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
            string imagePath = Path.Combine(appPath, "images");
            string src = "https://jsonblob.com/api/jsonBlob/661d7e94-fcc3-11e6-a0ba-cfb86d2966d2";

            Console.WriteLine(appPath);
            if (!Directory.Exists(appPath)) // only created after first run
            {
                // first run of app, download JSON from server
                DirectoryInfo di = Directory.CreateDirectory(appPath);
                DirectoryInfo dim = Directory.CreateDirectory(imagePath);
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.DownloadFile(src, dataPath);
                }
            }
            Application.Current.Properties["menuJSON"] = dataPath;

            // caching images
            InitializeComponent();
        }
    }
}
