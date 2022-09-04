using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirList
{
    [Serializable]
    public class DirPathList
    {
        public List<Util.DirPath> List { get; set; } = new List<Util.DirPath>();

        public DirPathList() { }

        public void AddDirPath()
        {

        }

    }
}
