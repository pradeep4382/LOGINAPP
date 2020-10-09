using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginApp.Models;

namespace LoginApp.Repository
{
    public interface IAccountData
    {
      
        
       
        string GetUserID_By_UserName(string UserName);
        string Get_checkUsernameExits(string username);
       
        string GetUserName_BY_UserID(string UserId);
        
    }
}