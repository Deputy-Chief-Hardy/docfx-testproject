﻿using System.Collections.Generic;

namespace IronLua.Compiler.Ast
{
    class FunctionBody : Node
    {
        public List<string> Parameters { get; set; }
        public bool Varargs { get; set; }
        public Block Body { get; set; }

        public FunctionBody(List<string> parameters, bool varargs, Block body)
        {
            Parameters = parameters;
            Varargs = varargs;
            Body = body;
        }
    }
}
