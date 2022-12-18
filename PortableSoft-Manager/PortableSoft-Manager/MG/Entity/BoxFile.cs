using System;
using System.Drawing;
using System.IO;

namespace PortableSoftManager.MG.Entity
{
    [Serializable]
    public class BoxFile
    {
        private static string s_curPath = Directory.GetCurrentDirectory();
        public string Key { get; set; }
        public string Name { get; set; }
        public Bitmap LargeIcon { get; set; }
        public Bitmap SmallIcon { get; set; }
        public string RelativePath { get; set; }

        //public string Path { get; set; }

        public string AbsolutePath { get { return $"{s_curPath}\\Progress\\{Name}"+RelativePath; } }
    }
}
