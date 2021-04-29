using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MycraftSync.Utils
{
    public class File
    {
        public string path;
        public string hash;
        public long size;
    }
    public class Manifest
    {
        public long version;
        public List<File> files;
    }
}
