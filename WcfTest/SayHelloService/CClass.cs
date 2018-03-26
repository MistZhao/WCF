using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Common;
using System.Data;
using System.Data.SqlClient;

namespace SayHelloService
{
    class CClass : IClass
    {
        public CClass()
        {
            DbHelper.InitConnString();
        }

        public List<Class> GetInfo(string strWhere, params object[] objParameters)
        {
            string strSql = "select * from Class" + (strWhere.Length == 0 ? "":" where " + strWhere);
            DataTable dt = DbHelper.ExecuteDataTable(strSql, objParameters);
            List<Class> lstClasses = new List<Class>();
            foreach (DataRow row in dt.Rows)
            {
                Class objClass = new Class();
                objClass.Id = (Guid)row["Id"];
                objClass.Name = (string)row["Name"];
                lstClasses.Add(objClass);
            }
            return lstClasses;
        }

        public bool Add(Class objClass)
        {
            string strSql = "insert into Class(Id,Name) values(@Id,@Name)";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@Id", objClass.Id),
                new SqlParameter("@Name", objClass.Name));

            return nNum > 0 ? true : false;
        }

        public bool Update(Class objClass)
        {
            string strSql = "update Class set Name=@Name where Id=@Id";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@Id", objClass.Id),
                new SqlParameter("@Name", objClass.Name));

            return nNum > 0 ? true : false;
        }

        public bool Delete(Class objClass)
        {
            string strSql = "delete from Class where Id=@Id";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@Id", objClass.Id));

            return nNum > 0 ? true : false;
        }
    }
}
