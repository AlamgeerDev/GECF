using System;
using GECF.Models;

namespace GECF.Utility
{
	public class ICUtility
	{
        public ICUtility()
        {
        }

        private static ICUtility mInstance = null;
        public static ICUtility Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new ICUtility();
                }

                return mInstance;
            }
        }
        public static User mUser = null;
        public static bool rememberPass = false;
        public static bool IsInForeground { get; set; } = false;
        public static string token = string.Empty;
    }
}

