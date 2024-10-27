using System;

namespace Watermelon
{
    public class ShareManager
    {
        public static void Share(Action<bool> callback, bool showErrorMessage = true)
        {
            callback(true);
        }

    }
}