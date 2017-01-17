using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam.Controller
{
    public abstract class BaseController : IController
    {
        public virtual string Add(string data)
        {
            return null;
        }
        public virtual bool Delete(int id)
        {
            return true;
        }
        public virtual string Read()
        {
            return null;
        }
        public virtual string Update(string data)
        {
            return null;
        }

        public virtual string Get(int id)
        {
            return null;
        }
    }
}
