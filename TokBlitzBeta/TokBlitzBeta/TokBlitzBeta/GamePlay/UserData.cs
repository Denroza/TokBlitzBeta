using System;
using System.Collections.Generic;
using System.Text;

namespace TokBlitzBeta.GamePlay
{
    public class UserData
    {
        static string UserLabel;
        static string User_Photo;
        static string UserID;
        public static void SetUserDataValues(string label,string photo_url,string user) {
            if (String.IsNullOrEmpty(label) || String.IsNullOrWhiteSpace(label)) { label = ""; }
            if (String.IsNullOrEmpty(photo_url) || String.IsNullOrWhiteSpace(photo_url)) {
                photo_url = "";
            }
            UserLabel = label;
            User_Photo = photo_url;
           UserID = user;
            GetUserLabel();
            GetUserID();
            GetUserProfileUrl();
        }
        public static string GetUserLabel() {
            return UserLabel;
        }
        public static string GetUserProfileUrl() {
            return User_Photo;
        }
        public static string GetUserID() {
            return UserID;
        }
    }
}
