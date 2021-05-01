using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MycraftSync.Utils;
using Newtonsoft.Json;

namespace MycraftSync
{
    class Config
    {
        public static readonly HttpClient http = new HttpClient();
        public static List<Source> sources = new List<Source>();
        public static string rootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".myc");

        public static void Init()
        {
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
                Save();
            } else
            {
                // config already exists
                var tmp = System.IO.File.ReadAllText(Path.Combine(rootDir, "sources.json"));
                sources = JsonConvert.DeserializeObject<List<Source>>(tmp);
            }
        }
        public static void Save()
        {
            System.IO.File.WriteAllText(Path.Combine(rootDir, "sources.json"), JsonConvert.SerializeObject(sources));
        }

        public async static Task<bool> UpdateNeeded(string pack)
        {
            if (!Directory.Exists(Path.Combine(rootDir, pack))) {
                // We don't have the pack even installed
                return true;
            }
            if (!System.IO.File.Exists(Path.Combine(rootDir, pack, ".manifest.json")))
            {
                // We don't have a manifest.json for some reason;
                return true;
            }
            // the pack is installed, check diff between local and remote manifest
            Manifest local = JsonConvert.DeserializeObject<Manifest>(System.IO.File.ReadAllText(Path.Combine(rootDir, pack, ".manifest.json")));
            var response = await http.GetAsync(GetSource(pack).baseURL + "/manifest.json");
            if (response.StatusCode != System.Net.HttpStatusCode.OK) { return false; }
            string result = await response.Content.ReadAsStringAsync();
            Manifest remote = JsonConvert.DeserializeObject<Manifest>(result);
            
            return local.version != remote.version;
        }

        public static bool IsInstalled(string pack)
        {
            return MinecraftLauncher.cached.profiles.ContainsKey(pack);
        }

        public async static void AddSource(string url)
        {
            var response = await http.GetAsync(url + "/manifest.json");
            if (response.StatusCode != System.Net.HttpStatusCode.OK) { return; }
            string result = await response.Content.ReadAsStringAsync();
            Manifest m = JsonConvert.DeserializeObject<Manifest>(result);
            Source s = new Source();
            s.baseURL = url;
            var tmp = url.Split('/');
            s.name = tmp[tmp.Length - 2];
            foreach(var src in sources)
            {
                if (s.name == src.name)
                {
                    return;
                }
            }
            sources.Add(s);
            RefreshBox();
        } 

        public static void RefreshBox()
        {
            MainWindow.instance.packBox.SelectedIndex = -1;
            MainWindow.instance.packBox.Items.Clear();
            foreach (var source in sources)
            {
                MainWindow.instance.packBox.Items.Add(source.name);
            }
            MainWindow.instance.actionButton.Enabled = false;
            MainWindow.instance.previewWindow.Url = new Uri("about:blank");
            MainWindow.instance.uninstallButton.Visible = false;

        }

        public static Source GetSource(string name)
        {
            Source s = null;
            foreach (var src in sources)
            {
                if (src.name == name)
                {
                    s = src;
                    break;
                }
            }

            return s;
        }
    }
}
