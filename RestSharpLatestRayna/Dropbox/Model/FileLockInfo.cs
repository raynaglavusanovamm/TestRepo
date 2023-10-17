using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatestRayna.Dropbox.Model
{
    public class FileLockInfo
    {
        public DateTime created { get; set; }
        public bool is_lockholder { get; set; }
        public string lockholder_name { get; set; }
    }
}
