using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam.Controller
{
    public abstract class BaseController<T> where T : class, IController
    {
        public virtual bool Add<Type>(Type type)
        {
            return true;
        }

        public virtual bool Delete<Type>(Type type)
        {
            return true;
        }

        public virtual List<Type> Read<Type>()
        {
            return null;
        }

        public virtual bool Update<Type>(Type type)
        {
            return true;
        }
    }
}
