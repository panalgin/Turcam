using System;
using System.Collections.Generic;

namespace Turcam.Controller
{
    public interface IController
    {
        string Add(string data);
        string Update(string data);
        bool Delete(int id);
        string Read();
        string Get(int id);
    }
}