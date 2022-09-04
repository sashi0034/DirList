using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirList.Util
{
    public struct DirPath
    {
        private string _path;
        public string Path => _path;

        public DirPath(string path)
        {
            _path = path;
        }


    }
}
