using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace SayHelloService
{
    public interface ITeacher
    {
        List<Teacher> GetInfo(string strWhere = "", params object[] objParameters);
        bool Add(Teacher objTeacher);
        bool Update(Teacher objTeacher);
        bool Delete(Teacher objTeacher);
    }
}
