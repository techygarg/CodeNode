using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Security.Principal;
using System.Web;

namespace CodeNode.ActiveDirectory
{
    public class Helper
    {
        public static Guid GetCurrentUserAdGuid()
        {
            var userGuid = Guid.Empty;
            const string ldapPath = "LDAP://<SID={0}>";

            var userWinId = HttpContext.Current.User.Identity as WindowsIdentity;
            
            if (userWinId != null)
            {
                var userSid = userWinId.User;

                if (userSid != null)
                {
                    using (var userDirEntry = new DirectoryEntry(string.Format(CultureInfo.CurrentCulture, ldapPath, userSid.Value)))
                    {
                        userGuid = userDirEntry.Guid;
                    }
                }
            }
            // this part execute when we run project in debug mode through IDE
            if (userGuid == Guid.Empty)
                userGuid = UserPrincipal.Current.Guid.HasValue
                    ? UserPrincipal.Current.Guid.Value
                    : Guid.Empty;

            return userGuid;
        }
    }
}