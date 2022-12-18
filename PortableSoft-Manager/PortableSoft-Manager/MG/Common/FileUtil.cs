using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Util.Util
{
    internal class FileUtil
    {
        private static List<string> s_targetFileExtension = new List<string> { ".exe", ".bat" };
        private static string s_targetFilename = "";
        private static bool s_findTargetFile;
        private static string s_findedFile = "";

        public static string FindExecutableProgramWithSameNameAsDirectory(string directoryPath)
        {
            return FindExecutableProgramWithSameNameAsDirectory(directoryPath, null);
        }

        public static string FindExecutableProgramWithSameNameAsDirectory(string directoryPath, List<string> targetFileExtension)
        {
            s_targetFilename = Path.GetFileName(directoryPath);

            if (targetFileExtension == null)
                targetFileExtension = s_targetFileExtension;

            FindExecutableProgramWithSameNameAsDirectoryRecur(directoryPath);

            s_findTargetFile = false;
            return s_findedFile;
        }

        private static void FindExecutableProgramWithSameNameAsDirectoryRecur(string path)
        {
            if (s_findTargetFile)
                return;

            foreach (string file in Directory.GetFiles(path))
            {
                var filename = Path.GetFileNameWithoutExtension(file);
                if (s_targetFilename.Equals(filename))
                {
                    var extension = Path.GetExtension(file);
                    if (s_targetFileExtension.IndexOf(extension) != -1)
                    {
                        s_findTargetFile = true;
                        s_findedFile = file;
                        return;
                    }
                }
            }

            foreach (string dir in Directory.GetDirectories(path))
            {
                FindExecutableProgramWithSameNameAsDirectoryRecur(dir);
            }

        }
    }
}
