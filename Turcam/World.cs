﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam
{
    public static class World
    {
        public static ControlBoard ControlBoard { get; set; }
        public static List<Axis> Axes { get; set; }

        public static void Initialize()
        {
            if (Axes == null)
            {
                Axes = new List<Axis>()
                {
                    new Axis('X'),
                    new Axis('Y'),
                    new Axis('Z'),
                    new Axis('A'),
                    new Axis('B')
                };
            }
        }
    }
}
