﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace TheKitchen
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine();
            engine.LoadAll();
            engine.SaveAll();

            string tacos = ColorsDictionary.ConvertToXamlMarkup();

            int breakpoint = 0;
        }
    }
}
