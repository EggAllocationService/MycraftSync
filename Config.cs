using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MycraftSync.Utils;
using Newtonsoft.Json;

namespace MycraftSync
{
    class Config
    {
        public static readonly HttpClient http = new HttpClient();
        public static HashSet<string> sources = new HashSet<string>();

        public async static void AddSource(string url)
        {
            var response = await http.GetAsync(url);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) { return; }
            string result = await response.Content.ReadAsStringAsync();
            Manifest m = JsonConvert.DeserializeObject<Manifest>(result);
            
        } 
    }
}
