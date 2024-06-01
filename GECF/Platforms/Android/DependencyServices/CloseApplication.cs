using System;
using GECF.Interfaces;

namespace GECF.Platforms.Android.DependencyServices
{
	public class CloseApplication : ICloseApplication
    {
        /// <summary>
        /// Closes the application.
        /// </summary>
        public void closeApplication()
        {

            global::Android.OS.Process.KillProcess(global::Android.OS.Process.MyPid());
        }
    }
}

