using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SayHelloService;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (ServiceHost host = new ServiceHost(typeof(CTeachManager)))
                {
                    host.Opened += delegate
                    {
                        Console.WriteLine("服务已经启动，按任意键终止！");
                    };
                    if (host.State != CommunicationState.Opened)
                    {
                        host.Open();
                    }
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                Console.ReadLine();
            }
        }
    }
}
