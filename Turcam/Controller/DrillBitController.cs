using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turcam;

namespace Turcam.Controller
{
    public sealed class DrillBitController : BaseController
    {
        public override bool Add(string data)
        {
            base.Add(data);

            return false;
        }

        public override bool Delete(string data)
        {
            base.Delete(data);

            return false;
        }

        public override List<T> Read<T>()
        {
            base.Read<T>();

            return null;
        }

        public override bool Update(string data)
        {
            base.Update(data);

            return false;
        }
    }
}
