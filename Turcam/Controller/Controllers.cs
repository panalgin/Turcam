using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam.Controller
{
    public static class Controllers
    {
        public static DrillBitController DrillBitController { get; set; }

        public static void Initialize()
        {
            DrillBitController = new DrillBitController();
        }

        public static void Parse(string json)
        {

        }
    }
}
