﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckInterpreter
{
    internal interface IBrainfuckCompiler
    {
        IEnumerable<BrainfuckInstruction> Compile();
    }
}
