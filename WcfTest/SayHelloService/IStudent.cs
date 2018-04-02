using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace SayHelloService
{
    public interface IStudent
    {
        List<Student> GetInfo(string strWhere = "", params object[] objParameters);
        bool Add(Student objStudent);
        bool Update(Student objStudent);
        bool Delete(Student objStudent);
    }
}
