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
        List<Class> GetClassesByTeacherId(Guid gidTeacherId);

        bool GetTeachRelation();
        bool AddTeachRelation();
        bool UpdateTeachRelation();
        bool DeleteTeachRelation();
    }
}
