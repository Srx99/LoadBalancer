using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            
                using (ServiceHost svc = new ServiceHost(typeof(Servis)))
                {
                
                
                    svc.AddServiceEndpoint(typeof(Common.ISlanje),
                                                new NetTcpBinding(),
                                                new Uri("net.tcp://localhost:4000/ISlanje"));
                    svc.Open();

                while (true) {

                    if (Servis.primljena != "") {
                        Console.WriteLine(Servis.primljena);
                        Servis.primljena = "";
                    }
                    
                }

                    //Console.ReadLine();
                }

            
        }
    }
}
