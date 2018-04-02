using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Entity;

namespace SayHelloService
{
    [ServiceContract]
    public interface ITeachManager
    {
        [OperationContract]
        List<Teacher> GetTeachersByClassId(Guid gidClassId);
        [OperationContract]
        List<Student> GetStudentsByClassId(Guid gidClassId);
        [OperationContract]
        List<Class> GetAllClasses();
        [OperationContract]
        bool AddClass(Class objClass);
        [OperationContract]
        bool UpdateClass(Class objClass);
        [OperationContract]
        bool DeleteClass(Class objClass);
        [OperationContract]
        bool AddTeacher(Teacher objTeacher);
        [OperationContract]
        bool UpdateTeacher(Teacher objTeacher);
        [OperationContract]
        bool DeleteTeacher(Teacher objTeacher);
        [OperationContract]
        bool AddStudent(Student objStudent);
        [OperationContract]
        bool UpdateStudent(Student objStudent);
        [OperationContract]
        bool DeleteStudent(Student objStudent);
        [OperationContract]
        bool AddTeachRelation(Teacher objTeacher, Class objClass);
        [OperationContract]
        bool UpdateTeachRelation(Teacher objTeacher, Class objClass);
        [OperationContract]
        bool DeleteTeachRelation(Teacher objTeacher, Class objClass);
    }
}
