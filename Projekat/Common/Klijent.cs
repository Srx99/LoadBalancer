using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Klijent
    {
        public ISlanje proxy { get; set; }

        public Klijent()
        {
           
            ChannelFactory<ISlanje> factory = new ChannelFactory<ISlanje>(new NetTcpBinding(),
                            new EndpointAddress("net.tcp://localhost:4000/ISlanje"));
            

            proxy = null;
            proxy = factory.CreateChannel();
            if(proxy == null)
            {
                throw new Exception("Kanal nije otvoren!");
            }
        }

        [ExcludeFromCodeCoverage]
        public void PosaljiPoruku(string poruka)
        {
            
            proxy.SlanjePoruke(poruka);
            
            
        }

    }
}
