using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turcam;
using Newtonsoft.Json;

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

        public override string Read()
        {
            base.Read();

            using(TurcamEntities m_Context = new TurcamEntities())
            {
                var m_DrillBits = m_Context.DrillBits.ToList();
                return JsonConvert.SerializeObject(m_DrillBits);
            }
        }

        public override bool Update(string data)
        {
            base.Update(data);

            return false;
        }
    }
}
