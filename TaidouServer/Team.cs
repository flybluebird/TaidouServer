using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaidouServer
{
    public class Team
    {
        public List<ClientPeer> ClientPeers=new List<ClientPeer>();

        public Team()
        {
        }

        public Team(ClientPeer peer1,ClientPeer peer2,ClientPeer peer3)
        {
            ClientPeers.Add(peer1);
            ClientPeers.Add(peer2);
            ClientPeers.Add(peer3);
            peer1.team = this;
            peer2.team = this;
            peer3.team = this;
        }
    }
}
