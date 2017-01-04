using System;
using System.Collections.Generic;

namespace Turcam.Controller
{
    public interface IController
    {
        bool Add(string data);
        bool Update(string data);
        bool Delete(string data);
        string Read();
        string Get(int id);
    }
}