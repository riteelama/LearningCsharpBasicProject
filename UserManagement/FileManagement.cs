using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement
{
    public class FileManagement
    {
        public void SaveFile(string fileName, string txtToWrite)
        {
            string strFileNameWithPath = GetFilePath();
            if(!Directory.Exists(strFileNameWithPath))
            {
                Directory.CreateDirectory(strFileNameWithPath);
            }
            //Path.Combine("root", "join", "filename we want to add"); better option for folder operation
            strFileNameWithPath += fileName;
            File.WriteAllText(strFileNameWithPath, txtToWrite);                
        }

        public string GetFilePath()
        {
            //stores the file within the bin directory
            string str = @".\myFiles\";
            return str;
        }
    }
}
