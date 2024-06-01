using System;
using GECF.Interfaces;

namespace GECF.Platforms.iOS.DependencyServices
{
	public class FileService: IFileService
    {
        public void SavePicture(string name, Stream data)//, string location = "temp")
        {

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filePath = Path.Combine(documentsPath, "ChartGraphics" + ".png");

            byte[] bArray = new byte[data.Length];
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (data)
                {
                    data.Read(bArray, 0, (int)data.Length);
                }
                int length = bArray.Length;
                fs.Write(bArray, 0, length);
            }
        }
    }
}

