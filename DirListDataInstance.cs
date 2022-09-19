using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirList
{
    public class DirListDataInstance
    {
        public string InstanceName { get; set; } = "";
        public List<DirPath> DirPathList { get; set; } = new();

        public DirListDataInstance() { }

        public DirListDataInstance(string instanceName)
        {
            InstanceName = instanceName;
        }
    }
}
