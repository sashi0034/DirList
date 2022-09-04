using DirList.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DirList
{
    public class UserData
    {
        public UserData() { }
        public List<DirPath> DirPathList { get; set; } = new();


        private const string DataPath = @"UserData.xml";


        public void WriteSelf()
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(UserData));
            
            using (var sw = new System.IO.StreamWriter(DataPath, false, new System.Text.UTF8Encoding(false)))
            {
                serializer.Serialize(sw, this);
            }
        }
        public static UserData? ReadFromFile()
        {
            UserData? result = null;

            try
            {
                using (StreamReader sr = new StreamReader(DataPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(UserData));
                    result = serializer.Deserialize(sr) as UserData;
                }

            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
