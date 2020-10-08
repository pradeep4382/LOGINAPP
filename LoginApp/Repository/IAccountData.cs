using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginApp.Models;

namespace LoginApp.Repository
{
    public interface IAccountData
    {
        IEnumerable<Role> GetRoles();
        IEnumerable<Users> GetAllUsers();
        string GetRoleByUserID(string UserId);
        string GetUserID_By_UserName(string UserName);
        string Get_checkUsernameExits(string username);
        bool Get_CheckUserRoles(string UserId);
        string GetUserName_BY_UserID(string UserId);
        
    }
}