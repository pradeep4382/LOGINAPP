using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using LoginApp.Models;
using Dapper;

namespace LoginApp.Repository
{
    public class AccountData : IAccountData
    {

       

       

        public string GetRoleByUserID(string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@UserId", UserId);
                return con.Query<string>("Usp_getRoleByUserID", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public string GetUserID_By_UserName(string UserName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@UserName", UserName);
                return con.Query<string>("Usp_UserIDbyUserName", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public string Get_checkUsernameExits(string username)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@UserName", username);
                return con.Query<string>("Usp_checkUsernameExits", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

       
        public string GetUserName_BY_UserID(string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@UserId", UserId);
                return con.Query<string>("Usp_UserNamebyUserID", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

       
    }
}