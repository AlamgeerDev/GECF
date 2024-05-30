using System;
namespace GECF.Interfaces
{
    public interface IFileService
    {
        void SavePicture(string name, Stream data);//, string location = "temp");
    }
}

