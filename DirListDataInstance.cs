using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirList
{
    public class DirListDataInstance
    {
        public List<DirPath> DirPathList { get; set; } = new();
        public string InstanceName { get; set; } = "";

        public DirListDataInstance() { }

        public DirListDataInstance(string instanceName)
        {
            InstanceName = instanceName;
        }
    }
}
