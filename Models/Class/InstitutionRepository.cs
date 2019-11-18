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
    public class InstitutionRepository : IInstitutionRepository
    {
        public Institution SelectInstitution(Guid id)
        {

            Institution ret;
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["SoulApp"].ConnectionString))
            {
                const string sqlstring = "SELECT * from Institution Where Id = @Id";
                ret = db.Query<Institution>(sqlstring, new { Id = id }, commandType: CommandType.Text).FirstOrDefault();
            }
            return ret;
        }
    }
}