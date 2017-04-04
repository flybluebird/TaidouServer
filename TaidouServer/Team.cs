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
        public int masterRoleID = -1;

        public Team()
        {
        }

        public Team(ClientPeer peer1,ClientPeer peer2)
        {
            ClientPeers.Add(peer1);
            ClientPeers.Add(peer2);

            peer1.Team = this;
            peer2.Team = this;
            
            masterRoleID = peer2.LoginRole.ID;
        }
    }
}
