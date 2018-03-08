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
    public interface IStudent
    {
        [OperationContract]
        List<Student> GetInfo(string strWhere);
        [OperationContract]
        bool Add(Student objStudent);
        [OperationContract]
        bool Update(Student objStudent);
        [OperationContract]
        bool Delete(Student objStudent);
    }
}
