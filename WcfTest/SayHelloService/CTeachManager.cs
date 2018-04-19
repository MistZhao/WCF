using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Common;
using System.Data.SqlClient;
using System.Data;

namespace SayHelloService
{
    public class CTeachManager : ITeachManager
    {
        public CTeachManager()
        {
            DbHelper.InitConnString();
        }

        public List<Teacher> GetTeachersByClassId(Guid gidClassId)
        {
            string strWhere = "Id in(select TeacherId from TeacherToClass where ClassId=@ClassId)";
            return new CTeacher().GetInfo(strWhere, new SqlParameter("@ClassId", gidClassId));
        }

        public List<Student> GetStudentsByClassId(Guid gidClassId)
        {
            string strWhere = "ClassId=@ClassId";
            return new CStudent().GetInfo(strWhere, new SqlParameter("@ClassId", gidClassId));
        }

        /// <summary>
        /// 获取所有班级对象
        /// </summary>
        /// <returns>List</returns>
        public List<Class> GetAllClasses()
        {
            return new CClass().GetInfo();
        }

        public bool AddClass(Class objClass)
        {
            return new CClass().Add(objClass);
        }

        public bool UpdateClass(Class objClass)
        {
            return new CClass().Update(objClass);
        }

        public bool DeleteClass(Class objClass)
        {
            return new CClass().Delete(objClass);
        }

        public bool AddTeacher(Teacher objTeacher)
        {
            return new CTeacher().Add(objTeacher);
        }

        public bool UpdateTeacher(Teacher objTeacher)
        {
            return new CTeacher().Update(objTeacher);
        }

        public bool DeleteTeacher(Teacher objTeacher)
        {
            return new CTeacher().Delete(objTeacher);
        }

        public bool AddStudent(Student objStudent)
        {
            return new CStudent().Add(objStudent);
        }

        public bool UpdateStudent(Student objStudent)
        {
            return new CStudent().Update(objStudent);
        }

        public bool DeleteStudent(Student objStudent)
        {
            return new CStudent().Delete(objStudent);
        }

        public bool AddTeachRelation(Teacher objTeacher, Class objClass)
        {
            string strSql = "insert into TeacherToClass(TeacherId,ClassId) values(@TeacherId,@ClassId)";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@TeacherId", objTeacher.Id),
                new SqlParameter("@ClassId", objClass.Id)
                );

            return nNum > 0 ? true : false;
        }

        public bool UpdateTeachRelation(Teacher objTeacher, Class objClass)
        {
            string strSql = "update TeacherToClass set TeacherId=@TeacherId,ClassId=@ClassId";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@TeacherId", objTeacher.Id),
                new SqlParameter("@ClassId", objClass.Id)
                );

            return nNum > 0 ? true : false;
        }

        public bool DeleteTeachRelation(Teacher objTeacher, Class objClass)
        {
            string strSql = "delete from TeacherToClass where TeacherId=@TeacherId and ClassId=@ClassId";
            int nNum = DbHelper.ExecuteNonQuery(strSql,
                new SqlParameter("@TeacherId", objTeacher.Id), 
                new SqlParameter("@ClassId", objClass.Id));

            return nNum > 0 ? true : false;
        }
    }
}
