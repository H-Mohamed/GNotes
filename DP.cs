using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DB_SAIR_NOTES
{
    //data processing Layer
    public class DP
    {
        public SqlConnection Cnx = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();
        public SqlDataAdapter da = new SqlDataAdapter();
        public SqlDataReader dr;
        public DataSet ds;
        public static void initCmd(string cmmd,int type)
        { 
            if (type == 1) cmd.CommandType = CommandType.StoredProcedure;
            if (type == 2) cmd.CommandType = CommandType.Text;
            cmd.Connection = Cnx;
            cmd.CommandText = cmmd;
        }
    }
}
