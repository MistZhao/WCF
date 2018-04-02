using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data;
using System.Data.SqlClient;

namespace SayHelloService
{
    class CStudent : IStudent
    {
        public CStudent()
        {
            DbHelper.InitConnString();
        }

        public List<Student> GetInfo(string strWhere = "", params object[] objParameters)
        {
            string strSql = "select * from Student" + (strWhere.Length == 0 ? "" : " where " + strWhere);
            DataTable dt = DbHelper.ExecuteDataTable(strSql, objParameters);
            List<Student> lstStudents = new List<Student>();
            foreach (DataRow row in dt.Rows)
            {
                Student objStudent = new Student();
                objStudent.Id = (Guid)row["Id"];
                objStudent.Name = (string)row["Name"];
                objStudent.Age = (int)row["Age"];
                objStudent.ClassId = (Guid)row["ClassId"];
                lstStudents.Add(objStudent);
            }
            return lstStudents;
        }

        public bool Add(Student objStudent)
        {
            string strSql = "insert into Student(Id,Name,Age,ClassId) values(@Id,@Name,@Age,@ClassId)";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@Id", objStudent.Id),
                new SqlParameter("@Name", objStudent.Name),
                new SqlParameter("@Age", objStudent.Age),
                new SqlParameter("@ClassId", objStudent.ClassId)
                );

            return nNum > 0 ? true : false;
        }

        public bool Update(Student objStudent)
        {
            string strSql = "update Student set Name=@Name, Age=@Age, ClassId=@ClassId where Id=@Id";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@Id", objStudent.Id),
                new SqlParameter("@Name", objStudent.Name),
                new SqlParameter("@Age", objStudent.Age),
                new SqlParameter("@ClassId", objStudent.ClassId)
                );

            return nNum > 0 ? true : false;
        }

        public bool Delete(Student objStudent)
        {
            string strSql = "delete from Student where Id=@Id";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@Id", objStudent.Id));

            return nNum > 0 ? true : false;
        }
    }
}
