using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.Socket.Core
{
    public class MedComSetting
    {

        public string? ServerAddr { get; set; }

        public IPAddress? ServerIPAddr =>
            IPAddress.TryParse(ServerAddr, out var ipAddress) ? ipAddress : null;

        public int ServerPort { get; set; }

        public int IsPersistentConnection { get; set; }

        public bool IsPersistent => IsPersistentConnection > 0;

    }
}
