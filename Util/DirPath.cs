using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirList.Util
{
    public interface IDirPath
    {
        string Path { get;}
        string PushedTime { get; }
    }
        

    [Serializable]
    public class DirPath : IDirPath
    {
        public string Path { get; set; } = "";
        public string PushedTime { get; set; } = "";
        public bool HasPushedTime => PushedTime != "";

        public DirPath()
        {}

        public static DirPath CreateWithoutPushedTime(string path)
        {
            var result = new DirPath();
            result.Path = path;
            return result;
        }

        public static DirPath PushNew(string path)
        {
            var result = new DirPath();
            result.Path = path;
            result.PushedTime = DateTime.Now.ToString();
            return result;
        }


    }
}
