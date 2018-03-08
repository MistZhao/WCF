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
    public interface IClass
    {
        [OperationContract]
        List<Class> GetInfo(string strWhere);
        [OperationContract]
        bool Add(Class objClass);
        [OperationContract]
        bool Update(Class objClass);
        [OperationContract]
        bool Delete(Class objClass);
    }
}
