using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using PbData.Entities;
using PbData.Business;

namespace PencilBox.Helpers
{
    public static class ps_Membership
    {
        public static bool IsAuthenticated()
        {
            return WebMatrix.WebData.WebSecurity.IsAuthenticated;
        }

        public static pb_UserProfile GetUser()
        {
            var userName = WebMatrix.WebData.WebSecurity.CurrentUserName;
            bn_UserProfile userProfile = new bn_UserProfile();

            return userProfile.GetByUserName(userName);
        }

        public static bool IsCurrentUser(Guid? userId)
        {
            if (!WebMatrix.WebData.WebSecurity.IsAuthenticated ||
                userId == null)
                return false;

            var user = GetUser();
            if (user.UserId == userId)
                return true;
            else
                return false;
        }
    }
}