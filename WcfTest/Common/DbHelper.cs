using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace Common
{
    public sealed class DbHelper
    {
        private static ENUM_DBTYPE dbType;
        private static ENUM_DBTYPE eDbType
        {
            get
            {
                if (dbType == null)
                {
                    switch (ConfigurationManager.AppSettings["DbType"].ToString())
                    {
                        case "SQLSERVER":
                            dbType = ENUM_DBTYPE.SQLSERVER;
                            break;
                        case "ORACLE":
                            dbType = ENUM_DBTYPE.ORACLE;
                            break;
                        default:
                            dbType = ENUM_DBTYPE.UNDEFINED;
                            break;
                    }
                }

                return dbType;
            }
        }

        private static string strConn = "";

        public static void InitConnString()
        {
            if (eDbType == ENUM_DBTYPE.UNDEFINED)
            {
                MessageBox.Show("配置的数据库类型不正确，无法连接！");
                return;
            }

            switch (eDbType)
            {
                case ENUM_DBTYPE.SQLSERVER:
                    strConn = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
                    break;
                case ENUM_DBTYPE.ORACLE:
                    strConn = ConfigurationManager.ConnectionStrings["ORACLE"].ConnectionString;
                    break;
                default:
                    strConn = "";
                    break;
            }
        }

        /// <summary>
        /// ExecuteNonQuery的重写
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params object[] parameters)
        {
            try
            {
                if (strConn.Length <= 0)
                {
                    MessageBox.Show("未初始化数据库连接语句，无法连接！");
                    return 0;
                }

                switch (eDbType)
                {
                    case ENUM_DBTYPE.SQLSERVER:
                        using (SqlConnection conn = new SqlConnection(strConn))
                        {
                            conn.Open();
                            using (SqlCommand cmd = conn.CreateCommand())//!!!一定要注意连接的打开
                            {
                                cmd.Parameters.AddRange(parameters);//AddRange添加参数数组
                                cmd.CommandText = sql;
                                return cmd.ExecuteNonQuery();
                            }
                        }
                    case ENUM_DBTYPE.ORACLE:
                        using (OracleConnection conn = new OracleConnection(strConn))
                        {
                            conn.Open();
                            using (OracleCommand cmd = conn.CreateCommand())//!!!一定要注意连接的打开
                            {
                                cmd.Parameters.AddRange(parameters);//AddRange添加参数数组
                                cmd.CommandText = sql;
                                return cmd.ExecuteNonQuery();
                            }
                        }
                    default:
                        return 0;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SqlHelper.ExecuteNonQuery执行出错：" + ex.Message + Environment.NewLine + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// ExecuteScalar的重写
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params object[] parameters)
        {
            try
            {
                if (strConn.Length <= 0)
                {
                    MessageBox.Show("未初始化数据库连接语句，无法连接！");
                    return "";
                }

                switch (eDbType)
                {
                    case ENUM_DBTYPE.SQLSERVER:
                        using (SqlConnection conn = new SqlConnection(strConn))
                        {
                            conn.Open();
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.Parameters.AddRange(parameters);
                                cmd.CommandText = sql;
                                return cmd.ExecuteScalar();
                            }
                        }
                    case ENUM_DBTYPE.ORACLE:
                        using (OracleConnection conn = new OracleConnection(strConn))
                        {
                            conn.Open();
                            using (OracleCommand cmd = conn.CreateCommand())
                            {
                                cmd.Parameters.AddRange(parameters);
                                cmd.CommandText = sql;
                                return cmd.ExecuteScalar();
                            }
                        }
                    default:
                        return "";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SqlHelper.ExecuteScalar执行出错：" + ex.Message + Environment.NewLine + ex.StackTrace);
                return "";
            }
        }

        /// <summary>
        /// ExecuteReader的重写，由于本地只是放置数据库的游标，所以在调用的时候不能断开和数据库的连接，所以不能使用using
        /// 调用完后，通过参数传出Connection连接，再关闭
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="objConn"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteReader(string sql, out object objConn,params object[] parameters)
        {
            try
            {
                if (strConn.Length <= 0)
                {
                    MessageBox.Show("未初始化数据库连接语句，无法连接！");
                    objConn = null;
                    return null;
                }

                switch (eDbType)
                {
                    case ENUM_DBTYPE.SQLSERVER:
                        objConn = new SqlConnection(strConn);
                        SqlConnection sqlConn = objConn as SqlConnection;
                        sqlConn.Open();
                        SqlCommand sqlCmd = sqlConn.CreateCommand();
                        sqlCmd.Parameters.AddRange(parameters);
                        sqlCmd.CommandText = sql;
                        SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                        return sqlReader;
                    case ENUM_DBTYPE.ORACLE:
                        objConn = new OracleConnection(strConn);
                        OracleConnection oracleConn = objConn as OracleConnection;
                        oracleConn.Open();
                        OracleCommand oracleCmd = oracleConn.CreateCommand();
                        oracleCmd.Parameters.AddRange(parameters);
                        oracleCmd.CommandText = sql;
                        OracleDataReader oracleReader = oracleCmd.ExecuteReader();
                        return oracleReader;
                    default:
                        objConn = null;
                        return null;
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error("SqlHelper.ExecuteReader执行出错：" + ex.Message + Environment.NewLine + ex.StackTrace);
                switch (eDbType)
                {
                    case ENUM_DBTYPE.SQLSERVER:
                        objConn = new SqlConnection(strConn);
                        break;
                    case ENUM_DBTYPE.ORACLE:
                        objConn = new OracleConnection(strConn);
                        break;
                    default:
                        objConn = null;
                        break;
                }
                return null;
            }
        }

        /// <summary>
        /// 可以再写一个DataSetTable函数，返回DataSetTable，方便
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql, params object[] parameters)
        {
            try
            {
                if (strConn.Length <= 0)
                {
                    MessageBox.Show("未初始化数据库连接语句，无法连接！");
                    return new DataSet();
                }

                switch (eDbType)
                {
                    case ENUM_DBTYPE.SQLSERVER:
                        using (SqlConnection conn = new SqlConnection(strConn))
                        {
                            conn.Open();
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.Parameters.AddRange(parameters);
                                cmd.CommandText = sql;
                                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                DataSet dataSet = new DataSet();
                                adapter.Fill(dataSet);//DataSet是载体，Fill是填充到这个载体里，最后应该返回DataSet
                                return dataSet;
                            }
                        }
                    case ENUM_DBTYPE.ORACLE:
                        using (OracleConnection conn = new OracleConnection(strConn))
                        {
                            conn.Open();
                            using (OracleCommand cmd = conn.CreateCommand())
                            {
                                cmd.Parameters.AddRange(parameters);
                                cmd.CommandText = sql;
                                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                                DataSet dataSet = new DataSet();
                                adapter.Fill(dataSet);//DataSet是载体，Fill是填充到这个载体里，最后应该返回DataSet
                                return dataSet;
                            }
                        }
                    default:
                        return new DataSet();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SqlHelper.ExecuteDataSet执行出错：" + ex.Message + Environment.NewLine + ex.StackTrace);
                return new DataSet();
            }
        }

        /// <summary>
        /// 一般只有一个表，所以返回DataTable=dataSet.Tables[0]
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, params object[] parameters)
        {
            try
            {
                if (strConn.Length <= 0)
                {
                    MessageBox.Show("未初始化数据库连接语句，无法连接！");
                    return new DataTable();
                }

                switch (eDbType)
                {
                    case ENUM_DBTYPE.SQLSERVER:
                        using (SqlConnection conn = new SqlConnection(strConn))
                        {
                            conn.Open();
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.Parameters.AddRange(parameters);
                                cmd.CommandText = sql;
                                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                DataSet dataSet = new DataSet();
                                adapter.Fill(dataSet);//DataSet是载体，Fill是填充到这个载体里，最后应该返回DataSet
                                return dataSet.Tables[0];
                            }
                        }
                    case ENUM_DBTYPE.ORACLE:
                        using (OracleConnection conn = new OracleConnection(strConn))
                        {
                            conn.Open();
                            using (OracleCommand cmd = conn.CreateCommand())
                            {
                                cmd.Parameters.AddRange(parameters);
                                cmd.CommandText = sql;
                                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                                DataSet dataSet = new DataSet();
                                adapter.Fill(dataSet);//DataSet是载体，Fill是填充到这个载体里，最后应该返回DataSet
                                return dataSet.Tables[0];
                            }
                        }
                    default:
                        return new DataTable();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SqlHelper.ExecuteDataTable执行出错：" + ex.Message + Environment.NewLine + ex.StackTrace);
                return new DataTable();
            }
        }

        /// <summary>
        /// 从数据库读取数据时，将数据库的空DBNull转化成null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object FromDbValue(object obj)
        {
            if (obj == DBNull.Value)
            {
                return null;
            }
            else
                return obj;
        }

        /// <summary>
        /// 将数据写入数据库时，将null转换成数据库中的空DBNull
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object ToDbValue(object obj)
        {
            if (obj == null)
            {
                return DBNull.Value;
            }
            else
                return obj;
        }

        /// <summary>
        /// 批量插入数据库
        /// </summary>
        /// <param name="destTableName"></param>
        /// <param name="dt"></param>
        public static void ExecuteSqlBulkCopy(string destTableName, DataTable dt)
        {
            try
            {
                if (strConn.Length <= 0)
                {
                    MessageBox.Show("未初始化数据库连接语句，无法连接！");
                    return;
                }

                switch (eDbType)
                {
                    case ENUM_DBTYPE.SQLSERVER:
                        using (SqlConnection conn = new SqlConnection(strConn))
                        {
                            conn.Open();
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                            {
                                bulkCopy.BulkCopyTimeout = 100;
                                bulkCopy.DestinationTableName = destTableName;
                                bulkCopy.WriteToServer(dt);
                            }
                        }
                        break;
                    default:
                        return;
                }
            }
            catch (System.Exception ex)
            {
                LogHelper.Error("SqlHelper.ExecuteSqlBulkCopy执行出错：" + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        /// <summary>
        /// 批量插入数据库，带事务回滚
        /// </summary>
        /// <param name="destTableName"></param>
        /// <param name="dt"></param>
        public static void ExecuteSqlBulkCopyWithTrans(string destTableName, DataTable dt)
        {
            try
            {
                if (strConn.Length <= 0)
                {
                    MessageBox.Show("未初始化数据库连接语句，无法连接！");
                    return;
                }

                switch (eDbType)
                {
                    case ENUM_DBTYPE.SQLSERVER:
                        using (SqlConnection sqlConn = new SqlConnection(strConn))
                        {
                            sqlConn.Open();
                            using (SqlTransaction transaction = sqlConn.BeginTransaction())
                            {
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn, SqlBulkCopyOptions.CheckConstraints & SqlBulkCopyOptions.UseInternalTransaction, transaction))
                                {
                                    bulkCopy.BulkCopyTimeout = 100;
                                    bulkCopy.DestinationTableName = destTableName;
                                    try
                                    {
                                        bulkCopy.WriteToServer(dt);
                                        transaction.Commit();
                                    }
                                    catch (Exception exp)
                                    {
                                        transaction.Rollback();
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        return;
                }
            }
            catch (System.Exception ex)
            {                
                LogHelper.Error("SqlHelper.ExecuteSqlBulkCopyWithTrans执行出错：" + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
