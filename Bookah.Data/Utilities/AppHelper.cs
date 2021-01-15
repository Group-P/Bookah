using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookah.Data.Utilities
{
    public class AppHelper
    {
        public static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "cU4ODAgQ4wU9ahR4PC1JWfBtPqlLszijkcqfEbMJ",
            BasePath = "https://itc327w-group-p-bookah-default-rtdb.firebaseio.com/",


        };
        public static string ApiKey = "AIzaSyDvK8JbNi9eue23AYgOlaO5xwcmpwpSFfU";
        public static string Bucket = "itc327w-group-p-bookah.appspot.com";

        public static IFirebaseClient client;
    }
}
