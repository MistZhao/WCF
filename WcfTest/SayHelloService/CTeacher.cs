using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Entity;
using System.Data;
using System.Data.SqlClient;

namespace SayHelloService
{
    class CTeacher : ITeacher
    {
        public CTeacher()
        {
            DbHelper.InitConnString();
        }

        public List<Teacher> GetInfo(string strWhere, params object[] objParameters)
        {
            string strSql = "select * from Teacher" + (strWhere.Length == 0 ? "":" where " + strWhere);
            DataTable dt = DbHelper.ExecuteDataTable(strSql, objParameters);
            List<Teacher> lstTeachers = new List<Teacher>();
            foreach (DataRow row in dt.Rows)
            {
                Teacher objTeacher = new Teacher();
                objTeacher.Id = (Guid)row["Id"];
                objTeacher.Name = (string)row["Name"];
                objTeacher.Age = (int)row["Age"];
                objTeacher.Title = (string)row["Title"];
                lstTeachers.Add(objTeacher);
            }
            return lstTeachers;
        }

        public bool Add(Teacher objTeacher)
        {
            string strSql = "insert into Teacher(Id,Name,Age,Title) values(@Id,@Name,@Age,@Title)";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@Id", objTeacher.Id),
                new SqlParameter("@Name", objTeacher.Name),
                new SqlParameter("@Age", objTeacher.Age),
                new SqlParameter("@Title", objTeacher.Title)
                );

            return nNum > 0 ? true : false;
        }

        public bool Update(Teacher objTeacher)
        {
            string strSql = "update Teacher set Name=@Name, Age=@Age, Title=@Title where Id=@Id";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@Id", objTeacher.Id),
                new SqlParameter("@Name", objTeacher.Name),
                new SqlParameter("@Age", objTeacher.Age),
                new SqlParameter("@Title", objTeacher.Title)
                );

            return nNum > 0 ? true : false;
        }

        public bool Delete(Teacher objTeacher)
        {
            string strSql = "delete from Teacher where Id=@Id";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@Id", objTeacher.Id));

            return nNum > 0 ? true : false;
        }
    }
}
