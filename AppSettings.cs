using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tandem.API
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string ConnectionString { get; set; }
    }

    public class ConnectionStrings
    {
        public string TandemDB { get; set; }
    }
}
