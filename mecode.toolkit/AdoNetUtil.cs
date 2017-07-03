using System;
using System.Data;
using System.Data.SqlClient;

namespace mecode.toolkit
{
    public class AdoNetUtil
    {

        public static DataTable Query(string sql, string connstr = "", params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(sql, connstr))
            {
                if (parameters != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                }
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
