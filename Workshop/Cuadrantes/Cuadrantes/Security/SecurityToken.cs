using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cuadrantes.Security
{
    public class SecurityToken

    {
        public Guid id { get; set; }

        public DateTime Expiration { get; set; }

        public string[] Roles { get; set; }

        public string Username { get; set; }

        public string DisplayName { get; set; }
    }
}
