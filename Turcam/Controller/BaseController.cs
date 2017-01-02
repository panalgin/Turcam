using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam.Controller
{
    public abstract class BaseController : IController
    {
        public virtual bool Add(string data)
        {
            return true;
        }
        public virtual bool Delete(string data)
        {
            return true;
        }
        public virtual List<T> Read<T>()
        {
            return null;
        }
        public virtual bool Update(string data)
        {
            return true;
        }

        public virtual T Get<T>(int id)
        {
            return default(T);
        }
    }
}
