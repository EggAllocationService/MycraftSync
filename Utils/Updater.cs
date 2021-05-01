using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Net;

namespace MycraftSync.Utils
{
    class Updater
    {

        #region stackoverflow copy-paste
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
        #endregion

        public static readonly HttpClient http = new HttpClient();
        public static readonly BetterWebClient webClient = new BetterWebClient();

        public static void Setup()
        {
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            webClient.Timeout = 600 * 60 * 1000;
        }

        private static void WebClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            UpdatingWindow.instance.smallBar.Value = e.ProgressPercentage;
        }

        public async static Task Update(Source pack)
        {
            Directory.CreateDirectory(Path.Combine(Config.rootDir, pack.name));
            var response = await http.GetAsync(pack.baseURL + "/manifest.json");
            if (response.StatusCode != System.Net.HttpStatusCode.OK) { return; }
            string result = await response.Content.ReadAsStringAsync();
            Manifest remote = JsonConvert.DeserializeObject<Manifest>(result);

            var rootDir = Path.Combine(Config.rootDir, pack.name);
            Manifest local;
            if (!System.IO.File.Exists(Path.Combine(rootDir, ".manifest.json")))
            {
                // first installing the pack
                System.IO.File.WriteAllText(Path.Combine(rootDir, ".manifest.json"), result);
                local = new Manifest();
                local.files = new List<File>();
            } else
            {
                local = JsonConvert.DeserializeObject<Manifest>(System.IO.File.ReadAllText(Path.Combine(rootDir, ".manifest.json")));
            }


            // do update
            var diff = Diff(local, remote);
            List<UpdateTask> delete_tasks = diff.FindAll(task => task.action == Action.Delete);
            List<UpdateTask> download_tasks = diff.FindAll(task => task.action == Action.Download);

            // start by deleting local files that must be destroyed
            if (delete_tasks.Count != 0)
            {
                // there are files to delete
                SetState(UpdatingWindow.instance.progressBar1, 2);
                UpdatingWindow.instance.progressBar1.Maximum = delete_tasks.Count;
                UpdatingWindow.instance.progressBar1.Value = 0;
                foreach (var task in delete_tasks)
                {
                    UpdatingWindow.instance.statusText.Text = "Deleting " + task.file.path + "...";
                    System.IO.File.Delete(Path.Combine(rootDir, task.file.path));
                    UpdatingWindow.instance.progressBar1.Value++;
                }
            }
            // next let the downloading begin
            if (download_tasks.Count != 0)
            {
                UpdatingWindow.instance.progressBar1.Value = 0;
                SetState(UpdatingWindow.instance.progressBar1, 1);
                int toDownload = 0;
                foreach (var task in download_tasks)
                {
                    toDownload += task.file.size;
                }
                UpdatingWindow.instance.progressBar1.Maximum = toDownload;
                foreach (var task in download_tasks)
                {
                    UpdatingWindow.instance.statusText.Text = "Downloading " + task.file.path + "...";

                    var url = new Uri( pack.baseURL + "objects/" + task.file.hash.Substring(0, 2) + "/" + task.file.hash);
                    var target = Path.Combine(rootDir, task.file.path);
                    Directory.CreateDirectory(Path.GetDirectoryName(target));

                    await webClient.DownloadFileTaskAsync(url, target);
                    UpdatingWindow.instance.progressBar1.Value += task.file.size;
                }

            }

        }

       

        public static List<UpdateTask> Diff(Manifest local, Manifest remote)
        {
            var tasks = new List<UpdateTask>();
            var local_files = FileHashmap(local);
            var remote_files = FileHashmap(remote);
            // check for files in local that arent in remote
            foreach (var e in local.files)
            {
                if (!remote_files.ContainsKey(e.hash))
                {
                    // file removed in new version
                    var x = new UpdateTask();
                    x.action = Action.Delete;
                    x.file = e;
                    tasks.Add(x);
                }
            }

            // do inverse, check for files in remote that arent in local
            foreach (var e in remote.files)
            {
                if (!local_files.ContainsKey(e.hash))
                {
                    // file removed in new version
                    var x = new UpdateTask();
                    x.action = Action.Download;
                    x.file = e;
                    tasks.Add(x);
                }
            }

            return tasks;
        }

        static Dictionary<string, File> FileHashmap(Manifest m)
        {
            var map = new Dictionary<string, File>();
            foreach (var f in m.files) {
                if (map.ContainsKey(f.hash)) continue;
                map.Add(f.hash, f);
            }
            return map;
        }
        
    }

    class BetterWebClient : System.Net.WebClient
    {
        public int Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest lWebRequest = base.GetWebRequest(uri);
            lWebRequest.Timeout = Timeout;
            ((HttpWebRequest)lWebRequest).ReadWriteTimeout = Timeout;
            return lWebRequest;
        }
    }


    class DownloadTask
    {
        public Uri url;
        public string target;
    }

    class UpdateTask
    {
        public File file;
        public Action action;
    }
    enum Action
    {
        Delete,
        Download
    }
}
