using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using CodeNode.Core.Utils;
using CodeNode.Extention;

namespace CodeNode.ActiveDirectory
{
    public class ActiveDirectoryManager
    {
        #region Variables

        private readonly PrincipalContext principatContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveDirectoryManager"/> class.
        /// </summary>
        public ActiveDirectoryManager()
        {
            principatContext = GetPrincipalContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveDirectoryManager"/> class.
        /// </summary>
        /// <param name="domain">The domain. for e.g.: mycampany.com</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password of the user.</param>
        public ActiveDirectoryManager(string domain, string userName, string password)
        {
            principatContext = GetPrincipalContext(domain, userName, password);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveDirectoryManager"/> class.
        /// </summary>
        /// <param name="context">The PrincipalContext. If you can want to create some custom principal context on which rest of the operation will execute</param>
        public ActiveDirectoryManager(PrincipalContext context)
        {
            Ensure.Argument.NotNull(context, "PrincipalContext");
            principatContext = context;
        }

        #endregion

        #region User Validation

        /// <summary>
        ///     Validates the username and password of a given user
        /// </summary>
        /// <param name="userName">The username to validate</param>
        /// <param name="password">The password of the username to validate</param>
        /// <returns>Returns True of user is valid</returns>
        public bool IsUserValid(string userName, string password)
        {
            return principatContext.ValidateCredentials(userName, password);
        }

        /// <summary>
        ///     Checks if the User Account is Expired
        /// </summary>
        /// <param name="userName">The username to check</param>
        /// <returns>Returns true if Expired</returns>
        public bool IsUserExpired(string userName)
        {
            var oUserPrincipal = GetUser(userName);
            return oUserPrincipal.AccountExpirationDate == null;
        }

        /// <summary>
        ///     Checks if user exists on AD
        /// </summary>
        /// <param name="userName">The username to check</param>
        /// <returns>Returns true if username Exists</returns>
        public bool IsUserExist(string userName)
        {
            return GetUser(userName) != null;
        }

        /// <summary>
        ///     Checks if user account is locked
        /// </summary>
        /// <param name="userName">The username to check</param>
        /// <returns>Returns true of Account is locked</returns>
        public bool IsAccountLocked(string userName)
        {
            var oUserPrincipal = GetUser(userName);
            return oUserPrincipal.IsAccountLockedOut();
        }

        #endregion

        #region Search Methods

        /// <summary>
        ///     Gets a certain user on Active Directory
        /// </summary>
        /// <param name="searchValue">The username to get</param>
        /// <returns>Returns the UserPrincipal Object</returns>
        public UserPrincipal GetUser(string searchValue)
        {
            return GetUser(IdentityType.SamAccountName, searchValue);
        }

        /// <summary>
        ///     Gets the user.
        /// </summary>
        /// <param name="identityType">Type of the identity.</param>
        /// <param name="searchValue">The search value.</param>
        /// <returns></returns>
        public UserPrincipal GetUser(IdentityType identityType, string searchValue)
        {
            var userPrincipal = UserPrincipal.FindByIdentity(principatContext, identityType, searchValue);
            return userPrincipal;
        }

        /// <summary>
        ///     Gets a certain group on Active Directory
        /// </summary>
        /// <param name="groupName">The group to get</param>
        /// <returns>Returns the GroupPrincipal Object</returns>
        public GroupPrincipal GetGroupPrincipal(string groupName)
        {
            var groupPrincipal = GroupPrincipal.FindByIdentity(principatContext, groupName);
            return groupPrincipal;
        }

        /// <summary>
        ///     Gets all user of a AD group.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="recursiveSearch">if set to <c>true</c> [recursive search].</param>
        /// <returns></returns>
        public IEnumerable<Principal> GetGroupUsers(string groupName, bool recursiveSearch = false)
        {
            Ensure.Argument.NotNull(groupName);
            IEnumerable<Principal> users = null;

            var groupPrincipal = GroupPrincipal.FindByIdentity(principatContext, groupName);
            if (groupPrincipal != null)
            {
                users = groupPrincipal.GetMembers(recursiveSearch);
            }
            return users;
        }

        /// <summary>
        ///     Gets all user based on search criteria of a particular group.
        /// </summary>
        /// <param name="search">The user search.</param>
        /// <param name="recursiveSearch">if set to <c>true</c> [recursive search].</param>
        /// <returns></returns>
        public IEnumerable<Principal> GetGroupUsers(UserSearchCriteria search, bool recursiveSearch = false)
        {
            Ensure.Argument.NotNull(search.Parameter);
            Ensure.Argument.NotNullOrEmpty(search.GroupName);


            IEnumerable<Principal> users = null;
            var groupPrincipal = GroupPrincipal.FindByIdentity(principatContext, search.GroupName);

            if (groupPrincipal != null)
            {
                switch (search.Parameter)
                {
                    case SearchOn.Description:
                        users =
                            groupPrincipal.GetMembers(recursiveSearch)
                                .Where(
                                    x =>
                                        search.ExactMatch
                                            ? x.Description.IsEqual(search.SearchValue)
                                            : x.Description.DoContains(search.SearchValue));
                        break;
                    case SearchOn.Guid:
                        users =
                            groupPrincipal.GetMembers(recursiveSearch)
                                .Where(x => x.Guid == new Guid(search.SearchValue));
                        break;
                    case SearchOn.Name:
                        users =
                            groupPrincipal.GetMembers(recursiveSearch)
                                .Where(
                                    x =>
                                        search.ExactMatch
                                            ? x.Name.IsEqual(search.SearchValue)
                                            : x.Name.DoContains(search.SearchValue));
                        break;
                    case SearchOn.SamAccountName:
                        users =
                            groupPrincipal.GetMembers(recursiveSearch)
                                .Where(
                                    x =>
                                        search.ExactMatch
                                            ? x.SamAccountName.IsEqual(search.SearchValue)
                                            : x.SamAccountName.DoContains(search.SearchValue));
                        break;
                    case SearchOn.Sid:
                        users =
                            groupPrincipal.GetMembers(recursiveSearch)
                                .Where(x => x.Sid.Equals(new SecurityIdentifier(search.SearchValue)));
                        break;
                    case SearchOn.UserPricipalName:
                        users =
                            groupPrincipal.GetMembers(recursiveSearch)
                                .Where(
                                    x =>
                                        search.ExactMatch
                                            ? x.UserPrincipalName.IsEqual(search.SearchValue)
                                            : x.UserPrincipalName.DoContains(search.SearchValue));
                        break;
                    case SearchOn.Email:
                        users =
                            groupPrincipal.GetMembers(recursiveSearch)
                                .Where(
                                    x =>
                                        search.ExactMatch
                                            ? ((UserPrincipal)x).EmailAddress.IsEqual(search.SearchValue)
                                            : ((UserPrincipal)x).EmailAddress.DoContains(search.SearchValue));

                        break;
                    case SearchOn.Firstname:
                        users =
                            groupPrincipal.GetMembers(recursiveSearch)
                                .Where(
                                    x =>
                                        search.ExactMatch
                                            ? ((UserPrincipal)x).GivenName.IsEqual(search.SearchValue)
                                            : ((UserPrincipal)x).GivenName.DoContains(search.SearchValue));

                        break;
                    case SearchOn.MiddleName:
                        users =
                            groupPrincipal.GetMembers(recursiveSearch)
                                .Where(
                                    x =>
                                        search.ExactMatch
                                            ? ((UserPrincipal)x).MiddleName.IsEqual(search.SearchValue)
                                            : ((UserPrincipal)x).MiddleName.DoContains(search.SearchValue));

                        break;
                    case SearchOn.SurName:
                        users =
                            groupPrincipal.GetMembers(recursiveSearch)
                                .Where(
                                    x =>
                                        search.ExactMatch
                                            ? ((UserPrincipal)x).Surname.IsEqual(search.SearchValue)
                                            : ((UserPrincipal)x).Surname.DoContains(search.SearchValue));

                        break;
                }
            }
            return users;
        }


        /// <summary>
        ///     Gets the users irrespective of their group based on search criteria .
        /// </summary>
        /// <param name="criteria">The search.</param>
        /// <param name="recursiveSearch">if set to <c>true</c> [recursive search].</param>
        /// <returns></returns>
        public IEnumerable<Principal> GetUsers(UserSearchCriteria criteria, bool recursiveSearch = false)
        {
            var search = new UserPrincipal(principatContext);
            switch (criteria.Parameter)
            {
                case SearchOn.Description:
                    search.Description = GetFormattedSearchString(criteria.ExactMatch, criteria.SearchValue);
                    break;
                case SearchOn.Name:
                    search.Name = GetFormattedSearchString(criteria.ExactMatch, criteria.SearchValue);
                    break;
                case SearchOn.SamAccountName:
                    search.SamAccountName = GetFormattedSearchString(criteria.ExactMatch, criteria.SearchValue);
                    break;
                case SearchOn.UserPricipalName:
                    search.UserPrincipalName = GetFormattedSearchString(criteria.ExactMatch, criteria.SearchValue);
                    break;
                case SearchOn.Email:
                    search.EmailAddress = GetFormattedSearchString(criteria.ExactMatch, criteria.SearchValue);
                    break;
                case SearchOn.Firstname:
                    search.GivenName = GetFormattedSearchString(criteria.ExactMatch, criteria.SearchValue);
                    break;
                case SearchOn.MiddleName:
                    search.MiddleName = GetFormattedSearchString(criteria.ExactMatch, criteria.SearchValue);
                    break;
                case SearchOn.SurName:
                    search.Surname = GetFormattedSearchString(criteria.ExactMatch, criteria.SearchValue);
                    break;
            }
            var searcher = new PrincipalSearcher { QueryFilter = search };
            IEnumerable<Principal> users = searcher.FindAll();
            return users;
        }

        #endregion

        #region Group Methods

        /// <summary>
        ///     Checks if user is a member of a given group
        /// </summary>
        /// <param name="userName">The user you want to validate</param>
        /// <param name="groupName">The group you want to check the membership of the user</param>
        /// <returns>Returns true if user is a group member</returns>
        public bool IsUserGroupMember(string userName, string groupName)
        {
            var userPrincipal = GetUser(userName);
            var groupPrincipal = GetGroupPrincipal(groupName);
            var result = false;
            if (userPrincipal != null && groupPrincipal != null)
            {
                result = groupPrincipal.Members.Contains(userPrincipal);
            }
            return result;
        }

        /// <summary>
        ///     Gets a list of the users group memberships
        /// </summary>
        /// <param name="userName">The user you want to get the group memberships</param>
        /// <returns>Returns an arraylist of group memberships</returns>
        public IEnumerable<string> GetUserGroupNames(string userName)
        {
            var userPrincipal = GetUser(userName);
            return userPrincipal.GetGroups().ToList().Select(x => x.Name);
        }

        /// <summary>
        ///     Gets the user groups.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public IEnumerable<Principal> GetUserGroups(string userName)
        {
            var userPrincipal = GetUser(userName);
            return userPrincipal.GetGroups();
        }

        /// <summary>
        ///     Gets a list of the users authorization groups
        /// </summary>
        /// <param name="userName">The user you want to get authorization groups</param>
        /// <returns>Returns an arraylist of group authorization memberships</returns>
        public IEnumerable<string> GetUserAuthorizationGroupNames(string userName)
        {
            var userPrincipal = GetUser(userName);
            return userPrincipal.GetAuthorizationGroups().ToList().Select(x => x.Name);
        }

        /// <summary>
        ///     Gets the user authorization groups.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public IEnumerable<Principal> GetUserAuthorizationGroups(string userName)
        {
            var userPrincipal = GetUser(userName);
            return userPrincipal.GetAuthorizationGroups();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets the base principal context
        /// </summary>
        /// <returns>Returns the PrincipalContext object</returns>
        private static PrincipalContext GetPrincipalContext()
        {
            var principalContext = new PrincipalContext(ContextType.Domain);
            return principalContext;
        }

        /// <summary>
        ///     Gets the principal context.
        /// </summary>
        /// <param name="domain">The domain name. For eg. "mycompany.com"</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        private static PrincipalContext GetPrincipalContext(string domain, string userName, string password)
        {
            var principalContext = new PrincipalContext(ContextType.Domain, domain, userName, password);
            return principalContext;
        }

        /// <summary>
        ///     Gets the formatted search string.
        /// </summary>
        /// <param name="exactMatch">if set to <c>true</c> [exact match].</param>
        /// <param name="searchValue">The search value.</param>
        /// <returns></returns>
        private static string GetFormattedSearchString(bool exactMatch, string searchValue)
        {
            return exactMatch ? searchValue : searchValue + "*";
        }

        #endregion
    }
}