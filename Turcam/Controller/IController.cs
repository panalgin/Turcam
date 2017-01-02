using System;
using System.Collections.Generic;

namespace Turcam.Controller
{
    public interface IController
    {
        bool Add<T>(T type);
        bool Update<T>(T type);
        bool Delete<T>(T type);
        List<T> Read<T>();
    }
}