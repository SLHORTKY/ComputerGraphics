using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial1
{
    internal class EntryPoint
    {
        [STAThread]
        private static void Main(string[] args) => new Client.Program().Run();
    }
}
