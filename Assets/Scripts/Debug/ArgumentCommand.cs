using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgumentCommand<T> : CommandBase
{
    private Func<T,string> _func;

    public ArgumentCommand(string id, string description, string usage, Func<T, string> func) : base(id, description, usage)
    {
        _func = func;
    }

    public string invoke(T arg)
    {
        return _func(arg);
    }
}
