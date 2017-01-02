using System;
using System.Collections.Generic;

namespace Turcam.Controller
{
    public interface IController
    {
        bool Add(string data);
        bool Update(string data);
        bool Delete(string data);
        List<T> Read<T>();
        T Get<T>(int id);
    }
}