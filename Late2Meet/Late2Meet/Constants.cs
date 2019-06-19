using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Late2Meet
{
    static class Constants
    {
        public const string DefaultAdd = "DefaultAdd";
        // The iOS simulator can connect to localhost. However, Android emulators must use the 10.0.2.2 special alias to your host loopback interface.
        public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";
        public static string TodoItemsUrl = BaseAddress + "/api/todoitems/{0}";
    }

}
