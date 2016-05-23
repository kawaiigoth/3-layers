using _EPAM_UserList.BLL;
using _EPAM_UserList.BLL.UserLogic;
using _EPAM_UserList.Interfases.BLL;
using _EPAM_UserList.BLL.UserListLogic;
using Logs;
using System.Collections.Generic;
using _EPAM_UserList.Entites;
using System.Globalization;
using System.Configuration;
using System.Web.UI.WebControls;
using _EPAM_UserList.PL.WebUI;
using System.Web.Security;
using _EPAM_UserList.Interfases;
using System;

namespace _EPAM_UserList.PL.WebUI
{
    public class MyRoleProvider : RoleProvider
    {
        IAppUserListBLL AppUserLogic = new AppUserLogic();
        IAppRoleUserListBLL AppRoleLogic = new AppRoleLogic();
             
        public override bool IsUserInRole(string username, string roleName)
        {
            var Username = AppUserLogic.Get(username);
            var RoleName = AppRoleLogic.Get(roleName);

            if (AppUserLogic.IsInRole(Username, RoleName))
                return true;
            else return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();
            foreach (var role in AppUserLogic.Get(username).Roles)
            {
                roles.Add(role.Name);
            }
            return roles.ToArray();
        }

        #region Not Implemented
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }



        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }



        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}