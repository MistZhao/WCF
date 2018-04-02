using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace SayHelloService
{
    public interface IClass
    {
        List<Class> GetInfo(string strWhere = "", params object[] objParameters);
        bool Add(Class objClass);
        bool Update(Class objClass);
        bool Delete(Class objClass);
    }
}
