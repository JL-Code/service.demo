using System;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace mecode.toolkit
{
    public static class ExecSqlUtil
    {
        /// <summary>
        /// 利用osql实现执行sql脚本文件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <param name="databasename"></param>
        /// <param name="targetdir"></param>
        public static void ExcuteByOsql()
        {
            try
            {
                var sqlProcess = new Process();
                sqlProcess.StartInfo.FileName = "osql.exe ";
                sqlProcess.StartInfo.Arguments = @" -S zapdev.ticp.net\dev,2989 -U sa -P Hz2017 -d Zapsoft_kx_test_0314 -i E:\Media\Sql\0914升级脚本.sql ";
                sqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                sqlProcess.Start();
                sqlProcess.WaitForExit();
                sqlProcess.Close();
            }
            catch (Exception e)
            {
                throw new Exception("OSQL操作数据库异常", e);
            }
        }

        /// <summary>
        /// 读取文件执行SQL
        /// </summary>
        /// <param name="sqlList"></param>
        /// <param name="connString">server=dev.highzap.com,1989;database=ZapSoft;uid=sa;pwd=Cqhz2015</param>
        public static void ExcuteSqlFile(string filePath, string connString)
        {
            var sqlList = GetSqlFile(filePath);

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand command = new SqlCommand()
            {
                Connection = conn,
                Transaction = trans
            };

            try
            {
                foreach (string sql in sqlList)
                {
                    if(!string.IsNullOrEmpty(sql))
                    {
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new Exception("读取SQL文件操作数据库异常", e);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 把SQL文件读取到流中，以Go为分隔
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        private static ArrayList GetSqlFile(string filePath)
        {
            ArrayList sqlList = new ArrayList();
            if (!File.Exists(filePath))
                return sqlList;

            StreamReader reader = new StreamReader(filePath, System.Text.Encoding.Default);

            string commandText = "", line = "";

            while (reader.Peek() > -1)
            {
                line = reader.ReadLine();
                if (line == "")
                    continue;

                if (!line.TrimEnd().TrimStart().ToUpper().Equals("GO"))
                {
                    commandText += line;
                    commandText += "\r\n";
                }
                else
                {
                    sqlList.Add(commandText);
                    commandText = "";
                }
            }
            reader.Close();

            return sqlList;
        }
    }
}
