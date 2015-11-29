using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLibrary
{
    public class UserSession
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        // signalR connectionId
        public string ConnectionId { get; set; }
    }
}
