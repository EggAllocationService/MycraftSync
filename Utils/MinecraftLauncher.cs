using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MycraftSync.Utils
{
    class MinecraftLauncher
    {
        public static Root cached;
        public static void Load()
        {
            string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft", "launcher_profiles.json");
            string data = System.IO.File.ReadAllText(file);
            cached = JsonConvert.DeserializeObject<Root>(data);
        }

        public static void Save()
        {
            string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft", "launcher_profiles.json");
            System.IO.File.WriteAllText(file, JsonConvert.SerializeObject(cached));
        }

    }


    public class LauncherVersion
    {
        public int format { get; set; }
        public string name { get; set; }
        public int profilesFormat { get; set; }
    }
    public class LauncherSettings
    {
        public bool crashAssistance { get; set; }
        public bool enableAdvanced { get; set; }
        public bool enableAnalytics { get; set; }
        public bool enableHistorical { get; set; }
        public bool enableReleases { get; set; }
        public bool enableSnapshots { get; set; }
        public bool keepLauncherOpen { get; set; }
        public string profileSorting { get; set; }
        public bool showGameLog { get; set; }
        public bool showMenu { get; set; }
        public bool soundOn { get; set; }
    }
    public class Root
    {
        public Dictionary<string, AuthenticationItem> authenticationDatabase { get; set; }
        public string clientToken { get; set; }
        public LauncherVersion launcherVersion { get; set; }
        public Dictionary<string, LauncherProfile> profiles { get; set; }
        public SelectedUser selectedUser { get; set; }
        public LauncherSettings settings { get; set; }
    }
    public class LauncherProfile
    {
        public string created { get; set; }
        public string gameDir { get; set; }
        public string icon { get; set; }
        public string lastUsed { get; set; }
        public string lastVersionId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
    public class SelectedUser
    {
        public string account { get; set; }
        public string profile { get; set; }
    }
    public class AuthenticationItem
    {
        public string accessToken;
        public List<object> properties;
        public string username;
        public Dictionary<string, AuthenticationProfile> profiles;
    }

    public class AuthenticationProfile
    {
        public string displayName;
    }

}
