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
    public interface ITeacher
    {
        [OperationContract]
        List<Teacher> GetInfo(string strWhere);
        [OperationContract]
        bool Add(Teacher objTeacher);
        [OperationContract]
        bool Update(Teacher objTeacher);
        [OperationContract]
        bool Delete(Teacher objTeacher);
    }
}
