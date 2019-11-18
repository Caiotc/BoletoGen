using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BoletoGen2.Models.Interface;
using Dapper;

namespace BoletoGen2.Models.Class
{
    public class UserRepository : IUserRepository
    {
        public User SelectUser(Guid UserId)
        {
            User ret;
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["SoulApp"].ConnectionString))
            {
                const string sqlstring = "SELECT * from dbo.[User] Where Id = @Id";
                ret = db.Query<User>(sqlstring, new { Id = UserId }, commandType: CommandType.Text).FirstOrDefault();
            }
            return ret;
        }
    }
}